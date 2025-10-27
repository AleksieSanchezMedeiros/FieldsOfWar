using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "MenuUnit", menuName = "Menu/Unit", order = 0)]
public class MenuUnitObject : ScriptableObject
{
    [SerializeField] GameObject unit;
    [SerializeField] int cost;
    [SerializeField] float timeUntilSpawn; //how long it takes for a unit to spawn after it's been queued

    void Awake()
    {
        if (!unit.TryGetComponent<Unit>(out _))
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