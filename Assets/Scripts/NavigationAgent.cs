using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class NavigationAgent : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;
    private int lastAction = -1;
    private static bool timerRunning = false;
    private static float timerCountdown = 0f;
    private static float controlDuration = 30f;
    private static int requiredUnits = 20;
    private static float proximityThreshold = 1.5f;
    void Start()
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();

        if (waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }

    public void setWaypoints(List<Transform> _waypoints)
    {
        waypoints = _waypoints.ToArray();
    }

    void Update()
    {
        if (!agent.isOnNavMesh) return;
        int action = GameManager.currrentAction;
        if (action != lastAction)
        {
            HandleAction(action);
            lastAction = action;
        }

        CheckIfReachedDestination();
        if (timerRunning)
        {
            timerCountdown -= Time.deltaTime;
            FindObjectOfType<UIManager>().UpdateTimerDisplay(timerCountdown);

            if (timerCountdown <= 0f)
            {
                timerRunning = false;
                // win / lose screen
            }
        }
    }

private void HandleAction(int action)
{
    switch (action) //0 = retreat, 1 = defend, 2 = attack
    {
        case 0: // Retreat 
            MoveToPreviousWaypoint();
            break;
        case 1: // Defend- hold position
            StopMoving();
            break;
        case 2: // attack
            MoveToNextWaypoint();
            break;
    }
}

private void MoveToNextWaypoint()
{
    if (waypoints.Length == 0) return;

    if (currentWaypointIndex < waypoints.Length - 1)
    {
        currentWaypointIndex++;
        agent.SetDestination(waypoints[currentWaypointIndex].position);
    }
    else
    {
        StopMoving(); 
    }
}

private void MoveToPreviousWaypoint()
{
    if (waypoints.Length == 0) return;

    currentWaypointIndex = 0;
    agent.SetDestination(waypoints[currentWaypointIndex].position);
}


private void StopMoving()
{
    agent.ResetPath();
}

private void CheckIfReachedDestination()
{
    if (!agent.pathPending && agent.remainingDistance < 0.5f)
    {
        if (GameManager.currrentAction == 2)
        {
            MoveToNextWaypoint();
        }
    }
}

private void MonitorGroupAtWaypointZero()
{
    if (waypoints.Length == 0 || timerRunning) return;

    NavigationAgent[] allAgents = FindObjectsOfType<NavigationAgent>();
    int count = 0;

    foreach (var unit in allAgents)
    {
        if (unit.CompareTag("Player")) 
        {
            float dist = Vector3.Distance(unit.transform.position, waypoints[0].position);
            if (dist < proximityThreshold)
            {
                count++;
            }
        }
    }

    if (count == requiredUnits) 
    {
        timerRunning = true;
        timerCountdown = controlDuration;
    }
}

private void RunGroupTimer()
{
    if (timerRunning)
    {
        timerCountdown -= Time.deltaTime;
        if (timerCountdown <= 0f)
        {
            timerRunning = false;
           //lose/win screen 
        }
    }
}
}