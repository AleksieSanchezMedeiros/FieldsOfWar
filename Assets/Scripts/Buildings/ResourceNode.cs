using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    [SerializeField] float resourceYield;
    [SerializeField] string faction;

    public void sendResource(string teamTag)
    {
        Debug.Log("Plata");
        CommunicationEvents.GatherResource?.Invoke(teamTag, resourceYield);
    }
}