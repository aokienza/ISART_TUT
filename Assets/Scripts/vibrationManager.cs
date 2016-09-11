using UnityEngine;
using System.Collections;

public class vibrationManager : MonoBehaviour {

    GameObject _Player;
    GameObject _Shepherd;

    float distance;

    public float LEVEL1;
    public float LEVEL2;
    public float LEVEL3;


    bool isRunning = false;


    void Start()
    {
        _Player = GameObject.Find("Player");
        _Shepherd = GameObject.Find("Shepherd");
    }

    void Update()
    {
        distance = Vector3.Distance(_Player.transform.position, _Shepherd.transform.position);
        if(distance > LEVEL1)
        {
            // 
        }
        else if(LEVEL1 >= distance && distance > LEVEL2)
        {
            StartCoroutine("Vibrate",1.0f);
        }
        else if (LEVEL2 >= distance && distance > LEVEL3)
        {
            StartCoroutine("Vibrate",0.5f);
        }
        else if(LEVEL3 >= distance)
        {
            StartCoroutine("Vibrate",0.2f);
        }
    }
    IEnumerator Vibrate(float time)
    {
        if (isRunning) yield break;
        isRunning = true;
        VibrateHandler();
        yield return new WaitForSeconds(time);
        isRunning = false;
    }
  

    void VibrateHandler()
    {
        Handheld.Vibrate();
    }
}

