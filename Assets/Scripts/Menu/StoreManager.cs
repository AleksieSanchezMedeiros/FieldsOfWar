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
        changeUI();
        return true;
    }

    IEnumerator spawnUnit(int val, bool spawnOnTop)
    {
        GameObject spawnee;
        yield return new WaitForSeconds(listOfUnits[val].GetTimeUntilSpawn());

        if (spawnOnTop)
        {
            spawnee = Instantiate(listOfUnits[val].GetUnit(), topTrackSpawn);
        }
        else
        {
            spawnee = Instantiate(listOfUnits[val].GetUnit(), bottomTrackSpawn.transform.position, new quaternion(0,0,0,0));
        }
        
        if (spawnee.TryGetComponent<GatheringUnit>(out _))
        {
            spawnee.GetComponent<GatheringUnit>().SpawnMe(faction, spawnOnTop, this.gameObject);
        }
        else
        {
            spawnee.GetComponent<Unit>().Spawn(faction, spawnOnTop, this.gameObject);
        }
    }

    void increaseFunds(string _faction, int value)
    {
        if (_faction != faction) return;

        funds += value;
        changeUI();
    }

    void changeUI()
    {
        if (gameObject.tag != "Player") return;
        CommunicationEvents.updateFunds?.Invoke(funds);
    }
}