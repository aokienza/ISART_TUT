using UnityEngine;
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
    public GameObject[] entrances;


    Text UISheepCount;
    Text UIShepherdCount;
    int shepherdNumber;

    public int minSheep = 5;
    public int maxSheep = 7;
    ScoreRecap scoreObject;
    public int score;
    GameObject playerRef;
    List <AI_Entity> _sheepList;

    // Use this for initialization

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        scoreObject = GameObject.Find("UI").GetComponent<ScoreRecap>();

        GameManager.instance.OnGameStart    += NewGame;
        GameManager.instance.OnGamePause    += PauseGame;
        GameManager.instance.OnGameUnPause  += UnPauseGame;
        GameManager.instance.OnGameEnd      += EndGame;


        shepherdNumber = 0;
        UISheepCount = GameObject.Find("UISheepCount").GetComponent<Text>();
        UIShepherdCount = GameObject.Find("UIShepherdCount").GetComponent<Text>();
    }

    public void PlayerCought()
    {
        playerRef.GetComponent<PlayerController>().enabled = false;
        StartCoroutine(DeathAnim());
    }

    void NewGame()
    {
        Time.timeScale = 1;
        playerRef = (GameObject)Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);

        if (OnPlayerSpawn != null)
        {
            OnPlayerSpawn(playerRef.transform);
        }

        _sheepList = new List<AI_Entity>();

        NewWave();
    }

    void EndGame()
    {
        scoreObject.RetrieveBestScore();
        scoreObject.SubmitScore(SceneManager.GetActiveScene().name,score);
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
        UISheepCount.text = _sheepList.Count.ToString();
        StartCoroutine(nShepherd.GetComponent<AI_Shepherd>().GetOnSpot(getRandomPositionOnStage()));
    }

    void OnSheepDeath(Transform transform)
    {
        AI_Sheep sheepScript = transform.GetComponent<AI_Sheep>();
        sheepScript.OnDeath -= OnSheepDeath;

        if (_sheepList.Contains(sheepScript))
        {
            _sheepList.Remove(sheepScript);
        }

        if(_sheepList.Count <= 0)
        {
            NewWave();
        }
        sheepScript.Unregister();
        Destroy(sheepScript.gameObject);

        UISheepCount.text = _sheepList.Count.ToString();
    }

    Vector3 getRandomPositionOnStage()
    {
        Mesh planeMesh = stage.GetComponent<MeshFilter>().mesh;
        Bounds bounds = planeMesh.bounds;

        float boundX = (stage.transform.localScale.x * bounds.size.x) * 0.5f;
        float boundZ = (stage.transform.localScale.z * bounds.size.z) * 0.5f;
        Vector3 minPosition = new Vector3(stage.transform.position.x - boundX, stage.transform.position.y + 0.5f, stage.transform.position.z - boundZ);
        Vector3 maxPosition = new Vector3(stage.transform.position.x + boundX, stage.transform.position.y + 0.5f, stage.transform.position.z + boundZ);
        return new Vector3(Random.Range(minPosition.x, maxPosition.x), stage.transform.position.y + 0.5f, Random.Range(minPosition.z, maxPosition.z));
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

