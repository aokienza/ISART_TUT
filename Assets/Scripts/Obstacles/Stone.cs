using UnityEngine;
using System.Collections;

public class Stone : MonoBehaviour {
    public float StanTime = 1.0f;
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
            col.gameObject.GetComponent<PlayerController>().velocity = 0;
            col.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            col.gameObject.GetComponent<BoxCollider>().isTrigger = false;
            Invoke("setFloat", StanTime);
        }
        if (col.transform.CompareTag("Sheep"))
        {

        }
        if (col.transform.CompareTag("Shepherd"))
        {

        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.transform.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            col.gameObject.GetComponent<BoxCollider>().isTrigger = true;
        }
    }
            void setFloat()
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().velocity = velocity;
    }
    
}
