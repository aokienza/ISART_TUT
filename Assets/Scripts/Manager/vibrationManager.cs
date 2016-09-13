using UnityEngine;
using System.Collections;


public class vibrationManager : MonoBehaviour {

    GameObject _Player;
    GameObject _Shepherd;

    float distance;

    public float LEVEL1_range = 45;
    public float LEVEL2_range = 35;
    public float LEVEL3_range = 25;
    public float LEVEL4_range = 15;
    public float LEVEL5_range = 8;

    bool isRunning = false;


    void Start()
    {
        //if (!SystemInfo.supportsVibration) Destroy(gameObject);
        _Player = GameObject.FindWithTag("Player");
        _Shepherd = GameObject.FindWithTag("Shepherd");
    }

    int debugint = 1;

    void Update()
    {
        if (_Player.GetComponent<PlayerController>().isHided)
            {
                //debug//VibrateHandler();
                Vector3 PlayerVec = new Vector3(_Player.transform.position.x, 0, _Player.transform.position.z);
                Vector3 shepherdVec = new Vector3(_Shepherd.transform.position.x, 0, _Shepherd.transform.position.z);
                distance = Vector3.Distance(PlayerVec, shepherdVec);
            if (distance > LEVEL1_range)
            {
                StartCoroutine("Vibrate", "Level0");
            }
            else if (LEVEL1_range >= distance && distance > LEVEL2_range)
            {
                StartCoroutine("Vibrate", "Level1");// Vibrate, per seconds
            }
            else if (LEVEL2_range >= distance && distance > LEVEL3_range)
            {
                StartCoroutine("Vibrate", "Level2");
            }
            else if (LEVEL3_range >= distance && distance > LEVEL4_range)
            {
                StartCoroutine("Vibrate", "Level3");
            }
            else if (LEVEL4_range >= distance && distance > LEVEL5_range)
            {
                StartCoroutine("Vibrate", "Level4");
            }
            else if (LEVEL5_range >= distance)
            {
                StartCoroutine("Vibrate", "Level5");
            }
        }
        }
    
    IEnumerator Vibrate(string level)
    {
        if (level == "Level0")
        {
            if (isRunning) yield break;
            isRunning = true;
            VibrateHandler();
            yield return new WaitForSeconds(0.3f);
            VibrateHandler();
            yield return new WaitForSeconds(2.5f);
            isRunning = false;
        }
        if (level == "Level1")
        {
            if (isRunning) yield break;
            isRunning = true;
            VibrateHandler();
            yield return new WaitForSeconds(0.3f);
            VibrateHandler();
            yield return new WaitForSeconds(2.0f);
            isRunning = false;
        }
        if (level == "Level2")
        {
            if (isRunning) yield break;
            isRunning = true;
            VibrateHandler();
            yield return new WaitForSeconds(0.3f);
            VibrateHandler();
            yield return new WaitForSeconds(1.5f);
            isRunning = false;
        }
        if (level == "Level3")
        {
            if (isRunning) yield break;
            isRunning = true;
            VibrateHandler();
            yield return new WaitForSeconds(0.3f);
            VibrateHandler();
            yield return new WaitForSeconds(0.9f);
            isRunning = false;
        }
        if (level == "Level4")
        {
            if (isRunning) yield break;
            isRunning = true;
            VibrateHandler();
            yield return new WaitForSeconds(0.3f);
            VibrateHandler();
            yield return new WaitForSeconds(0.6f);
            isRunning = false;
        }
        if (level == "Level5")
        {
            if (isRunning) yield break;
            isRunning = true;
            VibrateHandler();
            yield return new WaitForSeconds(0.3f);
            VibrateHandler();
            yield return new WaitForSeconds(0.4f);
            isRunning = false;
        }
    }
  

    void VibrateHandler()
    {
        Vibration.Vibrate(150);//vibrate function
    }
}

