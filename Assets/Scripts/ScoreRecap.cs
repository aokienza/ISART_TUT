using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScoreRecap : MonoBehaviour
{

    public static ScoreRecap instance = null;

    public int highScore; // High Score saved in game memory
    public int score; // Local high score saved if higher than high score

    #region UI SCORES
    [SerializeField]
    Text[] highScoresTexts = new Text[15];
    #endregion
    bool hasFinishedFont;
    [SerializeField]
    Text menuScoreEnd;

    public LevelManager Manager;

    string[,] PlayerName = new string[3,5];

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
        //UpdateMenuScore(highScore);
    }
    public void RetrieveBestScore()
    {
        // Initialisation High Score Level 1
        string[] levels = { "level01", "level02", "level03" };
        for (int j = 0; j < levels.Length; j++)
        {
            if (!bestScore.ContainsKey(levels[j]))
                bestScore.Add(levels[j], new int[5]);
            for (int i = 0; i < 5; i++)
            {
                bestScore[levels[j]][i] = PlayerPrefs.GetInt(("bestScores" + levels[j]), 0);
            }

        }
    }
 
    public void SubmitScore(string levelName, int score)
    {
        if (!bestScore.ContainsKey(levelName))
        {
            bestScore.Add(levelName, new int[5]);
        }
        for (var i = bestScore[levelName].Length - 1; i >= 0; i--)
        {
            if (bestScore[levelName][i] < score)
            {
                menuScoreEnd.text = ("YOUR SCORE IS : " + score);
                InsertScoreInBestScores(bestScore[levelName], score, i);
                UpdateHighScoresMenu(levelName, score, i);
                break;
            }
        }

        for (int i = 0; i < bestScore[levelName].Length; i++)
        {
            PlayerPrefs.SetInt(("bestScores" + levelName), bestScore[levelName][i]);
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

    #region Update UI Scores
    void UpdateHighScoresMenu(string tableau, int score, int index)
    {
        switch (tableau)
        {
            case "Level01":
                switch (index)
                {
                    case 0:
                        highScoresTexts[0].text = ("1) " + score).ToString();
                        break;
                    case 1:
                        highScoresTexts[1].text = ("2) " + score).ToString();
                        break;
                    case 2:
                        highScoresTexts[2].text = ("3) " + score).ToString();
                        break;
                    case 3:
                        highScoresTexts[3].text = ("4) " + score).ToString();
                        break;
                    case 4:
                        highScoresTexts[4].text = ("5) " + score).ToString();
                        break;
                }
                break;
            case "Level02":
                switch (index)
                {
                    case 0:
                        highScoresTexts[5].text = ("1) " + score).ToString();
                        break;
                    case 1:
                        highScoresTexts[6].text = ("2) " + score).ToString();
                        break;
                    case 2:
                        highScoresTexts[7].text = ("3) " + score).ToString();
                        break;
                    case 3:
                        highScoresTexts[8].text = ("4) " + score).ToString();
                        break;
                    case 4:
                        highScoresTexts[9].text = ("5) " + score).ToString();
                        break;
                }
                break;
            case "Level03":
                switch (index)
                {
                    case 0:
                        highScoresTexts[10].text = ("1) " + score).ToString();
                        break;
                    case 1:
                        highScoresTexts[11].text = ("2) " + score).ToString();
                        break;
                    case 2:
                        highScoresTexts[12].text = ("3) " + score).ToString();
                        break;
                    case 3:
                        highScoresTexts[13].text = ("4) " + score).ToString();
                        break;
                    case 4:
                        highScoresTexts[14].text = ("5) " + score).ToString();
                        break;
                }
                break;
        }

    }

    public IEnumerator FontEffect(Text cible, int size)
    {
        cible.fontSize = size;
        yield return new WaitForSeconds(.01f);
        if (cible.fontSize >60)
        {
            cible.fontSize--;
            StartCoroutine(FontEffect(cible, cible.fontSize));
        }
    }

    #endregion
    public void ResetAllPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
