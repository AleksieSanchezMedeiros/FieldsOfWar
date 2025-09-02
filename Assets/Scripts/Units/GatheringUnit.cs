using System.Collections;
using UnityEngine;

public class GatheringUnit : Unit
{
    [SerializeField] public GameObject resourceNode, castle, spawner;
    [SerializeField] bool goGather = true, loadingCargo = false;
    ResourceNode nodeLogic;

    //Move ignoring key path and instead goes to and fro the nearest resource node 
    void Update()
    {
        if (!resourceNode || !castle)
        {
            spawner.GetComponent<BaseController>().addGathererToUnitList(this, isOnTopTrack);
        }

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


        if (loadingCargo && CalculateDistanceToTarget(castle.transform) < shortRange)
        {
            if(loadingCargo)
                nodeLogic.sendResource(faction);
        }

        if (goGather)
        {
            StartCoroutine(startGather());
            return;
        }
    }

    public void SpawnMe(string _faction, bool _isOnTopTrack, GameObject _spawner)
    {
        gameObject.tag = _faction;
        faction = _faction;
        isOnTopTrack = _isOnTopTrack;
        spawner = _spawner;
        //Debug.Log($"{this} base Unit L 59 inc: {_faction}; present: {faction}");

        if (_faction == "Player")
        {
            transform.Find("player-graphics").gameObject.SetActive(true);
            transform.Find("enemy-graphics").gameObject.SetActive(false);
        }
        else
        {
            transform.Find("enemy-graphics").gameObject.SetActive(true);
            transform.Find("player-graphics").gameObject.SetActive(false);
        }
        health = maxHealth;
        gameObject.layer = LayerMask.NameToLayer(_faction);
        spawner.GetComponent<BaseController>().addUnitToUnitList(faction, this, _isOnTopTrack);
        spawner.GetComponent<BaseController>().addGathererToUnitList(this, isOnTopTrack);
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
        Debug.Log(node.name + _castle.name + "Curse it all");
        resourceNode = node;
        castle = _castle;
        if (!resourceNode.TryGetComponent(out nodeLogic))
        {
            throw new System.Exception(resourceNode + " is not a valid resource node");
        }
    }
}