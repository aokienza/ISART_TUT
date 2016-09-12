using UnityEngine;
using System.Collections;

public class Bush : MonoBehaviour {
    public float loss;
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
            float _velocity = col.gameObject.GetComponent<PlayerController>().velocity;
            _velocity = _velocity / loss;
            col.gameObject.GetComponent<PlayerController>().velocity = _velocity;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.transform.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerController>().velocity = velocity;
        }
    }
}
