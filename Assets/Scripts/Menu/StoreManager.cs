using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    [SerializeField] string faction;
    MenuUnitObject[] listOfUnits;
    int funds;

    void Start()
    {
        CommunicationEvents.GatherResource += increaseFunds;
    }


    public bool trySpawnUnit(int val, Transform spawnLocation)
    {
        if (funds < listOfUnits[val].GetCost())
        {
            return false;
        }
        StartCoroutine(spawnUnit(val, spawnLocation));
        funds -= listOfUnits[val].GetCost();
        return true;
    }

    IEnumerator spawnUnit(int val, Transform spawnLocation)
    {
        yield return new WaitForSeconds(listOfUnits[val].GetTimeUntilSpawn());
        Instantiate(listOfUnits[val].GetUnit(), spawnLocation);
    }

    void increaseFunds(string _faction, int value)
    {
        if (_faction != faction) return;

        funds += value;
    }
}