using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
public class LocalLevelManager : MonoBehaviour
{
    [SerializeField] GameObject victoryBanner, defeatBanner, gameEndUI, timer, nextLevel;
    TextMeshPro timerText;
    bool suddenDeath = false;
    [SerializeField] float timeToDefeat;
    [SerializeField] int totalUnitsOnBottomTrack;
    [SerializeField] int unitsThatAreAboutToDie;

    void Awake()
    {
        timerText = timer.GetComponent<TextMeshPro>();
        CommunicationEvents.onFactionDefeated += onFactionDefeated;
    }

    void onFactionDefeated(string faction)
    {
        if (faction == "Player")
        {
            gameEndUI.SetActive(true);
            defeatBanner.SetActive(true);
        }
    }

    public void onNextLevelPressed()
    {
        CommunicationEvents.onLoadNextLevel?.Invoke();
    }

    public void onBackToMainMenuPressed()
    {
        CommunicationEvents.onLoadMainMenu?.Invoke();
    }

    public void onRetryPressed()
    {
        CommunicationEvents.onReloadLevel?.Invoke();
    }

    void FixedUpdate()
    {
        if (suddenDeath)
        {
            timeToDefeat -= Time.deltaTime;
        }

        if (timeToDefeat <= 0)
        {
            gameOver("Player");
        }
    }

    void updateUnitsOnBottomTrack(bool increase)
    {
        if (suddenDeath) return;
        if (increase)
        {
            totalUnitsOnBottomTrack++;
            if (totalUnitsOnBottomTrack == unitsThatAreAboutToDie)
            {
                suddenDeath = true;
            }
        }
        else
        {
            totalUnitsOnBottomTrack--;
        }
    }

    void updateTimer()
    {
        string mins = (timeToDefeat / 60).ToString();
        string seg = (timeToDefeat % 60).ToString();
        timerText.text = timeToDefeat.ToString($"{mins}:{seg}");
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
