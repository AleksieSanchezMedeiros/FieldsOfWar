using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10;
    public float lifeTime = 5f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Health hp = collision.gameObject.GetComponent<Health>();
        if (hp != null) {
            hp.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
