using UnityEngine;

public abstract class Unit : MonoBehaviour, IUnitBase
{
    public float speed, shortRange, longRange, distanceToTarget, attackCooldown;
    public int damage, health, maxHealth, type;
    public bool canAttack;
    public string faction;

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
    }

    public void Spawn()
    {
        faction = this.gameObject.tag;
    }

    public void Die()
    {
    }

    public float CalculateDistanceToTarget(Transform target) {
        
        return Vector3.Distance(transform.position, target.position);
    }
}