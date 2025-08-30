using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    [SerializeField] string faction;
    protected StoreManager storeManager;
    [SerializeField] protected GameObject topTrack, bottomTrack, selectedTrack, activeResourceNode, castle;
    bool selectedTopTrack = true;
    [SerializeField] protected ResourceNode[] nodes;
    [SerializeField] public int maxNumberOfTroops;
    [SerializeField] protected List<Unit> ActiveUnits;

    void Awake()
    {
        storeManager = gameObject.GetComponent<StoreManager>();//get store component on own GameObject
        ActiveUnits = new List<Unit>();
    }

    public virtual void Start()
    {
        faction = gameObject.tag;
        selectedTrack = topTrack;
        selectedTopTrack = true;
        CommunicationEvents.AddUnitToFactionList += addUnitToUnitList;
        CommunicationEvents.RemoveUnitFromFactionList += removeUnitFromList;
        alternateSelectedTrack(true);
    }

    public void orderAdvance() { }
    public void orderStay() { }
    public void orderRetreat() { }

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
        Debug.Log($"{_faction},  {faction}");
        if (faction == _faction)
        {
            ActiveUnits.Add(unit);
            if (unit is GatheringUnit)
            {
                ((GatheringUnit)unit).setCastleAndGatherNode(castle, activeResourceNode);
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
        storeManager = gameObject.GetComponent<StoreManager>();
        Debug.Log($"{_orderInList} {storeManager.gameObject}");
        bool r = storeManager.trySpawnUnit(_orderInList, selectedTopTrack);
    }
}