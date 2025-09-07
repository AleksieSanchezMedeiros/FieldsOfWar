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
    [SerializeField] float funds;
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
            spawnee = Instantiate(listOfUnits[val].GetUnit(), bottomTrackSpawn);
        }
        
        if (spawnee.TryGetComponent<GatheringUnit>(out _))
        {
            spawnee.GetComponent<GatheringUnit>().SpawnMe(faction, spawnOnTop, gameObject);
        }
        else
        {
            spawnee.GetComponent<Unit>().Spawn(faction, spawnOnTop, gameObject);
        }
    }

    void increaseFunds(string _faction, float value)
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