using UnityEngine;
using System.Collections;

public class Stone : MonoBehaviour {
    public float StanTime = 1.0f;
    float velocity;
	// Use this for initialization
	void Start () {
        velocity = GameObject.Find("Player").GetComponent<PlayerController>().velocity;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.transform.CompareTag("Player"))
        {
            Debug.Log("Stone");
            col.gameObject.GetComponent<PlayerController>().velocity = 0;
            Invoke("setFloat", StanTime);
        }
    }

    void setFloat()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().velocity = velocity;
    }
}
