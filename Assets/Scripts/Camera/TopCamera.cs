using UnityEngine;
using System.Collections;

public class TopCamera : MonoBehaviour {

    public Transform target;
    public float height;
	// Use this for initialization
	void Start () {
        transform.position = new Vector3(0, target.position.y + height, 0);
        transform.LookAt(target);
    }
	
	// Update is called once per frame
	void Update () {
        Recenter();
	}

    void Recenter()
    {
        transform.position = Vector3.Lerp(new Vector3(target.position.x, target.position.y + height, target.position.z), 
                                            new Vector3(transform.position.x, target.position.y + height, transform.position.z), 
                                                Time.deltaTime);
    }
}
