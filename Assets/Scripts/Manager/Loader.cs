using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

    public GameObject UI;
    // Use this for initialization
    void Start () {
        Instantiate(UI, Vector3.zero, Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
