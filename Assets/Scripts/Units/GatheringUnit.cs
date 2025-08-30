using System.Collections;
using UnityEngine;

public class GatheringUnit : Unit
{

    [SerializeField] GameObject resourceNode, castle;
    Transform target;
    [SerializeField] bool goGather = true, loadingCargo = false;
    ResourceNode nodeLogic;

    //Move ignoring key path and instead goes to and fro the nearest resource node 
    void Update()
    {
        //if (!resourceNode || !castle) return;

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
            nodeLogic.sendResource(faction);
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

    public void setCastleAndGatherNode(GameObject _castle, GameObject node)
    {
        Debug.Log($"{_castle}, {node}, {resourceNode}, {castle}");
        resourceNode = node;
        castle = _castle;
        if (!resourceNode.TryGetComponent(out nodeLogic))
        {
            throw new System.Exception(resourceNode + " is not a valid resource node");
        }
    }
}