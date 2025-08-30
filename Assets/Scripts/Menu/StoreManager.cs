using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    [SerializeField] string faction;
    [SerializeField] Transform topTrackSpawn, bottomTrackSpawn;
    [SerializeField] MenuUnitObject[] listOfUnits;
    [SerializeField] int funds;
    [SerializeField] int startingFunds = 100;

    void Awake()
    {
        CommunicationEvents.GatherResource += increaseFunds;
    }

    void Start()
    {
        funds = startingFunds;
        changeUI();
    }

    public bool trySpawnUnit(int val, bool spawnOnTop)
    {
        if (funds < listOfUnits[val].GetCost())
        {
            return false;
        }
        StartCoroutine(spawnUnit(val, spawnOnTop));
        funds -= listOfUnits[val].GetCost();
        return true;
    }

    IEnumerator spawnUnit(int val, bool spawnOnTop)
    {
        GameObject spawnee;
        yield return new WaitForSeconds(listOfUnits[val].GetTimeUntilSpawn());
        UnityEngine.Vector3 spawnPos;

        if (spawnOnTop)
        {
            spawnPos = new UnityEngine.Vector3(topTrackSpawn.position.x, topTrackSpawn.position.y, topTrackSpawn.position.z);
            spawnee = Instantiate(listOfUnits[val].GetUnit(), spawnPos, new quaternion(0,0,0,0));
        }
        else
        {
            spawnee = Instantiate(listOfUnits[val].GetUnit(), bottomTrackSpawn);
        }
        spawnee.GetComponent<Unit>().Spawn(faction, spawnOnTop);
    }

    void increaseFunds(string _faction, int value)
    {
        if (_faction != faction) return;

        funds += value;
        changeUI();
    }

    void changeUI()
    {
        CommunicationEvents.updateFunds(funds);
    }
}