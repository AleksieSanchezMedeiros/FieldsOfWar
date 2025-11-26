using UnityEngine;
using System.Collections.Generic;

public class Castle : Tower
{
    public int maxTargets = 3;
    [SerializeField] GameObject[] enemies;

    void Start()
    {
        enemies = new GameObject[maxTargets];
    }

    private void Update()
    {
        if (canAttack)
        {
            FindEnemiesInRange();
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i] != null)
                {
                    Attack(enemies[i].GetComponent<Unit>());
                }
            }
            canAttack = false;
            StartCoroutine(attackFrequencyStart());
        }
    }

    private void FindEnemiesInRange()
    {
        bool targetInArray = false;
        //check if object is already on the array
        //Check if array full with existing objects
        Collider[] hits = Physics.OverlapSphere(transform.position, range, opposingLayer);

        foreach (var hit in hits)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                if (hit.gameObject == enemies[i])
                {
                    targetInArray = true;
                    break;
                }
                targetInArray = false;
            }

            if (!targetInArray)
            {
                for (int i = 0; i < enemies.Length; i++)
                {
                    if (!enemies[i] && hit.gameObject.CompareTag(opposingFaction))
                    {
                        enemies[i] = hit.gameObject;
                        break;
                    }
                }
            }
        }
    }

    public override void DestroyBuilding()
    {
        Die();
        base.DestroyBuilding();
    }

    public void Die()
    {
        CommunicationEvents.onFactionDefeated?.Invoke(faction);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
