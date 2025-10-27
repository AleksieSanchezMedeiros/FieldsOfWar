using UnityEngine;

public class TroopCommand : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Vector3 baseLocation;
    public Vector3 defendLocation;
    public Vector3 attackLocation;

    protected bool isDoingAction = false;

    protected virtual void Start()
    {
        baseLocation = new Vector3(0, 0, 0);
        defendLocation = new Vector3(3, 0, 0);
        attackLocation = new Vector3(10, 0, 0);
    }

    protected virtual void Update()
    {
        int action = GameManager.currrentAction;

        switch (action)
        {
            case 0: // Retreat
                MoveTo(baseLocation);
                break;
            case 1: // Defend
                MoveTo(defendLocation);
                break;
            case 2: // Attack
                MoveTo(attackLocation);
                break;
        }
    }

    protected void MoveTo(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            moveSpeed * Time.deltaTime
        );
    }
}
