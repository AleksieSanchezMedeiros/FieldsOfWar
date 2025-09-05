using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class GatheringUnit : Unit
{
    [SerializeField] public GameObject resourceNode, castle, spawner;
    [SerializeField] bool goGather = true, loadingCargo = false;
    ResourceNode nodeLogic;

    void Start()
    {
        command = 3;
    }

    //Move ignoring key path and instead goes to and fro the nearest resource node 
    public override void Update()
    {
        if (command != 0) command = 3;

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

        base.Update();

        if (loadingCargo && CalculateDistanceToTarget(castle.transform) < shortRange)
        {
            loadingCargo = false;
            //Debug.Log("I'm depositing resources");
            nodeLogic.sendResource(faction);
            goGather = true;
            return;
        }

        if (goGather && CalculateDistanceToTarget(resourceNode.transform) < shortRange)
        {
            StartCoroutine(startGather());
        }
    }

    public void SpawnMe(string _faction, bool _isOnTopTrack, GameObject _spawner)
    {
        gameObject.tag = _faction;
        faction = _faction;
        isOnTopTrack = _isOnTopTrack;
        spawner = _spawner;

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
        spawner.GetComponent<BaseController>().addGathererToUnitList(this, isOnTopTrack);
    }

    public override void Move(int move)
    {
        if (move != 0)//if unit isn't set to retreat then keep mining
        {
            command = 3;
            //MoveTowardsTarget(target);
            return;
        }
        base.Move(move);
    }

    IEnumerator startGather()
    {
        yield return new WaitForSeconds(attackCooldown);
        goGather = false;
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