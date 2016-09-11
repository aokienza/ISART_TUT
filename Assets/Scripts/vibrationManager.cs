using UnityEngine;
using System.Collections;

public class vibrationManager : MonoBehaviour {

    GameObject _Player;
    GameObject _Shepherd;

    float distance;

    public float LEVEL1;
    public float LEVEL2;
    public float LEVEL3;

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

        }
        else if(LEVEL1 >= distance && distance > LEVEL2)
        {
            StartCoroutine("Level1");
        }
        else if (LEVEL2 >= distance && distance > LEVEL3)
        {
            StartCoroutine("Level2");
        }
        else if(LEVEL3 >= distance)
        {
            StartCoroutine("Level3");
        }
    }

    IEnumerator Level1()
    {
        VibrateHandler();
        yield return new WaitForSeconds(1.0f);
    }
    IEnumerator Level2()
    {
        VibrateHandler();
        yield return new WaitForSeconds(0.5f);
    }
    IEnumerator Level3()
    {
        VibrateHandler();
        yield return new WaitForSeconds(0.2f);
    }
    void VibrateHandler()
    {
        Handheld.Vibrate();
    }
}

