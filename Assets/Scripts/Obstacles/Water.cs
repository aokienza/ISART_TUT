using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {

    public float loss;
    public float accele;
    float velocity;
    float sheep_speed;
    float shepherd_speed;
    // Use this for initialization
    void Start()
    {
        velocity = GameObject.FindWithTag("Player").GetComponent<PlayerController>().velocity;
        sheep_speed = GameObject.FindWithTag("Sheep").GetComponent<AI_Sheep>().speed;
        shepherd_speed = GameObject.FindWithTag("Shepherd").GetComponent<AI_Shepherd>().speed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.transform.CompareTag("Player"))
        {
            float _velocity = col.gameObject.GetComponent<PlayerController>().velocity;
            _velocity = _velocity * accele;
            col.gameObject.GetComponent<PlayerController>().velocity = _velocity;
        }
        if (col.transform.CompareTag("Sheep"))
        {
            float _speed = col.gameObject.GetComponent<AI_Sheep>().speed;
            _speed = _speed / 2;
            col.gameObject.GetComponent<AI_Sheep>().speed = _speed; 
        }
        if (col.transform.CompareTag("Shepherd"))
        {
            float _speed = col.gameObject.GetComponent<AI_Shepherd>().speed;
            _speed = _speed / 2;
            col.gameObject.GetComponent<AI_Shepherd>().speed = _speed;

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
            col.gameObject.GetComponent<AI_Sheep>().speed = sheep_speed;
        }
        if (col.transform.CompareTag("Shepherd"))
        {
            col.gameObject.GetComponent<AI_Shepherd>().speed = shepherd_speed;
        }
    }
}
