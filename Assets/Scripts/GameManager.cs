using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    //text fields
    public TMP_Text numberOfUnitsText;
    public TMP_Text goldAmountText;

    //unit action
    public static int currrentAction = 0; //0 = retreat, 1 = defend, 2 = attack, all units will have the same action and access this every frame probably

    //unit cost
    public int[] unitCosts = new int[3] { 50, 150, 200 }; //0 is mining, 1 is melee, 2 is ranged

    //unit spawning, depending on if unit is spawned on top or bottom choose the correct spawn points in array
    public bool spawnOnTop = true;
    public GameObject[] topSpawnPoints;
    public GameObject[] bottomSpawnPoints;

    //variables
    public int maxNumOfUnits = 25;
    private int currentNumOfUnits = 0;
    
    public int currentGoldAmount = 0; //current gold amount, used to check if we can spawn a unit


    public TMP_Text flipSpawnPointsText; //extra for show, to be removed later
    public void FlipSpawnPoints()
    {
        spawnOnTop = !spawnOnTop;
        flipSpawnPointsText.text = spawnOnTop ? "^\n|" : "|\nv";
        Debug.Log("Spawn points flipped. Now spawning on " + (spawnOnTop ? "top" : "bottom"));
    }


    //used for the buttons
    public void SpawnUnit(int unit) //0 is mining, 1 is melee, 2 is ranged
    {
        //check if we can spawn a new unit
        if (currentNumOfUnits < maxNumOfUnits)
        {
            if (currentGoldAmount >= unitCosts[unit])
            {
                DecreaseCurrentGoldAmount(unitCosts[unit]);

                //increase number of units
                IncreaseCurrentNumOfUnits();

                //check which array of spawn points to use
                GameObject[] spawnPoints = spawnOnTop ? topSpawnPoints : bottomSpawnPoints;
                //choose a random spawn point from the array
                int randomIndex = Random.Range(0, spawnPoints.Length);
                Debug.Log("Spawning unit of type " + (unit == 0 ? "Miner" : unit == 1 ? "Melee" : "Ranged"));
            }
            else
            {
                Debug.Log("Not enough gold to spawn this unit.");
            }
        }
        else
        {
            Debug.Log("Maximum number of units reached.");
        }
    }


    //all player units depend on this, call it only to change the action of all units
    public void ChangeCurrentAction(int action) //0 = retreat, 1 = defend, 2 = attack
    {
        currrentAction = action;
        Debug.Log("Current action changed to: " + (action == 0 ? "Retreat" : action == 1 ? "Defend" : "Attack"));
    }


    private void UpdateCurrentGoldAmount()
    {
        goldAmountText.text = currentGoldAmount.ToString("000");
    }

    public void IncreaseCurrentGoldAmount(int amount)
    {
        currentGoldAmount += amount;
        UpdateCurrentGoldAmount();
    }

    public void DecreaseCurrentGoldAmount(int amount)
    {
        currentGoldAmount -= amount;
        UpdateCurrentGoldAmount();
    }

    /// <summary>
    /// Number of units text updates below
    /// </summary>
    private void UpdateCurrentNumOfUnits()
    {
        numberOfUnitsText.text = currentNumOfUnits.ToString("00") + "/" + maxNumOfUnits;
    }

    public void IncreaseCurrentNumOfUnits()
    {
        currentNumOfUnits++;
        UpdateCurrentNumOfUnits();
    }

    public void DecreaseCurrentNumOfUnits()
    {
        currentNumOfUnits--;
        UpdateCurrentNumOfUnits();
    }

    private void Start()
    {
        // Initialize the number of units text
        UpdateCurrentNumOfUnits();
        UpdateCurrentGoldAmount();
        ChangeCurrentAction(1); // Default action set to defend
    }
}
