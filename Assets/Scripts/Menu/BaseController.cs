using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    [SerializeField] protected string faction;
    protected StoreManager storeManager;
    [SerializeField] protected GameObject topTrack, bottomTrack, selectedTrack, activeResourceNode, spawnTop, spawnBottom;
    protected bool selectedTopTrack = true;
    [SerializeField] protected ResourceNode[] nodes;
    [SerializeField] public int maxNumberOfTroops;
    [SerializeField] protected List<Unit> ActiveUnits;
    [SerializeField] protected List<Transform> waypointsTop, waypointsBottom;

    public virtual void Awake()
    {
        storeManager = gameObject.GetComponent<StoreManager>();//get store component on own GameObject
        ActiveUnits = new List<Unit>();
        CommunicationEvents.AddUnitToFactionList += addUnitToUnitList;
        CommunicationEvents.RemoveUnitFromFactionList += removeUnitFromList;
        CommunicationEvents.SetGathererInfo += addGathererToUnitList;
    }

    public virtual void Start()
    {
        faction = gameObject.tag;
        selectedTrack = topTrack;
        selectedTopTrack = true;
        alternateSelectedTrack(true);
        storeManager = gameObject.GetComponent<StoreManager>();
    }

    public void alternateSelectedTrack(bool top)
    {
        if (top)
        {
            activeResourceNode = nodes[0].gameObject;
            selectedTrack = topTrack;
            selectedTopTrack = true;
        }
        else
        {
            selectedTrack = bottomTrack;
            selectedTopTrack = false;
            activeResourceNode = nodes[1].gameObject;
        }
    }

    public virtual void addUnitToUnitList(string _faction, Unit unit, bool isOnTopTrack)
    {
        if (unit.faction != faction) return;
        ActiveUnits.Add(unit);
        if (isOnTopTrack)
        {
            unit.gameObject.GetComponent<NavigationAgent>().setWaypoints(waypointsTop);
        }
        else
        {
            unit.gameObject.GetComponent<NavigationAgent>().setWaypoints(waypointsBottom);
        }
        
        if (faction != "Player") return;
        CommunicationEvents.updateUI?.Invoke(ActiveUnits.Count, maxNumberOfTroops);
    }

    public void addGathererToUnitList(GatheringUnit unit, bool isOnTopTrack)
    {
        addUnitToUnitList(unit.faction, unit, isOnTopTrack);
        if (unit.faction != faction) return;
        if (isOnTopTrack)
        {
            unit.setCastleAndGatherNode(spawnTop, nodes[0].gameObject);
        }
        else
        {
            unit.setCastleAndGatherNode(spawnBottom, nodes[1].gameObject);
        }
    }

    public virtual void removeUnitFromList(Unit unit)
    {
        ActiveUnits.Remove(unit);
    }

    public void purchaseUnit(int _orderInList)
    {
        bool r = storeManager.trySpawnUnit(_orderInList, selectedTopTrack);
    }
    

}