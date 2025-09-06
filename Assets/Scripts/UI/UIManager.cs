using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    //playerManagerReference PMR; //This is a stand in for a class being worked on, final names won't necesarily reflect what's written here.
    //storeManagerReference SMR; //This is a stand in for a class being worked on, final names won't necesarily reflect what's written here.
    public TMP_Text numberOfUnitsText;
    public TMP_Text goldAmountText;
    //public GameManager gameManager;
    public static UIManager Instance;
    AudioSource audioSource;
    AudioClip music;
    bool paused = false;
    LevelManager levelManager;
    //
    private bool isTopTrackSelected = true;
    public CommandsGroup commandsGroup;
    void instantiate()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Awake()
    {
        instantiate();
        audioSource = GetComponent<AudioSource>();
        levelManager = LevelManager.Instance;
        CommunicationEvents.updateFunds += UpdateCurrentGoldAmount;
        CommunicationEvents.updateUnitCount += UpdateCurrentNumOfUnits;
        audioSource.loop = true;
        audioSource.clip = music;
        audioSource.Play();
    }

    void Start()
    {
        //PMR = getComponent<playerManagerReference>();
        //SMR = getComponent<storeManagerReference>();
        //subscribe UpdateCurrentGoldAmount and UpdateCurrentNumberOfUnits to event that will tell them what to display
    }

    private void UpdateCurrentGoldAmount(int currentGoldAmount)
    {
        string currentGoldAmountStr = currentGoldAmount.ToString();
        goldAmountText.text = currentGoldAmountStr;
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
        //GameManager.currrentAction = 2;
       // CommunicationEvents.setUnitOrders?.Invoke(2, true, "Player");
       CommunicationEvents.setUnitOrders?.Invoke(2, isTopTrackSelected, "Player");
    }
    public void DefendCommand()
    {
        //  GameManager.currrentAction = 1;

        //CommunicationEvents.setUnitOrders?.Invoke(1, true, "Player");

        CommunicationEvents.setUnitOrders?.Invoke(1, isTopTrackSelected, "Player");

    }

    public void RetreatCommand()
    {
       // GameManager.currrentAction = 0;
      //  CommunicationEvents.setUnitOrders?.Invoke(0, true, "Player");
        CommunicationEvents.setUnitOrders?.Invoke(0, isTopTrackSelected, "Player");

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

    public void pause()
    {
        if (paused)
        {
            Time.timeScale = 1f;
            return;
        }
        Time.timeScale = 0f;
    }

    public void retryLevel()
    {
        
    }

    public void goToNextLevel()
    {

    }

    public void gotToMainMenu()
    {
        
    }
    
    //

    public void SelectTopTrack()
    {
        isTopTrackSelected = true; 
        commandsGroup.ResetCommandsVisual();

    }

    public void SelectBottomTrack()
    {
        isTopTrackSelected = false;
        commandsGroup.ResetCommandsVisual();

    }
    
    
}
