using UnityEngine;

public class Building : MonoBehaviour
{
    public float range;
    public int HP;
    protected bool hasBeenDestroyed = false;
    public static string price;
    [SerializeField] GameObject buildingBody;
    [SerializeField] protected BaseController factionController;

    public void Build()
    {
        if (!hasBeenDestroyed)
        {
            gameObject.SetActive(true);
        }
    }

    public void DestroyBuilding()
    {
        buildingBody.SetActive(false);
        hasBeenDestroyed = true;
    }

    public void TakeDamage(int incomingDamage)
    {
        HP -= incomingDamage;
        if (HP <= 0)
        {
            DestroyBuilding();
        }
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
