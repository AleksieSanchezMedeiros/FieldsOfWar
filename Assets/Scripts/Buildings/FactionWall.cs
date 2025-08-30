using UnityEngine;

public class FactionWall : Building
{
    //protected List<GameObject> allies = new List<GameObject>();
    public void OnAllyCreated(GameObject obj)
    {
        if (obj.CompareTag(tag))
        {
            //allies.Add(obj);
            Physics.IgnoreCollision(obj.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }
}
