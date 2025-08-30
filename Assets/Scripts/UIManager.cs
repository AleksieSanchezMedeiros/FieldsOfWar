using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //playerManagerReference PMR; //This is a stand in for a class being worked on, final names won't necesarily reflect what's written here.
    //storeManagerReference SMR; //This is a stand in for a class being worked on, final names won't necesarily reflect what's written here.
    public TMP_Text numberOfUnitsText;
    public TMP_Text goldAmountText;
    //public GameManager gameManager;

    void Awake()
    {
        CommunicationEvents.updateFunds += UpdateCurrentGoldAmount;
        CommunicationEvents.updateUnitCount += UpdateCurrentNumOfUnits;
    }

    void Start()
    {
        //PMR = getComponent<playerManagerReference>();
        //SMR = getComponent<storeManagerReference>();
        //subscribe UpdateCurrentGoldAmount and UpdateCurrentNumberOfUnits to event that will tell them what to display
    }

    private void UpdateCurrentGoldAmount(int currentGoldAmount)
    {
        Debug.Log(currentGoldAmount);
        goldAmountText.text = currentGoldAmount.ToString();
    }

    private void UpdateCurrentNumOfUnits(int currentNumOfUnits, int maxNumOfUnits)
    {
        numberOfUnitsText.text = currentNumOfUnits.ToString("00") + "/" + maxNumOfUnits;
    }

   // public void issueCommand() // 0: Advance; 1: Halt; 2: Retreat;
   // {
        //send out an event to all subscriptors with the selected command
   // }
   
    //0 = retreat, 1 = defend, 2 = attack
    public void AttackCommand()
    {
        GameManager.currrentAction = 2;
    } 
    public void DefendCommand()
    {
        GameManager.currrentAction = 1;
    }

    public void RetreatCommand()
    {
        GameManager.currrentAction = 0;
    }

    public void alternateSpawnPoint()
    {
        //PMR.alternateSpawn();
    }

    public void purchaseUnit(int placeInList)
    {
        //
    }
    public TMP_Text timerText;

    public void UpdateTimerDisplay(float timeLeft)
    {
        timerText.text = Mathf.Ceil(timeLeft).ToString("00") + "s";
    }

}
