using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    int resourceYield;

    public void sendResource(string teamTag)
    {
        CommunicationEvents.GatherResource?.Invoke(teamTag, resourceYield);
    }
}