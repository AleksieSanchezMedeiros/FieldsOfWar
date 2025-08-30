using UnityEngine;

public class Tower : Building
{
    public float attackFrequency = 1f;
    public int damage = 10;
    protected float attackTimer;

    private void Update()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackFrequency) {
            GameObject enemy = FindEnemyInRange();
            if (enemy != null) {
                Attack(enemy);
                attackTimer = 0f;
            }
        }
    }

    private GameObject FindEnemyInRange()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, range);
        foreach (var hit in hits) {
            if (hit.CompareTag("Soldier")) {
                return hit.gameObject;
            }
        }
        return null;
    }

    protected void Attack(GameObject target)
    {
        // Uncomment when we have the code for the soliders
        //Soldier s = target.GetComponent<Soldier>();
        //if (s != null) {
        //    s.TakeDamage(damage);
        //}
    }
}
