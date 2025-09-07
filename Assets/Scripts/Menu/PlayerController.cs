using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : BaseController
{
    StoreManager store;
    List<ResourceNode> factionResourceNodes;
    public override void Start()
    {
        base.Start();
        CommunicationEvents.updateUnitCount?.Invoke(ActiveUnits.Count, maxNumberOfTroops);
    }

    public override void addUnitToUnitList(string _faction, Unit unit, bool isOnTopTrack)
    {
        base.addUnitToUnitList(_faction, unit, isOnTopTrack);
      //  CommunicationEvents.updateUnitCount?.Invoke(ActiveUnits.Count, maxNumberOfTroops);
        if (isOnTopTrack)
            UIManager.Instance.AddUnitToTopFloor();
        else
            UIManager.Instance.AddUnitToBottomFloor();

    }

    
    
    
}