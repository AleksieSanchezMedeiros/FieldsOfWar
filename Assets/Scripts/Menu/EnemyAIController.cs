using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using System;

public class EnemyAIController : BaseController
{
    int maxArmySize,
    topArmyCount,
    bottomArmyCount,
    randSpawnNumber, //determines how many times a certain unit is spawned in a row
    minersCount = 0,
    spawnType;// 0: miner; 1: melee; 2: ranged;
    //List<buildings> activeBuildings

    void Update()
    {
        if (ActiveUnits.Count < maxArmySize)
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

            if (topArmyCount <= maxArmySize / 2)
            {
                storeManager.trySpawnUnit(spawnType, topTrack);
            }
            else
            {
                storeManager.trySpawnUnit(spawnType, bottomTrack);
            }
            randSpawnNumber--;

            if (topArmyCount >= maxArmySize / 3)
            {
                //issue command attack on top
            }

            if (bottomArmyCount >= maxArmySize / 3)
            {
                //issue command attack on bottom
            }

            if (topArmyCount <= maxArmySize / 6)
            {
                //issue command retreat on top
            }

            if (bottomArmyCount <= maxArmySize / 6)
            {
                //issue command retreat on bottom
            }
        }
    }

    public override void addUnitToUnitList(string _faction, Unit unit)
    {
        base.addUnitToUnitList(_faction, unit);
        if (unit is GatheringUnit) minersCount++;
        if (unit.isOnTopTrack)
        {
            topArmyCount++;
        }
        else
        {
            bottomArmyCount++;
        }
    }

    public override void removeUnitFromList(Unit unit)
    {
        base.removeUnitFromList(unit);
        if (unit is GatheringUnit) minersCount--;
        if (unit.isOnTopTrack)
        {
            topArmyCount--;
        }
        else
        {
            bottomArmyCount--;
        }
    }
}