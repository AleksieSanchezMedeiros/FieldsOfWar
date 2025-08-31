using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] sceneCodes _controlScene, activeScene, nextScene, mainMenu;
    public sceneCodes[] scenes;

    public static LevelManager Instance;
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
    }

    public void changeScene(string sceneName)
    {
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
            if (scenes[i].getName() == activeScene.getName() && i != scenes.Length)
            {
                nextCode = scenes[i].getName();
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