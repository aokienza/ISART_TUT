using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VolumeControler : MonoBehaviour {

    [SerializeField]
    float volume;

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
        AudioListener.volume = volume;
    }

    public void SetVolume(float newVolume)
    {
        newVolume = GetComponent<Slider>().value;
        volume = newVolume;
        PlayerPrefs.SetFloat("Volume", newVolume);
    }
}
