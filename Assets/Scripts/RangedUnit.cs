using UnityEngine;

public class RangedUnit : TroopCommand
{
    public float visionRange = 10f;
    public float visionAngle = 60f;
    public float attackRange = 7f;
    public int attackDamage = 15;
    public float attackCooldown = 1.5f;

    public GameObject arrowPrefab;
    public Transform shootPoint;
    public float projectileSpeed = 12f;

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
                else {
                    if (Time.time - lastAttackTime > attackCooldown) {
                        Shoot(targetEnemy);
                        lastAttackTime = Time.time;
                    }
                }
            }
        }
    }

    private void Shoot(GameObject enemy)
    {
        if (arrowPrefab != null && shootPoint != null) {
            GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, Quaternion.identity);
            Rigidbody rb = arrow.GetComponent<Rigidbody>();

            if (rb != null) {
                Vector3 dir = (enemy.transform.position - shootPoint.position).normalized;
                rb.linearVelocity = dir * projectileSpeed;
            }

            Projectile proj = arrow.GetComponent<Projectile>();
            if (proj != null) {
                proj.damage = attackDamage;
            }
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