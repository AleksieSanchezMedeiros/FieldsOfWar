using System.Collections;
using UnityEngine;

public class CombatUnit : Unit
{
    [SerializeField] LayerMask detection, obstacles;
    GameObject targetEnemy;
    Unit targetEnemyUnit;
    Building targetEnemyBuilding;
    [SerializeField] float visionAngle;
    [SerializeField] protected AudioClip attackSound;
    [SerializeField] protected AudioSource audioSource;

    void Update()
    {
        // detect if enemy unit or building is nearby, if so set closest enemy unit to target

        if (!targetEnemy)
        {
            targetEnemy = FindEnemyInVision();
        }
        //move towards checkpoint

        //move towards enemy
        if (command == 1 || command == 2)
        {
            if (targetEnemy != null)
            {
                if (CalculateDistanceToTarget(targetEnemy.transform) > longRange)
                {
                    targetEnemy = null;
                    targetEnemyUnit = null;
                    targetEnemyBuilding = null;
                }

                if (CalculateDistanceToTarget(targetEnemy.transform) >= shortRange)
                {
                    MoveTowardsTarget(targetEnemy.transform);
                }

                else if (canAttack)
                {
                    if (targetEnemyBuilding != null || targetEnemyUnit != null)
                    {
                        if (targetEnemy.TryGetComponent(out targetEnemyBuilding))
                        {
                            targetEnemyBuilding.TakeDamage(damage);
                        }
                        else if (targetEnemy.TryGetComponent(out targetEnemyUnit))
                        {
                            targetEnemyUnit.TakeDamage(damage);
                        }
                    }
                    audioSource.PlayOneShot(attackSound);
                    StartCoroutine(reloadAttack());
                }
            }
        }
    }

    public override void Spawn(string _faction, bool _isOnTopTrack)
    {
        base.Spawn(_faction, _isOnTopTrack);
        canAttack = true;
    }

    private GameObject FindEnemyInVision()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, longRange, detection << obstacles);
        foreach (var hit in hits) {
            if (hit.CompareTag("Enemy")) {
                if (IsInVision(hit.gameObject)) {
                    return hit.gameObject;
                }
            }
        }
        return null;
    }

    private bool IsInVision(GameObject obj)
    {
        Vector3 dirToTarget = (obj.transform.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, dirToTarget);

        return angle < visionAngle * 0.5f;
    }

    IEnumerator reloadAttack()
    {
        canAttack = !canAttack;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = !canAttack;
    }
}