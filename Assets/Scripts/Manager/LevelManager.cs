﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour, EventHandler
{

    public static LevelManager instance = null;

    public delegate void PlayerSpawn(Transform value);
    public event PlayerSpawn OnPlayerSpawn;

    public GameObject playerPrefab;
    public Transform playerSpawnPoint;
    public GameObject shepherd;
    public GameObject sheep;
    public GameObject stage;
    public TopCamera _camera;
    public GameObject[] entrances;
    bool soundPlayed;

    Text UISheepCount;
    Text UIShepherdCount;
    Text UIIGScore;
    int shepherdNumber;
    [SerializeField]
    GameObject GameOverButton;


    public AudioClip OnCoughtSound;
    public AudioClip Gloups;
    protected AudioSource _audioSource;

    bool hasLost = false;

    public int minSheep = 5;
    public int maxSheep = 7;
    ScoreRecap scoreObject;
    public int score;
    GameObject playerRef;
    List <AI_Entity> _sheepList;
    public GameObject loadingScreen;
    public GameObject UI;

    // Use this for initialization

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        UI = GameObject.Find("UI");
        scoreObject = UI.GetComponent<ScoreRecap>();
        scoreObject.Manager = this;

        GameManager.instance.OnGameStart    += NewGame;
        GameManager.instance.OnGamePause    += PauseGame;
        GameManager.instance.OnGameUnPause  += UnPauseGame;
        GameManager.instance.OnGameEnd      += EndGame;

        _audioSource = GetComponent<AudioSource>();

        _camera = (TopCamera)FindObjectOfType(typeof(TopCamera));

        UISheepCount = GameObject.Find("UISheepCount").GetComponent<Text>();
        UIShepherdCount = GameObject.Find("UIShepherdCount").GetComponent<Text>();
        UIIGScore = GameObject.Find("UIIGScore").GetComponent<Text>();
        GameOverButton = UI.GetComponent<MainMenu>().GameOverButton;
        loadingScreen = UI.GetComponent<MainMenu>().LoadingScreen;

        GameOverButton.SetActive(false);
        loadingScreen.SetActive(false);
        UI.GetComponent<MainMenu>().INPUTS = GameObject.Find("[Manager]").GetComponent<InputHandler>();
    }

    public void PlayerCought()
    {
        if (hasLost)
            return;

        hasLost = true;
        _audioSource.clip = OnCoughtSound;
        if (!soundPlayed)
        {
            _audioSource.Play();
            soundPlayed = true;
        }
        

        playerRef.GetComponent<PlayerController>().enabled = false;
        StartCoroutine(DeathAnim());
    }

    void NewGame()
    {
        hasLost = false;
        Time.timeScale = 1;
        playerRef = (GameObject)Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);

        shepherdNumber = 0;
        UIIGScore.text = "0";

        if (OnPlayerSpawn != null)
        {
            OnPlayerSpawn(playerRef.transform);
        }

        _sheepList = new List<AI_Entity>();

        NewWave();
    }

    void EndGame()
    {
        hasLost = true;
        GameOverButton.SetActive(true);
        scoreObject.CallScores(score);
    }

    public void GoBackToMenu()
    {
        MainMenu.instance.OpenLevel("MainMenu");
    }

    IEnumerator DeathAnim()
    {
        float timer = 0;
        while (timer < 2.0f)
        {
            timer += Time.unscaledDeltaTime;
            yield return null;
        }
        EndGame();
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }

    void UnPauseGame()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update ()
    {
    }

    void NewWave()
    {
        GameObject entrance = entrances[Random.Range(0, 3)];
        entrance.GetComponent<Entrance>().Use();
        int sheepNumber = Random.Range(minSheep, maxSheep);
        for(var i = 0; i < sheepNumber; i++)
        {
            GameObject nSheep = Instantiate(sheep, entrance.transform.position, Quaternion.identity) as GameObject;
            AI_Sheep sheepScript = nSheep.GetComponent<AI_Sheep>();
            StartCoroutine(sheepScript.GetOnSpot(getRandomPositionOnStage()));
            _sheepList.Add(sheepScript);
            sheepScript.OnDeath += OnSheepDeath;
        }

        GameObject nShepherd = Instantiate(shepherd, entrance.transform.position, Quaternion.identity) as GameObject;
        shepherdNumber++;
        UIShepherdCount.text = shepherdNumber.ToString();
        StartCoroutine(scoreObject.FontEffect(UIShepherdCount,89));
        UISheepCount.text = _sheepList.Count.ToString();
        StartCoroutine(scoreObject.FontEffect(UISheepCount,89));
        StartCoroutine(nShepherd.GetComponent<AI_Shepherd>().GetOnSpot(getRandomPositionOnStage()));
    }

    void OnSheepDeath(Transform transform)
    {
        AI_Sheep sheepScript = transform.GetComponent<AI_Sheep>();
        sheepScript.OnDeath -= OnSheepDeath;

        if (_sheepList.Contains(sheepScript))
        {
            score += 100;
            _sheepList.Remove(sheepScript);
        }

        if(_sheepList.Count <= 0)
        {
            score += 200;
            NewWave();
        }
        _audioSource.clip = Gloups;
        _audioSource.Play();
        Debug.Log("Played Gloups");
        sheepScript.Unregister();
        Destroy(sheepScript.gameObject);

        Vibration.Vibrate(30);
        _camera.Shake(2f, 0.3f);

        UIIGScore.text = score.ToString();
        StartCoroutine(scoreObject.FontEffect(UIIGScore, 89));
        UISheepCount.text = _sheepList.Count.ToString();
        StartCoroutine(scoreObject.FontEffect(UISheepCount,89));
    }

    public Vector3 getRandomPositionOnStage()
    {
        Mesh planeMesh = stage.GetComponent<MeshFilter>().mesh;
        Bounds bounds = planeMesh.bounds;

        float boundX = (stage.transform.localScale.x * bounds.size.x) * 0.3f;
        float boundZ = (stage.transform.localScale.z * bounds.size.z) * 0.3f;
        Vector3 minPosition = new Vector3(stage.transform.position.x - boundX, 0, stage.transform.position.z - boundZ);
        Vector3 maxPosition = new Vector3(stage.transform.position.x + boundX, 0, stage.transform.position.z + boundZ);
        return new Vector3(Random.Range(minPosition.x, maxPosition.x), stage.transform.position.y + 0.5f, Random.Range(minPosition.z, maxPosition.z));
    }

    public Vector3 getRandomPositionOnStageCloseTo(Vector3 position, float distance)
    {
        Mesh planeMesh = stage.GetComponent<MeshFilter>().mesh;
        Bounds bounds = planeMesh.bounds;

        float boundX = (stage.transform.localScale.x * bounds.size.x) * 0.3f;
        float boundZ = (stage.transform.localScale.z * bounds.size.z) * 0.3f;
        Vector3 minPosition = new Vector3(stage.transform.position.x - boundX, 0, stage.transform.position.z - boundZ);
        Vector3 maxPosition = new Vector3(stage.transform.position.x + boundX, 0, stage.transform.position.z + boundZ);


        return new Vector3(Random.Range((position.x + minPosition.x), position.x + maxPosition.x), 
            stage.transform.position.y + 0.5f, 
                Random.Range(position.z + minPosition.z, position.z + maxPosition.z)
                );


    }

    public void OnDestroy()
    {
        Unregister();
    }

    public void Unregister()
    {
        GameManager.instance.OnGameStart -= NewGame;
        GameManager.instance.OnGamePause -= PauseGame;
        GameManager.instance.OnGameUnPause -= UnPauseGame;
        GameManager.instance.OnGameEnd -= EndGame;
    }
}

