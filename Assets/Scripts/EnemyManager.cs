using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    public void RequestReinforcements(Building requester)
    {
        // spawn soliders here for towers
    }
}
