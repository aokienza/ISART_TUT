using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScoreRecap : MonoBehaviour {

    public static ScoreRecap instance = null;

    public int highScore; // High Score saved in game memory
    //public int score; // Local high score saved if higher than high score

    [SerializeField]
    Text menuScoreEnd;

    [SerializeField] // Future feature, sauvegarde de score multiple
    Dictionary<string, int[]> bestScore = new Dictionary<string, int[]>();

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
    }
    // Use this for initialization
    void Start()
    {
        RetrieveBestScore();
        UpdateMenuScore(highScore);
    }
    void RetrieveBestScore()
    {
        // Initialisation High Score Level 1
        string[] levels = { "level1", "level2", "level3" };
        for (int j = 0; j < levels.Length; j++)
        {
            bestScore.Add(levels[j], new int[5]);
            for (int i = 0; i < 5; i++)
            {
                bestScore[levels[j]][i] = PlayerPrefs.GetInt(("bestScores"+ levels[j]), 0);
            }
        }
    }

    public void SubmitScore(string levelName, int score)
    {
        if (bestScore[levelName] != null)
        {
            for (var i = bestScore[levelName].Length - 1; i >= 0; i--)
            {
                if (bestScore[levelName][i] < score)
                {
                    InsertScoreInBestScores(bestScore[levelName], score, i);
                    break;
                }
            }
        }

        for (int i = 0; i < bestScore[levelName].Length; i++)
        {
            PlayerPrefs.SetInt(("bestScores"+ levelName), bestScore[levelName][i]);
        }
        PlayerPrefs.Save();
    }

    void InsertScoreInBestScores(int[] tab, int bestScore, int index)
    {
        for (var i = 0; i < index; i++)
        {
            tab[i] = tab[i + 1];
        }
        tab[index] = bestScore;
    }

    void UpdateMenuScore(int score)
    {
        menuScoreEnd.text = score.ToString();
    }

    public void ResetAllPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
