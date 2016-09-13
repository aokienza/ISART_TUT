using UnityEngine;
using System.Collections;

public class Entrance : MonoBehaviour {


    public Sprite openGate;
    public Sprite closeGate;

    protected SpriteRenderer _sRenderer;
    void Awake()
    {
        _sRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
    // Use this for initialization
    public void Use() {
        _sRenderer.sprite = openGate;
        StartCoroutine(Close());
    }

    IEnumerator Close()
    {
        float time = 1;
        while(time > 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }
        _sRenderer.sprite = closeGate;
    }
	
}
