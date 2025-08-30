using UnityEngine;

public class Building : MonoBehaviour
{
    public float range;
    public int HP;
    protected bool hasBeenDestroyed = false;

    public void Build()
    {
        if (!hasBeenDestroyed) {
            gameObject.SetActive(true);
        }
    }

    public void DestroyBuilding()
    {
        gameObject.SetActive(false);
        hasBeenDestroyed = true;
    }

    public void TakeDamage(int incomingDamage)
    {
        HP -= incomingDamage;
        if (HP <= 0) {
            DestroyBuilding();
        }
    }
}
