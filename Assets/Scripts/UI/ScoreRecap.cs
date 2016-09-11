using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreRecap : MonoBehaviour {

    public int highScore; // High Score saved in game memory
    //public int score; // Local high score saved if higher than high score

    [SerializeField]
    Text menuScoreEnd;

    [SerializeField]
    int[] bestScores;


    void Awake()
    {
        DontDestroyOnLoad(this);
    }
    // Use this for initialization
    void Start()
    {
        highScore = PlayerPrefs.GetInt("High Score");
        UpdateMenuScore(highScore);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SubmitScore(int score)
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("High Score", highScore);
            UpdateMenuScore(highScore);
        }
    }
    
    void UpdateMenuScore(int score)
    {
        Debug.Log(score);
        menuScoreEnd.text = score.ToString();
    }
}
