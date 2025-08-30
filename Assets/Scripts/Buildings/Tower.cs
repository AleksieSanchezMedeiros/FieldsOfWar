using UnityEngine;

public class Tower : Building
{
    public float attackFrequency = 1f;
    public int damage = 10;
    protected float attackTimer;
    [SerializeField] LayerMask opposingLayer;

    private void Update()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackFrequency)
        {
            GameObject enemy = FindEnemyInRange();
            if (enemy != null)
            {
                Attack(enemy);
                attackTimer = 0f;
            }
        }
    }

    private GameObject FindEnemyInRange()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, range, opposingLayer.value);
        foreach (var hit in hits) {
            if (!hit.CompareTag(tag)) {
                return hit.gameObject;
            }
        }
        return null;
    }

    protected void Attack(GameObject target)
    {        
        CombatUnit s = target.GetComponent<CombatUnit>();
        if (s != null) {
            s.TakeDamage(damage);
        }
    }
}
