using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public delegate void GameStart();
    public event GameStart OnGameStart;
    public delegate void GamePause();
    public event GamePause OnGamePause;
    public delegate void GameUnPause();
    public event GamePause OnGameUnPause;
    public delegate void GameEnd();
    public event GameEnd OnGameEnd;

    void Awake()
    {
        DontDestroyOnLoad(this);

        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
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

    public void LoadedLevel()
    {
        StartGame();
    }
}
