using System.Collections;
using UnityEngine;

public class CombatUnit : Unit
{
    [SerializeField] LayerMask detection;
    GameObject targetEnemy;
    

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        // detect if enemy unit or building is nearby, if so set closest enemy unit to target
        if (CalculateDistanceToTarget(targetEnemy.transform) > longRange) targetEnemy = null;
        if (!targetEnemy)
        {
            if (Physics.Raycast(ray, out RaycastHit Hit, longRange, detection))
            {
                targetEnemy = Hit.collider.gameObject;
            }
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

    IEnumerator reloadAttack()
    {
        canAttack = !canAttack;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = !canAttack;
    }
}