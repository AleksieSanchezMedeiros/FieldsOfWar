using System.Collections;
using UnityEngine;

public class CombatUnit : Unit
{
    [SerializeField] LayerMask detection;
    [SerializeField] GameObject targetEnemy;
    [SerializeField] Unit targetEnemyUnit;
    [SerializeField] Building targetEnemyBuilding;
    [SerializeField] float visionAngle;
    [SerializeField] protected AudioClip attackSound;
    [SerializeField] protected AudioSource audioSource;
    [SerializeField] string opposingFaction;

    public override void Update()
    {
        // detect if enemy unit or building is nearby, if so set closest enemy unit to target
        if (targetEnemy == null)
        {
            targetEnemy = FindEnemyInVision();
        }
        //move towards checkpoint
        base.Update();
        //move towards enemy
        //Debug.Log($"Post base update \n targetEnemy = {targetEnemy}");
        if (command != 0)
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
                    //MoveTowardsTarget(targetEnemy.transform);
                }
                else if (canAttack)
                {
                    if (targetEnemy.TryGetComponent(out targetEnemyBuilding))
                    {
                        targetEnemyBuilding.TakeDamage(damage);
                    }
                    else if (targetEnemy.TryGetComponent(out targetEnemyUnit))
                    {
                        targetEnemyUnit.TakeDamage(damage);
                    }
                    //audioSource.PlayOneShot(attackSound);
                    canAttack = false;
                    StartCoroutine(reloadAttack());
                }
            }
        }
    }

    public override void Spawn(string _faction, bool _isOnTopTrack, GameObject spawner)
    {
        base.Spawn(_faction, _isOnTopTrack, spawner);
        canAttack = true;
        if (faction == "Player")
        {
            detection = LayerMask.GetMask("Enemy", "Unbreakable"); //enemy layer
            opposingFaction = "Enemy";
        }
        else
        {
            detection = LayerMask.GetMask("Player", "Unbreakable"); //player layer
            opposingFaction = "Player";
        }
    }

    private GameObject FindEnemyInVision()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, longRange, detection);
        foreach (var hit in hits)
        {
            if (hit.CompareTag(opposingFaction))
            {
                if (IsInVision(hit.gameObject))
                {
                    target = hit.gameObject.transform;
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
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}