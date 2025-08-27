using System;
using System.Collections;
using UnityEditor.PackageManager;
using UnityEngine;

public class GatheringUnit : Unit
{

    [SerializeField] GameObject resourceNode, castle;
    Transform target;
    bool goGather = true, loadingCargo = false;
    ResourceNode nodeLogic;

    //Move ignoring key path and instead goes to and fro the nearest resource node 
    void Update()
    {
        if (goGather && !loadingCargo)
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


        if (loadingCargo && CalculateDistanceToTarget(castle.transform) > shortRange)
        {
            resourceNode.GetComponent<ResourceNode>().sendResource(faction);
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
        loadingCargo = true;
    }

    public void setGatherNode(GameObject node)
    {
        resourceNode = node;
        if (!resourceNode.TryGetComponent(out nodeLogic))
        {
            throw new System.Exception(resourceNode + " is not a valid resource node");
        }
    }
}