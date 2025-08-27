using System;
using TMPro;
using UnityEngine;

public class CommunicationEvents : ScriptableObject
{
    public static Action<string, int> GatherResource;
    public static Action<string, Unit> AddUnitToFactionList;
    public static Action<Unit> RemoveUnitFromFactionList;

    public static Action<int, int> updateUI; //currentUnitCount, maxUnitCount
}