using UnityEngine;
using System.Collections;

public class Fence_Pillar : MonoBehaviour
{
    float velocity;
    // Use this for initialization
    void Start()
    {
        velocity = GameObject.FindWithTag("Player").GetComponent<PlayerController>().velocity;
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void OnTriggerEnter(Collider col)
    {
        if (col.transform.CompareTag("Player"))
        {
            col.gameObject.GetComponent<BoxCollider>().isTrigger = false;
            col.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.transform.CompareTag("Player"))
        {
            col.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            col.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    
}