using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    
    public int score;
    public int perScore = 100;

	// Use this for initialization
	void Start () {
        score = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddScore()
    {
        score += perScore;
    }

    public int GetScore()
    {
        return score;
    }
}
