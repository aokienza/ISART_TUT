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
        Feedback();
    }

    int debugint = 1;

    void Feedback()
    {
        if (_Player.GetComponent<PlayerController>().isHided)
            {
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
        else
        {
            StartCoroutine("Vibrate", "Nope");
        }
    }
    
    IEnumerator Vibrate(string level)
    {
        if (level == "Level0")
        {
            if (isRunning) yield break;
            isRunning = true;
            VibrateHandler();
            yield return new WaitForSeconds(1.2f);
            VibrateHandler();
            yield return new WaitForSeconds(1.2f);
            isRunning = false;
        }
        if (level == "Level1")
        {
            if (isRunning) yield break;
            isRunning = true;
            VibrateHandler();
            yield return new WaitForSeconds(1.0f);
            VibrateHandler();
            yield return new WaitForSeconds(1.0f);
            isRunning = false;
        }
        if (level == "Level2")
        {
            if (isRunning) yield break;
            isRunning = true;
            VibrateHandler();
            yield return new WaitForSeconds(0.8f);
            VibrateHandler();
            yield return new WaitForSeconds(0.8f);
            isRunning = false;
        }
        if (level == "Level3")
        {
            if (isRunning) yield break;
            isRunning = true;
            VibrateHandler();
            yield return new WaitForSeconds(0.6f);
            VibrateHandler();
            yield return new WaitForSeconds(0.6f);
            isRunning = false;
        }
        if (level == "Level4")
        {
            if (isRunning) yield break;
            isRunning = true;
            VibrateHandler();
            yield return new WaitForSeconds(0.4f);
            VibrateHandler();
            yield return new WaitForSeconds(0.4f);
            isRunning = false;
        }
        if (level == "Level5")
        {
            if (isRunning) yield break;
            isRunning = true;
            VibrateHandler();
            yield return new WaitForSeconds(0.2f);
            VibrateHandler();
            yield return new WaitForSeconds(0.2f);
            isRunning = false;
        }
        if(level == "Nope")
        {
            yield return new WaitForSeconds(2f);
        }
        Feedback();
    }
  

    void VibrateHandler()
    {
        Vibration.Vibrate(150);//vibrate function
    }
}

