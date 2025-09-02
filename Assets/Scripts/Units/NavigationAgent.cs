using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

/* 
Remove references to deprecated Scripts 
    - GameManager
*/

public class NavigationAgent : MonoBehaviour
{
    public NavMeshAgent agent;
    Unit myUnit;
    public Transform[] waypoints;
    UIManager ins;
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
        myUnit = GetComponent<Unit>();
        agent.stoppingDistance = (myUnit.shortRange / 3) * 2;
    }

    public void setWaypoints(List<Transform> _waypoints)
    {
        waypoints = _waypoints.ToArray();
    }

    void Update()
    {
        if (!agent.isOnNavMesh) return;
        //by checking on the game manager on update the script invalidates the separation of top and bottom since 
        //any order issued on any will be mirrored by the other next update
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
            UIManager.Instance.UpdateTimerDisplay(timerCountdown);

            if (timerCountdown <= 0f)
            {
                timerRunning = false;
                // win / lose screen
            }
        }
    }

    public void setTarget(Transform enemyPosition)
    {
        
    }

    private void HandleAction(int action)
    {
        switch (action) //0 = retreat, 1 = defend, 2 = attack
        {
            case -1: //go after enemy
                pursueEnemy();
                break;
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

    void pursueEnemy()
    {
        agent.SetDestination(myUnit.getTarget().position);
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