using UnityEngine;
using System.Collections.Generic;

public class Castle : Tower
{
    public int maxTargets = 3;

    private void Update()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackFrequency)
        {
            List<GameObject> enemies = FindEnemiesInRange();

            int shotsFired = 0;
            foreach (GameObject enemy in enemies)
            {
                if (shotsFired >= maxTargets)
                {
                    break;
                }

                Attack(enemy);
                shotsFired++;
            }
            attackTimer = 0f;
        }
    }

    private List<GameObject> FindEnemiesInRange()
    {
        List<GameObject> enemies = new List<GameObject>();
        Collider[] hits = Physics.OverlapSphere(transform.position, range, opposingLayer << obstacleLayer);

        foreach (var hit in hits)
        {
            if (!hit.CompareTag(gameObject.tag))
            {
                enemies.Add(hit.gameObject);
            }
        }

        return enemies;
    }

    public void Die()
    {

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
