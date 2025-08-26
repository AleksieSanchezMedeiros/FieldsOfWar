using System.Collections;
using UnityEngine;

public class GatheringUnit : Unit
{
    [SerializeField] GameObject resourceNode, castle;
    Transform target;
    bool goGather = true;

    //Move ignoring key path and instead goes to and fro the nearest resource node 
    void Update()
    {
        if (goGather)
        {
            target = resourceNode.transform;
        }
        else
        {
            target = castle.transform;
        }

        if (CalculateDistanceToTarget(resourceNode.transform) > shortRange)
        {
            MoveTowardsTarget(target);
            return;
        }

        if (goGather)
        {
            StartCoroutine(startGather());
            return;
        }

    }

    IEnumerator startGather()
    {
        yield return new WaitForSeconds(attackCooldown);
        resourceNode.GetComponent<ResourceNode>().sendResource(faction);
    }
}