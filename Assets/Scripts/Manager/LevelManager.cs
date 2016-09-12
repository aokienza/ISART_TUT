using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public delegate void PlayerSpawn(Transform value);
    public event PlayerSpawn OnPlayerSpawn;

    public GameObject playerPrefab;
    public Transform playerSpawnPoint;
    public GameObject shepherd;
    public GameObject sheep;
    public GameObject stage;
    public GameObject[] entrances;

    public int minSheep = 5;
    public int maxSheep = 7;

    int score;
    GameObject playerRef;
    List <AI_Entity> _sheepList;

    // Use this for initialization

    void Awake()
    {
        GameManager.instance.OnGameStart    += NewGame;
        GameManager.instance.OnGamePause    += PauseGame;
        GameManager.instance.OnGameUnPause  += UnPauseGame;
        GameManager.instance.OnGameEnd      += EndGame;
    }

    void Start()
    {
        GameManager.instance.LoadedLevel();
    }

    void NewGame()
    {
        Time.timeScale = 1;
        playerRef = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity) as GameObject;
        playerRef.GetComponent<PlayerController>().OnPlayerDeath += PlayerDead;
        if (OnPlayerSpawn != null)
            OnPlayerSpawn(playerRef.transform);

        _sheepList = new List<AI_Entity>();

        NewWave();
    }

    void EndGame()
    {
        GameManager.instance.OnGameStart    -= NewGame;
        GameManager.instance.OnGamePause    -= PauseGame;
        GameManager.instance.OnGameUnPause  -= UnPauseGame;
        GameManager.instance.OnGameEnd      -= EndGame;

        Scene scene = SceneManager.GetActiveScene();
        //ScoreRecap.instance.SubmitScore(scene.name, score);
        MainMenu.instance.GoToMainMenu();
        //Destroy(gameObject);

    }

    void PlayerDead(Transform player)
    {
        StartCoroutine(DeathAnim());
    }

    IEnumerator DeathAnim()
    {
        playerRef.GetComponent<PlayerController>().OnPlayerDeath -= PlayerDead;

        float timer = 0;
        GameManager.instance.Pause(true);
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
    void Update () {
	
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
        StartCoroutine(nShepherd.GetComponent<AI_Shepherd>().GetOnSpot(getRandomPositionOnStage()));
    }

    void OnSheepDeath(Transform transform)
    {
        AI_Sheep sheepScript = transform.GetComponent<AI_Sheep>();
        if (_sheepList.Contains(sheepScript))
        {
            _sheepList.Remove(sheepScript);
        }

        if(_sheepList.Count <= 0)
        {
            NewWave();
        }
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
}
