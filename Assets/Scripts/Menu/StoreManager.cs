using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    MenuUnitObject[] listOfUnits;
    public bool trySpawnUnit(int val, int funds, Transform spawnLocation)
    {
        if (funds < listOfUnits[val].GetCost())
        {
            return false;
        }
        StartCoroutine("spawnUnit");
        return true;
    }

    IEnumerator spawnUnit(int val, Transform spawnLocation)
    {
        yield return new WaitForSeconds(listOfUnits[val].GetTimeUntilSpawn());
        Instantiate(listOfUnits[val].GetUnit());
    }
}