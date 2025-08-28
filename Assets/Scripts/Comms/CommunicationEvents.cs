using System;
using UnityEngine;

public class CommunicationEvents : ScriptableObject
{
    public static Action<string, int> GatherResource;
    public static Action<string, Unit> AddUnitToFactionList;
    public static Action<Unit> RemoveUnitFromFactionList;
    public static Action<int, int> updateUI; //currentUnitCount, maxUnitCount
    public static Action<string> onFactionDefeated;

    static bool modeIsTimer;
    public static void setMode(bool mode) { modeIsTimer = mode; }
    public static bool getMode() { return modeIsTimer; }
}