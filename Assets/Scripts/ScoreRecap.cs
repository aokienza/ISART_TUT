using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreRecap : MonoBehaviour {

    public int highScore; // High Score saved in game memory
    //public int score; // Local high score saved if higher than high score

    [SerializeField]
    Text menuScoreEnd;

    [SerializeField] // Future feature, sauvegarde de score multiple
    int[] bestScores;

    private static ScoreRecap alreadyCreated;

    void Awake()
    {
        DontDestroyOnLoad(this);
        checkDoublon();
    }
    // Use this for initialization
    void Start()
    {
        highScore = PlayerPrefs.GetInt("High Score");
        UpdateMenuScore(highScore);
    }

    void checkDoublon()
    {
        if (alreadyCreated == null)
        {
            alreadyCreated = this;
            
        }
        else
        {
            Destroy(this.gameObject);
        }
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
        menuScoreEnd.text = score.ToString();
    }
}
