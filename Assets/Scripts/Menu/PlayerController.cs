using System.Collections.Generic;
using UnityEngine;

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
        CommunicationEvents.updateUnitCount?.Invoke(ActiveUnits.Count, maxNumberOfTroops);
    }

    // i tried to see if that's what causes it not spawn  from bottom... if it doesn’t   help you can delete it - Shir
    // if you delete it - something else needs to be associated with the arrow UI
    public void FlipSpawnViaGameManager()
    {
        FindObjectOfType<GameManager>().FlipSpawnPoints();
    }
}