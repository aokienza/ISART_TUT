using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField]
    GameObject startMenu;
    [SerializeField]
    GameObject gameMenu;

    public static MainMenu instance = null;


    // Use this for initialization
    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(transform.gameObject);
    }

    public void UnPauseLevel()
    {
        GameManager.instance.Pause(false);
    }

    public void PauseLevel()
    {
        GameManager.instance.Pause(true);
    }

    public void OpenLevel(string LevelName)
    {
        GameManager.instance.LoadScene(LevelName);
        
        if ("MainMenu" == LevelName)
        {
            startMenu.SetActive(true);
            gameMenu.SetActive(false);
        }
    }
}

