using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class LocalLevelManager : MonoBehaviour
{
    [SerializeField] GameObject victoryBanner, defeatBanner, gameEndUI, underlevel, timer;
    bool timerMode = false;
    [SerializeField] float timeToDefeat;
    

    void Awake()
    {
        timerMode = CommunicationEvents.getMode();
        if (!timerMode)
        {
            CommunicationEvents.onFactionDefeated += gameOver;
            underlevel.SetActive(true);
        }
        else
        {
            timer.SetActive(true);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void FixedUpdate()
    {
        if (timerMode)
        {
            timeToDefeat -= Time.deltaTime;
        }

        if (timeToDefeat <= 0)
        {
            gameOver("Player");
        }
    }

    void gameOver(string _faction) // _faction = the defeated faction
    {
        gameEndUI.SetActive(true);
        Time.timeScale = 0f;
        if (_faction == "Player")
        {
            defeatBanner.SetActive(true);
            victoryBanner.SetActive(false);
        }
        else
        {
            victoryBanner.SetActive(true);
            defeatBanner.SetActive(false);
        }
    }
}
