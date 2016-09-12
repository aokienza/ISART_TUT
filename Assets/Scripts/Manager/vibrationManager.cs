using UnityEngine;
using System.Collections;

public class vibrationManager : MonoBehaviour {

    GameObject _Player;
    GameObject _Shepherd;

    float distance;

    public float LEVEL1_range;
    public float LEVEL2_range;



    bool isRunning = false;


    void Start()
    {
        if (!SystemInfo.supportsVibration) Destroy(gameObject); 
        _Player = GameObject.FindWithTag("Player");
        _Shepherd = GameObject.FindWithTag("Shepherd");
    }
    
    void Update()
    {
        if (_Player.GetComponent<PlayerController>().isHided)
        {
            //debug//VibrateHandler();
            Vector3 PlayerVec = new Vector3(_Player.transform.position.x, 0, _Player.transform.position.z);
            Vector3 shepherdVec = new Vector3(_Shepherd.transform.position.x, 0, _Shepherd.transform.position.z);
            distance = Vector3.Distance(PlayerVec, shepherdVec);
            Debug.Log(distance);
            if (distance > LEVEL1_range)
            {
                // 
            }
            else if (LEVEL1_range >= distance && distance > LEVEL2_range)
            {
                StartCoroutine("Vibrate", 3.0f);
            }
            else if (LEVEL2_range >= distance)
            {
                StartCoroutine("Vibrate", 1.5f);
            }
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

