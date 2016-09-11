using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VolumeControler : MonoBehaviour {

    [SerializeField]
    float volume;
    AudioListener activeCam;

    void Start()
    {
        if (PlayerPrefs.GetFloat("Volume") != null)
        {
            volume = PlayerPrefs.GetFloat("Volume");
        }
        else
        {
            volume = 1;
            PlayerPrefs.SetFloat("Volume", volume);
        }
        
    }

    void Update()
    {
        if (GameObject.FindObjectOfType<AudioListener>() == true && !activeCam)
        {
            activeCam = GameObject.FindObjectOfType<AudioListener>();
        }
        else if (activeCam)
        {
            //activeCam.volume = volume;
        }
    }

    public void SetVolume(float newVolume)
    {
        newVolume = GetComponent<Slider>().value;
        volume = newVolume;
        PlayerPrefs.SetFloat("Volume", newVolume);
    }
}
