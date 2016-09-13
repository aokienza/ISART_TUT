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
                    ScoreLV1.text = HighScore1.ToString();
                }           
                break;
            case ("Level02"):
                if (score > HighScore2)
                {
                    HighScore2 = score;
                    PlayerPrefs.SetInt("HighScore2", HighScore2);
                    ScoreLV2.text = HighScore2.ToString();
                }
                break;
            case ("Level03"):
                if (score > HighScore3)
                {
                    HighScore3 = score;
                    PlayerPrefs.SetInt("HighScore3", HighScore3);
                    ScoreLV3.text = HighScore3.ToString();
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
