using UnityEngine;

public abstract class Unit : MonoBehaviour, IUnitBase
{
    public float speed, shortRange, longRange, distanceToTarget, attackCooldown;
    public int damage, health, maxHealth, command, previousCommand;
    public bool canAttack, isOnTopTrack;
    public string faction;
    [SerializeField] GameObject playerGraphics, enemyGraphics;
    NavigationAgent agent;
    HealthBar healthBar;
    protected Transform target;

    void Awake()
    {
        CommunicationEvents.setUnitOrders += setCommand;
        healthBar = GetComponentInChildren<HealthBar>();
        agent = GetComponent<NavigationAgent>();
    }

    public virtual void Move(int move) //either <= or =>
    {
        if (previousCommand == move) return;
        command = move;
        previousCommand = command;
    }

    public virtual void MoveTowardsTarget(Transform targetPosition)
    {
        command = -1;
        agent.setTarget(targetPosition);
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
        healthBar.reduceHP(dmg);
    }

    public Transform getTarget()
    {
        return target;
    }

    public virtual void Spawn(string _faction, bool _isOnTopTrack, GameObject Spawner)
    {
        gameObject.tag = _faction;
        faction = _faction;
        isOnTopTrack = _isOnTopTrack;
        //Debug.Log($"{this} base Unit L 59 inc: {_faction}; present: {faction}");

        if (_faction == "Player")
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
        gameObject.layer = LayerMask.NameToLayer(_faction);
        CommunicationEvents.AddUnitToFactionList?.Invoke(faction, this, _isOnTopTrack);
    }

    public void Die()
    {
        CommunicationEvents.RemoveUnitFromFactionList(this);
        Destroy(this);
    }

    void setCommand(int _command, bool _isOnTop, string _faction)
    {
        if (_faction != faction) return;
        if (_isOnTop != isOnTopTrack) return;
        command = _command;
    }

    public float CalculateDistanceToTarget(Transform target)
    {
        return Vector3.Distance(transform.position, target.position);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, shortRange);
        Gizmos.DrawWireSphere(transform.position, longRange);
    }
}