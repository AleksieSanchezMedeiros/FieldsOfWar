using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class MenuUnitObject : ScriptableObject
{
    [SerializeField] GameObject unit;
    [SerializeField] int cost;
    [SerializeField] float timeUntilSpawn; //how long it takes for a unit to spawn after it's been queued

    void Awake()
    {
        Unit uniTry;
        if (!unit.TryGetComponent<Unit>(out uniTry))
        {
            throw new System.Exception(unit.gameObject + " is not a valid unit type!");
        }
    }

    public GameObject GetUnit()
    {
        return unit;
    }

    public int GetCost()
    {
        return cost;
    }

    public float GetTimeUntilSpawn(){ return timeUntilSpawn; }
}

public class UnitInfo {
    float timeUntilSpawn;
    Unit unitToSpawn;

    public UnitInfo(float time, Unit unit)
    {
        timeUntilSpawn = time;
        unitToSpawn = unit;
    }
}