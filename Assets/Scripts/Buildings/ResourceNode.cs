using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    [SerializeField] int resourceYield;
    [SerializeField] string faction;

    public void sendResource(string teamTag)
    {
        CommunicationEvents.GatherResource?.Invoke(teamTag, resourceYield);
    }
}