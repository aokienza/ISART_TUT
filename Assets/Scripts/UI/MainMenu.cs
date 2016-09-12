using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    [SerializeField]
    GameObject startMenu;
    [SerializeField]
    GameObject mainMenu;
    [SerializeField]
    GameObject GameScreen;

    public static MainMenu instance = null;


    // Use this for initialization
    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
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
    }

    public void GoToMainMenu()
    {
        GameManager.instance.LoadScene("MainMenu");
        mainMenu.SetActive(true);
        GameScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
