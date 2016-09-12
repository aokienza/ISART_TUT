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

    [SerializeField]
    Text menuScoreEnd;

    [SerializeField]
    Text[] PlayerNameTexts = new Text[15];

    [SerializeField] // Future feature, sauvegarde de score multiple
    Dictionary<string, int[]> bestScore = new Dictionary<string, int[]>();

    string nowPlayer;

    [SerializeField] // Future feature, sauvegarde de score multiple
    Dictionary<string, string[]> PlayerDictionary = new Dictionary<string, string[]>();


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
    public void RetrievePlayerName()
    {
        // Initialisation High Score Level 1
        string[] levels = { "level01", "level02", "level03" };
        for (int j = 0; j < levels.Length; j++)
        {
            if (!PlayerDictionary.ContainsKey(levels[j]))
                PlayerDictionary.Add(levels[j], new string[5]);
            for (int i = 0; i < 5; i++)
            {
                PlayerDictionary[levels[j]][i] = PlayerPrefs.GetString(("PlayerName" + levels[j]), "NoPlayer");
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
                InsertPlayerName(PlayerDictionary[levelName], nowPlayer, i);
                InsertScoreInBestScores(bestScore[levelName], score, i);
                UpdateHighScoresMenu(levelName, score, i);
                break;
            }
        }

        for (int i = 0; i < bestScore[levelName].Length; i++)
        {
            PlayerPrefs.SetInt(("bestScores" + levelName), bestScore[levelName][i]);
        }
        for (int i = 0; i < PlayerDictionary[levelName].Length; i++)
        {
            PlayerPrefs.SetString(("PlayerName" + levelName), PlayerDictionary[levelName][i]);
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

    void InsertPlayerName(string[] tab, string name, int index)
    {
        for (var i = 0; i < index; i++)
        {
            tab[i] = tab[i + 1];
        }
        tab[index] = name;
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
    void UpdatePlayerNameMenu(string tableau, string name, int index)
    {
        switch (tableau)
        {
            case "Level01":
                switch (index)
                {
                    case 0:
                        PlayerNameTexts[0].text = ("1) " + name).ToString();
                        break;
                    case 1:
                        PlayerNameTexts[1].text = ("2) " + name).ToString();
                        break;
                    case 2:
                        PlayerNameTexts[2].text = ("3) " + name).ToString();
                        break;
                    case 3:
                        PlayerNameTexts[3].text = ("4) " + name).ToString();
                        break;
                    case 4:
                        PlayerNameTexts[4].text = ("5) " + name).ToString();
                        break;
                }
                break;
            case "Level02":
                switch (index)
                {
                    case 0:
                        PlayerNameTexts[5].text = ("1) " + name).ToString();
                        break;
                    case 1:
                        PlayerNameTexts[6].text = ("2) " + name).ToString();
                        break;
                    case 2:
                        PlayerNameTexts[7].text = ("3) " + name).ToString();
                        break;
                    case 3:
                        PlayerNameTexts[8].text = ("4) " + name).ToString();
                        break;
                    case 4:
                        PlayerNameTexts[9].text = ("5) " + name).ToString();
                        break;
                }
                break;
            case "Level03":
                switch (index)
                {
                    case 0:
                        PlayerNameTexts[10].text = ("1) " + name).ToString();
                        break;
                    case 1:
                        PlayerNameTexts[11].text = ("2) " + name).ToString();
                        break;
                    case 2:
                        PlayerNameTexts[12].text = ("3) " + name).ToString();
                        break;
                    case 3:
                        PlayerNameTexts[13].text = ("4) " + name).ToString();
                        break;
                    case 4:
                        PlayerNameTexts[14].text = ("5) " + name).ToString();
                        break;
                }
                break;
        }

    }
    /*void UpdateMenuScore(int score)
    {
        menuScoreEnd.text = score.ToString();
    }*/
    #endregion
    public void ResetAllPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
