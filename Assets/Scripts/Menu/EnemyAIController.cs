using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using System;

public class EnemyAIController : BaseController
{
    int maxArmySize,
    randSpawnNumber, //determines how many times a certain unit is spawned in a row
    minersCount = 0,
    spawnType;// 0: miner; 1: melee; 2: ranged;
    List<Building> buildings;
    List<Unit> topUnits, bottomUnits;
    EnemyAIController Instance;
    void Awake()
    {        
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (ActiveUnits.Count < maxArmySize)
        {
            expandArmy();
        }

        if (topUnits.Count >= maxArmySize / 3)
        {
            //issue command attack on top
            CommunicationEvents.moveTowardsTarget(1, true);
        }

        if (bottomUnits.Count >= maxArmySize / 3)
        {
            //issue command attack on bottom
            CommunicationEvents.moveTowardsTarget(1, false);
        }

        if (topUnits.Count <= maxArmySize / 6)
        {
            //issue command retreat on top
            CommunicationEvents.moveTowardsTarget(2, true);
        }

        if (bottomUnits.Count <= maxArmySize / 6)
        {
            //issue command retreat on bottom
            CommunicationEvents.moveTowardsTarget(2, false);
        }
    }

    void expandArmy()
    {
        if (randSpawnNumber <= 0)
            {
                System.Random rnd = new System.Random(DateTime.Now.Millisecond);
                randSpawnNumber = rnd.Next(2, 3);
                if (minersCount < (maxArmySize / 4))
                {
                    spawnType = 0;
                }
                else
                {
                    rnd = new System.Random(DateTime.Now.Millisecond);
                    spawnType = rnd.Next(1, 2);
                }
            }

            if (topUnits.Count <= maxArmySize / 2)
            {
                storeManager.trySpawnUnit(spawnType, topTrack);
            }
            else
            {
                storeManager.trySpawnUnit(spawnType, bottomTrack);
            }
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

    void buyBuilding()
    {
        
    }
}