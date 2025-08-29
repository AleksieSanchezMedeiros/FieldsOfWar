using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    [SerializeField] string faction;
    [SerializeField] Transform topTrackSpawn, bottomTrackSpawn;
    [SerializeField] MenuUnitObject[] listOfUnits;
    int funds;

    void Start()
    {
        CommunicationEvents.GatherResource += increaseFunds;
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

        if (spawnOnTop)
        {
            spawnee = Instantiate(listOfUnits[val].GetUnit(), topTrackSpawn);
        }
        else
        {
            spawnee = Instantiate(listOfUnits[val].GetUnit(), bottomTrackSpawn);

        }

        spawnee.GetComponent<Unit>().isOnTopTrack = spawnOnTop;
        spawnee.GetComponent<Unit>().Spawn(faction);

    }

    void increaseFunds(string _faction, int value)
    {
        if (_faction != faction) return;

        funds += value;
    }
}