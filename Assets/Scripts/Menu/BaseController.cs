using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    string faction;
    protected StoreManager storeManager;
    [SerializeField] protected GameObject topTrack, bottomTrack, selectedTrack;
    [SerializeField] protected ResourceNode[] nodes;
    [SerializeField] int maxNumberOfTroops;
    [SerializeField] protected List<Unit> ActiveUnits;

    void Start()
    {
        faction = gameObject.tag;
        storeManager = GetComponent<StoreManager>();//get store component on own GameObject
        CommunicationEvents.AddUnitToFactionList += addUnitToUnitList;
        CommunicationEvents.RemoveUnitFromFactionList += removeUnitFromList;
    }

    public void orderAdvance() { }
    public void orderStay() { }
    public void orderRetreat() { }

    public void alternateSelectedTrack(bool top)
    {
        if (top)
        {
            selectedTrack = topTrack;
        }
        else
        {
            selectedTrack = bottomTrack;
        }
    }

    public virtual void addUnitToUnitList(string _faction, Unit unit)
    {
        if (faction == _faction)
        {
            ActiveUnits.Add(unit);
            if (unit is GatheringUnit)
            {
                ((GatheringUnit)unit).setGatherNode(selectedTrack);
            }
        }

        CommunicationEvents.updateUI?.Invoke(ActiveUnits.Count, maxNumberOfTroops);
    }

    public virtual void removeUnitFromList(Unit unit)
    {
        ActiveUnits.Remove(unit);
    }

    public void purchaseUnit(int _orderInList)
    {
        storeManager.trySpawnUnit(_orderInList, selectedTrack);
    }
}