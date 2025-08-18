using UnityEngine;

public class MeleeUnit : TroopCommand
{
    public float visionRange = 5f;
    public float visionAngle = 45f;
    public float attackRange = 1.5f;
    public int attackDamage = 20;
    public float attackCooldown = 1f;

    private float lastAttackTime = 0f;
    private GameObject targetEnemy;

    protected override void Update()
    {
        base.Update();

        if (GameManager.currrentAction == 2) {
            if (targetEnemy == null || !IsInVision(targetEnemy)) {
                targetEnemy = FindEnemyInVision();
            }

            if (targetEnemy != null) {
                float dist = Vector3.Distance(transform.position, targetEnemy.transform.position);

                if (dist > attackRange) {
                    MoveTo(targetEnemy.transform.position);
                }
                else
                {
                    if (Time.time - lastAttackTime > attackCooldown) {
                        Attack(targetEnemy);
                        lastAttackTime = Time.time;
                    }
                }
            }
        }
    }

    private void Attack(GameObject enemy)
    {
        Health hp = enemy.GetComponent<Health>();
        if (hp != null) {
            hp.TakeDamage(attackDamage);
        }
    }

    private GameObject FindEnemyInVision()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, visionRange);
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
}
