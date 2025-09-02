using System.Collections;
using UnityEngine;

public class GatheringUnit : Unit
{

    [SerializeField] GameObject resourceNode, castle;
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

    public override void Move(int move)
    {
        if (move != 0)//if unit isn't set to retreat then keep mining
        {
            command = -1;
            target = resourceNode.transform;
            MoveTowardsTarget(target);
            return;
        }
        base.Move(move);
    }

    IEnumerator startGather()
    {
        yield return new WaitForSeconds(attackCooldown);
        loadingCargo = true;
    }

    public void setCastleAndGatherNode(GameObject _castle, GameObject node)
    {
        resourceNode = node;
        castle = _castle;
        if (!resourceNode.TryGetComponent(out nodeLogic))
        {
            throw new System.Exception(resourceNode + " is not a valid resource node");
        }
    }
}