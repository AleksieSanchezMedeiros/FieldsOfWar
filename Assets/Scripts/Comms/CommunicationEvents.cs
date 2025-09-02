using System;
using UnityEngine;

public class CommunicationEvents : ScriptableObject
{
    public static Action<string, int> GatherResource;
    public static Action<string, Unit, bool> AddUnitToFactionList; //faction, unit, isOnTopTrack
    public static Action<GatheringUnit, bool> SetGathererInfo;
    public static Action<Unit> RemoveUnitFromFactionList;
    public static Action<int, int> updateUI, updateUnitCount; //currentUnitCount, maxUnitCount
    public static Action<string> onFactionDefeated;
    public static Action<int, bool> moveTowardsTarget; //0: retreat/1: hold/2: Advance, is on top track
    public static Action<int> updateFunds;
    public static Action<int, bool, string> setUnitOrders; //Command, isOnTop, faction
    static bool modeIsTimer;
    public static void setMode(bool mode) { modeIsTimer = mode; }
    public static bool getMode() { return modeIsTimer; }
}