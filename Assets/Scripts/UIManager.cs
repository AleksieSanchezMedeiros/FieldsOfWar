using TMPro;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    //playerManagerReference PMR; //This is a stand in for a class being worked on, final names won't necesarily reflect what's written here.
    //storeManagerReference SMR; //This is a stand in for a class being worked on, final names won't necesarily reflect what's written here.
    public TMP_Text numberOfUnitsText;
    public TMP_Text goldAmountText;

    void Start()
    {
        //PMR = getComponent<playerManagerReference>();
        //SMR = getComponent<storeManagerReference>();
        //subscribe UpdateCurrentGoldAmount and UpdateCurrentNumberOfUnits to event that will tell them what to display
    }

    private void UpdateCurrentGoldAmount(int currentGoldAmount)
    {
        goldAmountText.text = currentGoldAmount.ToString("000");
    }

    private void UpdateCurrentNumOfUnits(int currentNumOfUnits, int maxNumOfUnits)
    {
        numberOfUnitsText.text = currentNumOfUnits.ToString("00") + "/" + maxNumOfUnits;
    }

    public void issueCommand() // 0: Advance; 1: Halt; 2: Retreat;
    {
        //send out an event to all subscriptors with the selected command
    }

    public void alternateSpawnPoint()
    {
        //PMR.alternateSpawn();
    }

    public void purchaseUnit(int placeInList)
    {
        //
    }
}