using UnityEngine;
using System.Collections;

public class Bush : MonoBehaviour {
    public float loss;
    float velocity;
	// Use this for initialization
	void Start () {
        velocity = GameObject.FindWithTag("Player").GetComponent<PlayerController>().velocity;
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
        if (col.transform.CompareTag("Sheep"))
        {
            col.gameObject.GetComponent<AI_Sheep>().Jump();
        }
        if (col.transform.CompareTag("Shepherd"))
        {

        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.transform.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerController>().velocity = velocity;
        }
        if (col.transform.CompareTag("Sheep"))
        {

        }
        if (col.transform.CompareTag("Shepherd"))
        {

        }
    }
}
