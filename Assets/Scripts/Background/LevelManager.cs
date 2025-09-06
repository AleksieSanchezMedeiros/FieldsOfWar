using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelManager : MonoBehaviour
{
    [SerializeField] sceneCodes _controlScene, activeScene, nextScene, mainMenu;
    [SerializeField] sceneCodes[] scenes;
    public static LevelManager Instance;
    [SerializeField] Canvas pauseScreen;
    [SerializeField] Camera mainMenuCamera;
    bool paused;
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
        mainMenu = scenes[1];
        if (activeScene != scenes[1])
        {
            changeScene("Main Menu");
        }
        
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.P) && activeScene.getName() != "Main Menu")
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
        }
    }

    public void changeScene(string sceneName)
    {
        if(sceneName == "Background")
        for (int i = 2; i < scenes.Length; i++)
        {
            if (scenes[i].getName() == sceneName)
            {
                nextScene = scenes[i];
                if (activeScene != null)
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
        string nextCode = mainMenu.getName();
        for (int i = 2; i < scenes.Length; i++)
        {
            if (scenes[i].getName() == activeScene.getName() && i != scenes.Length - 1)
            {
                nextCode = scenes[i++].getName();
            }
            else
            {
                nextCode = "Main Menu";
            }
        }
        changeScene(nextCode);
    }

    public void loadMainMenu()
    {
        changeScene(mainMenu.getName());
    }
    public void reloadLevel()
    {
        SceneManager.LoadScene(activeScene.getName());
    }
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