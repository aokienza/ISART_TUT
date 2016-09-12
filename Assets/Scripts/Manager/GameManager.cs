using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public GameObject UI;

    public delegate void GameStart();
    public event GameStart OnGameStart;
    public delegate void GamePause();
    public event GamePause OnGamePause;
    public delegate void GameUnPause();
    public event GamePause OnGameUnPause;
    public delegate void GameEnd();
    public event GameEnd OnGameEnd;

    protected static bool UISpawned = false;
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);

        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        if (!UISpawned)
        {
            Instantiate(UI, Vector3.zero, Quaternion.identity);
            UISpawned = true;
        }
    }

    public void StartGame()
    {
        if (OnGameStart != null)
            OnGameStart(); 
    }

    public void Pause(bool value)
    {
        if (value)
        {
            if (OnGamePause != null)
                OnGamePause();
        }
        else
        {
            if (OnGameUnPause != null)
                OnGameUnPause();
        }
    }

    public void EndGame()
    {
        if (OnGameEnd != null)
            OnGameEnd();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        StartGame();
    }

    public void LoadScene(string LevelName)
    {
        SceneManager.LoadScene(LevelName);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
}
