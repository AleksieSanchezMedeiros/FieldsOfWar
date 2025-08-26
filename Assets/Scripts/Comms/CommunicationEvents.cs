using System;
using UnityEngine;

public class CommunicationEvents : ScriptableObject
{
    public static Action<string, int> GatherResource;
}