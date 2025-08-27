using UnityEngine;

public abstract class Unit : MonoBehaviour, IUnitBase
{
    public float speed, shortRange, longRange, distanceToTarget, attackCooldown;
    public int damage, health, maxHealth, type;
    public bool canAttack, isOnTopTrack;
    public string faction;
    [SerializeField] GameObject playerGraphics, enemyGraphics;

    //NAVIGATIONaGENTsTANDiN agentStandIn 

    public void Move() //either <= or =>
    {
        //agentStandIn.move() //either <= or =>
    }

    public void MoveTowardsTarget(Transform targetPosition) {
        //navAgent.move()
    }

    public void Attack(Unit target)
    {
        target.TakeDamage(damage);
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Spawn()
    {
        faction = this.gameObject.tag;
        CommunicationEvents.AddUnitToFactionList?.Invoke(faction, this);
    }

    public void Die()
    {
        CommunicationEvents.RemoveUnitFromFactionList(this);
        Destroy(this);
    }

    public float CalculateDistanceToTarget(Transform target) {
        
        return Vector3.Distance(transform.position, target.position);
    }
}