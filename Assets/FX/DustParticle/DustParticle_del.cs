using UnityEngine;
using System.Collections;

public class DustParticle_del : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        Invoke("del",0.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void del()
    {
        Destroy(this.gameObject);
    }
}
