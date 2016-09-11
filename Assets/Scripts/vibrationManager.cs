using UnityEngine;
using System.Collections;

public class vibrationManager : MonoBehaviour {

    public float setTime;
    float timer;

    void Start()
    {
        if (SystemInfo.supportsVibration) print("vibration OK");
        else print("振動非対応");

        timer = setTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            timer = setTime;
            VibrateHandler();
        }
    }

    void VibrateHandler()
    {
        Handheld.Vibrate();
    }
}

