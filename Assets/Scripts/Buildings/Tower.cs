using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;

public class Tower : Building
{
    public int damage = 10;
    [SerializeField] protected float attackFrequency;
    protected bool canAttack = true;
    [SerializeField] protected LayerMask opposingLayer;
    Unit currentTarget;

    protected virtual void Awake()
    {
        if (tag == "Player")
        {
            opposingLayer = LayerMask.GetMask("Enemy", "Unbreakable"); //enemy layer
        }
        else
        {
            opposingLayer = LayerMask.GetMask("Player", "Unbreakable"); //player layer
        }
    }

    private void FixedUpdate()
    {
        if (FindEnemyInRange() && canAttack)
        {
            Attack(currentTarget);
            canAttack = false;
            StartCoroutine(attackFrequencyStart());
        }
    }

    private Unit FindEnemyInRange()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, range, opposingLayer);
        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out currentTarget))
            {
                return currentTarget;
            }
        }
        return null;
    }

    protected void Attack(Unit target)
    {
        if (currentTarget != null)
        {
            currentTarget.TakeDamage(damage);
        }
    }

    protected IEnumerator attackFrequencyStart()
    {
        yield return new WaitForSeconds(attackFrequency);
        canAttack = true;
    }
}
