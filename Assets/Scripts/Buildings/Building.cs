using UnityEngine;

public class Building : MonoBehaviour
{
    public float range;
    public int HP, maxHealth;
    [SerializeField]protected bool Destroyed = false, isIndestructible = false;
    public static string price;
    protected string faction;
    [SerializeField] GameObject buildingBody;
    [SerializeField] protected BaseController factionController;
    HealthBar healthBar;

    protected virtual void Awake()
    {
        if (maxHealth == 0) return;
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.setMaxValue(maxHealth);
        HP = maxHealth;
        faction = tag;
    }

    public void Build()
    {
        if (!Destroyed)
        {
            gameObject.SetActive(true);
        }
    }

    public void DestroyBuilding()
    {
        gameObject.SetActive(false);
        Destroyed = true;
    }

    public bool TakeDamage(int incomingDamage)
    {
        if (isIndestructible) return false;
        HP = HP - incomingDamage;
        if (HP <= 0)
        {
            DestroyBuilding();
            return Destroyed;
        }
        healthBar.reduceHP(incomingDamage);
        return Destroyed;
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
