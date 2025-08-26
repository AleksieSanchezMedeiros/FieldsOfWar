using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    StoreManager storeManager;

    void Start()
    {
        storeManager = GetComponent<StoreManager>();
    }

    public void orderAdvance() { }
    public void orderStay() { }
    public void orderRetreat() { }

    public void purchaseUnit(int _orderInList)
    {
        
    }
}