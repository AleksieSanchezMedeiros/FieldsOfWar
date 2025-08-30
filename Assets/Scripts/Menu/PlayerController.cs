using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    StoreManager store;
    List<ResourceNode> factionResourceNodes;
    void Awake()
    {

    }

    void Start()
    {        
        CommunicationEvents.updateUnitCount?.Invoke(ActiveUnits.Count, maxNumberOfTroops);
    }

    public override void addUnitToUnitList(string _faction, Unit unit, bool isOnTopTrack)
    {
        base.addUnitToUnitList(_faction, unit, isOnTopTrack);
        CommunicationEvents.updateUnitCount?.Invoke(ActiveUnits.Count, maxNumberOfTroops);
    }

}