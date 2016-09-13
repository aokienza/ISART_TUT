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
    Text ScoreLV1;
    [SerializeField]
    Text ScoreLV2;
    [SerializeField]
    Text ScoreLV3;
    public int HighScore1;
    public int HighScore2;
    public int HighScore3;

    #endregion
    bool hasFinishedFont;
    [SerializeField]
    Text menuScoreEnd;
    [SerializeField]
    Text menuScoreEndHigh;
    [SerializeField]
    Sprite WinScreen;
    [SerializeField]
    Sprite LoseScreen;
    [SerializeField]
    Image backGroundScore;


    public LevelManager Manager;

    string[,] PlayerName = new string[3,5];




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
    }
    public void RetrieveBestScore()
    {
        HighScore1 = PlayerPrefs.GetInt("HighScore1");
        HighScore2 = PlayerPrefs.GetInt("HighScore2");
        HighScore3 = PlayerPrefs.GetInt("HighScore3");
        ScoreLV1.text = ("HIGH SCORE " + HighScore1.ToString());
        ScoreLV2.text = ("HIGH SCORE " + HighScore2.ToString());
        ScoreLV3.text = ("HIGH SCORE " + HighScore3.ToString());
    }
 
    public void ScoreScreen(int score, bool victory, string levelName)
    {
        if (victory)
        {
            menuScoreEnd.text = score.ToString();
            menuScoreEndHigh.text = score.ToString();
            backGroundScore.sprite = WinScreen;

        }
        else if (!victory)
        {
            menuScoreEnd.text = score.ToString();
            backGroundScore.sprite = LoseScreen;
            switch (levelName)
            {
                case ("Level01"):
                    menuScoreEndHigh.text = HighScore1.ToString();
                    break;
                case ("Level02"):
                    menuScoreEndHigh.text = HighScore2.ToString();
                    break;
                case ("Level03"):
                    menuScoreEndHigh.text = HighScore3.ToString();
                    break;
            }


        }
        
    }


    public void SubmitScore(string levelName, int score)
    {
        
        switch(levelName)
        {
            case ("Level01"):
                if (score >HighScore1)
                {
                    HighScore1 = score;
                    PlayerPrefs.SetInt("HighScore1", HighScore1);
                    ScoreLV1.text = ("HIGH SCORE " + HighScore1.ToString());
                    ScoreScreen(HighScore1, true, levelName);
                }
                else
                {
                    ScoreScreen(score, false, levelName);
                }
                break;
            case ("Level02"):
                if (score > HighScore2)
                {
                    HighScore2 = score;
                    PlayerPrefs.SetInt("HighScore2", HighScore2);
                    ScoreLV2.text = ("HIGH SCORE " +HighScore2.ToString());
                    ScoreScreen(HighScore2, true, levelName);
                }
                else
                {
                    ScoreScreen(score, false, levelName);
                }
                break;
            case ("Level03"):
                if (score > HighScore3)
                {
                    HighScore3 = score;
                    PlayerPrefs.SetInt("HighScore3", HighScore3);
                    ScoreLV3.text = ("HIGH SCORE " + HighScore3 .ToString());
                    ScoreScreen(HighScore3, true, levelName);
                }
                else
                {
                    ScoreScreen(score, false, levelName);
                }
                break;
        }      
        PlayerPrefs.Save();
    }
    
 
     #region Update UI Scores


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
