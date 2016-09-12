using UnityEngine;
using System.Collections;

public class Gravel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider col)
    {
        if (col.transform.CompareTag("Player"))
        {
            AudioClip clip = gameObject.GetComponent<AudioSource>().clip;
            gameObject.GetComponent<AudioSource>().PlayOneShot(clip);
        }
        if (col.transform.CompareTag("Sheep"))
        {

        }
        if (col.transform.CompareTag("Shepherd"))
        {

        }
    }
}
