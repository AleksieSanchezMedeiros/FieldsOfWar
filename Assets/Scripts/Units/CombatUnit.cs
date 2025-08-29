using System.Collections;
using UnityEngine;

public class CombatUnit : Unit
{
    [SerializeField] LayerMask detection;
    GameObject targetEnemy;
    [SerializeField] float visionAngle;

    void Update()
    {
        // detect if enemy unit or building is nearby, if so set closest enemy unit to target
        if (CalculateDistanceToTarget(targetEnemy.transform) > longRange && targetEnemy != null) targetEnemy = null;
        if (!targetEnemy)
        {
            targetEnemy = FindEnemyInVision(); 
        }

        //move towards enemy
        if (CalculateDistanceToTarget(targetEnemy.transform) >= shortRange)
        {
            MoveTowardsTarget(targetEnemy.transform);
        }
        else if (canAttack)
        {
            //attack nearby enemy
            targetEnemy.GetComponent<Unit>().TakeDamage(damage);
            //play animation
            StartCoroutine(reloadAttack());
        }

        //move towards checkpoint
    }

    public override void Spawn(string _faction)
    {
        Spawn(_faction);
        canAttack = true;
    }

    private GameObject FindEnemyInVision()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, longRange);
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