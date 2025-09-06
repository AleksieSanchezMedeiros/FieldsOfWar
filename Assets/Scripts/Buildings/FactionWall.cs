using System;
using UnityEngine;

public class FactionWall : Building
{
    //protected List<GameObject> allies = new List<GameObject>();

    protected override void Awake()
    {
        CommunicationEvents.AddUnitToFactionList += OnAllyCreated;
    }
    public void OnAllyCreated(string _faction, Unit u, bool top)
    {
        if (u.CompareTag(tag))
        {
            //allies.Add(obj);
            Physics.IgnoreCollision(u.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }
}
