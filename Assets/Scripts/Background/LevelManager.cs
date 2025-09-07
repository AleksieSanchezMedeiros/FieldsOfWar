using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    [SerializeField] public sceneCodes _controlScene, activeScene, nextScene;
    [SerializeField] sceneCodes[] scenes = new sceneCodes[5];
    public static LevelManager Instance;
    [SerializeField] Canvas pauseScreen;
    [SerializeField] Camera mainMenuCamera;
    [SerializeField] GameObject mainMenu, instructions, nextInstructionSlideBtn, prevInstructionSlideBtn, eventSystem;
    [SerializeField] Image[] instructionSlides;
    bool paused, showingSlides = false;
    [SerializeField]int currentlyActiveInstructionSlide = 0;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
        scenes = new sceneCodes[5];
        Debug.Log(scenes);

        for (int i = 0; i < scenes.Length; i++)
        {
            string pathToScene = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(pathToScene);
            if (sceneName.Length <= 0)
            {
                return;
            }
            else
            {
                scenes[i] = new sceneCodes(i, sceneName);
            }
        }

        _controlScene = scenes[0];
        mainMenu.gameObject.SetActive(true);

        CommunicationEvents.onFactionDefeated += pauseUnpause;
        CommunicationEvents.onLoadMainMenu += loadMainMenu;
        CommunicationEvents.onReloadLevel += reloadLevel;
        CommunicationEvents.onLoadNextLevel += moveToNextScene;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && activeScene.getName() != "Background")
        {
            pauseUnpause();
        }
    }

    #region Local level and pause menu management
    public void pauseUnpause()
    {
        if (paused)
        {
            paused = false;
            Time.timeScale = 1;
            pauseScreen.gameObject.SetActive(false);
        }
        else
        {
            paused = true;
            Time.timeScale = 0;
            pauseScreen.gameObject.SetActive(true);
        }
        showInstructionSlides();
    }

    public void pauseUnpause(string a)
    {
        pauseUnpause();
    }

    public void showInstructionSlides()
    {
        if (!showingSlides)
        {
            instructions.SetActive(true);
            currentlyActiveInstructionSlide = 0;
            showingSlides = !showingSlides;
            foreach (Image slide in instructionSlides)
            {
                slide.gameObject.SetActive(false);
            }
            checkSlidesindex();
        }
        else
        {
            instructions.SetActive(false);
            currentlyActiveInstructionSlide = 0;
            showingSlides = !showingSlides;
            checkSlidesindex();
        }
    }

    public void nextInstructionSlide()
    {
        instructionSlides[currentlyActiveInstructionSlide].gameObject.SetActive(false);
        currentlyActiveInstructionSlide++;
        checkSlidesindex();

    }

    public void prevInstructionSlide()
    {
        instructionSlides[currentlyActiveInstructionSlide].gameObject.SetActive(false);
        currentlyActiveInstructionSlide--;
        checkSlidesindex();
    }

    void checkSlidesindex()
    {
        instructionSlides[currentlyActiveInstructionSlide].gameObject.SetActive(true);
        switch (currentlyActiveInstructionSlide)
        {
            case 0:
                nextInstructionSlideBtn.gameObject.SetActive(true);
                prevInstructionSlideBtn.gameObject.SetActive(false);
                break;
            case 1:
                nextInstructionSlideBtn.gameObject.SetActive(true);
                prevInstructionSlideBtn.gameObject.SetActive(true);
                break;
            case 2:

                nextInstructionSlideBtn.gameObject.SetActive(false);
                prevInstructionSlideBtn.gameObject.SetActive(true);
                break;
        }
    }
    #endregion
    #region Scene management
    public void changeScene(string sceneName)
    {
        if (mainMenuCamera.gameObject.activeSelf)
        {
            unloadMainMenu();
        }
        if (sceneName == "Background")
        {
            loadMainMenu();
        }
        for (int i = 1; i < scenes.Length; i++)
        {
            if (scenes[i].getName() == sceneName)
            {
                nextScene = scenes[i];
                if (activeScene != null && activeScene.getName() != "Background")
                {
                    SceneManager.UnloadSceneAsync(activeScene.getCode());
                }
                SceneManager.LoadSceneAsync(nextScene.getCode(), LoadSceneMode.Additive);
                activeScene = nextScene;
            }
        }
    }

    public void moveToNextScene()
    {
        string nextCode = _controlScene.getName();
        for (int i = 1; i < scenes.Length; i++)
        {
            if (scenes[i].getName() == activeScene.getName() && i != scenes.Length - 1)
            {
                nextCode = scenes[i++].getName();
            }
            else
            {
                nextCode = "Background";
            }
        }
        changeScene(nextCode);
    }

    public void firstLevel()
    {
        unloadMainMenu();
        showingSlides = false;
        changeScene(scenes[1].getName());
    }

    public void loadMainMenu()
    {
        SceneManager.UnloadSceneAsync(activeScene.getName());
        eventSystem.SetActive(true);
        mainMenu.gameObject.SetActive(true);
        mainMenuCamera.gameObject.SetActive(true);
    }

    void unloadMainMenu()
    {
        eventSystem.SetActive(false);
        mainMenu.gameObject.SetActive(false);
        mainMenuCamera.gameObject.SetActive(false);
    }
    public void reloadLevel()
    {
        SceneManager.LoadScene(activeScene.getName());
    }

    public void exitGame()
    {
        Application.Quit();
    }
    #endregion
}

public class sceneCodes{
    int code;
    string sceneName;

    public sceneCodes(int _code, string _sceneName){
        this.code = _code;
        sceneName = _sceneName;
    }

    public string getName(){
        return sceneName;
    }

    public int getCode(){
        return code;
    }
}