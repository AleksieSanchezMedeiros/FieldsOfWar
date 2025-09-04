using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;

public class EnemyAIController : BaseController
{
    int randSpawnNumber, //determines how many times a certain unit is spawned in a row
    minersCount = 0,
    spawnType;// 0: miner; 1: melee; 2: ranged;
    List<Building> buildings;
    List<Unit> topUnits, bottomUnits;
    EnemyAIController Instance;
    [SerializeField] bool spawningInProcess;
    [SerializeField] float askForSpawnCooldown = 15f;
    public override void Awake()
    {
        base.Awake();
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        storeManager = GetComponent<StoreManager>();
        ActiveUnits = new List<Unit>();
        topUnits = new List<Unit>();
        bottomUnits = new List<Unit>();
        buildings = new List<Building>();
    }

    void Update()
    {
        if (ActiveUnits.Count < maxNumberOfTroops && spawningInProcess == false)
        {
            expandArmy();
        }

        if (topUnits.Count >= maxNumberOfTroops / 3)
        {
            //issue command attack on top
            CommunicationEvents.moveTowardsTarget?.Invoke(1, true);
        }

        if (bottomUnits.Count >= maxNumberOfTroops / 3)
        {
            //issue command attack on bottom
            CommunicationEvents.moveTowardsTarget?.Invoke(1, false);
        }

        if (topUnits.Count <= maxNumberOfTroops / 6)
        {
            //issue command retreat on top
            CommunicationEvents.moveTowardsTarget?.Invoke(2, true);
        }

        if (bottomUnits.Count <= maxNumberOfTroops / 6)
        {
            //issue command retreat on bottom
            CommunicationEvents.moveTowardsTarget?.Invoke(2, false);
        }
    }

    void expandArmy()
    {
        //StartCoroutine(waitToBuyUnits());
        if (spawningInProcess)
        {
            return;
        }
        
        spawningInProcess = true;
        StartCoroutine("waitToBuyUnits");
        if (randSpawnNumber <= 0)
        {
            System.Random rnd = new System.Random(DateTime.Now.Millisecond);
            randSpawnNumber = rnd.Next(2, 3);
            if (minersCount < (maxNumberOfTroops / 6)) 
            {
                spawnType = 0;
            }
            else
            {
                rnd = new System.Random(DateTime.Now.Millisecond);
                spawnType = rnd.Next(1, 2);
            }
        }

        if (topUnits.Count <= bottomUnits.Count)
        {
            selectedTopTrack = true;
        }
        else
        {
            selectedTopTrack = false;
        }
        storeManager.trySpawnUnit(spawnType, selectedTopTrack);
        randSpawnNumber--;
    }

    public override void addUnitToUnitList(string _faction, Unit unit, bool isOnTopTrack)
    {
        base.addUnitToUnitList(_faction, unit, isOnTopTrack);
        if (unit is GatheringUnit) minersCount++;
        if (unit.isOnTopTrack)
        {
            topUnits.Add(unit);
        }
        else
        {
            bottomUnits.Add(unit);
        }
    }

    public override void removeUnitFromList(Unit unit)
    {
        base.removeUnitFromList(unit);
        if (unit is GatheringUnit) minersCount--;
        if (unit.isOnTopTrack)
        {
            topUnits.Remove(unit);
        }
        else
        {
            bottomUnits.Remove(unit);
        }
    }

    IEnumerator waitToBuyUnits()
    {
        yield return new WaitForSeconds(askForSpawnCooldown);
        spawningInProcess = false;
    }

    void buyBuilding()
    {
        
    }
}