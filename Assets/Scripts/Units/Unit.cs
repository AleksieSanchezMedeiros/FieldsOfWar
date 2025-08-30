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

    public virtual void Spawn(string _faction, bool _isOnTopTrack)
    {
        gameObject.tag = _faction;
        isOnTopTrack = _isOnTopTrack;
        gameObject.layer = LayerMask.NameToLayer(_faction);
        CommunicationEvents.AddUnitToFactionList?.Invoke(faction, this, _isOnTopTrack);
        if (faction == "Player")
        {
            transform.Find("player-graphics").gameObject.SetActive(true);
            transform.Find("enemy-graphics").gameObject.SetActive(false);
        }
        else
        {
            transform.Find("enemy-graphics").gameObject.SetActive(true);
            transform.Find("player-graphics").gameObject.SetActive(false);
        }

        health = maxHealth;
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