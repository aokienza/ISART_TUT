using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    private int score;
    [SerializeField]
    private int[] highScore = new int[10];

    void Start()
    {
        score = 0;

        for (int i = 0; i < highScore.Length; i++)
        {
            highScore[i] = PlayerPrefs.GetInt("highScoreKey" + i.ToString(),0);
        }
    }

    void saveRanking(int score)
    {
            int _tmp = 0;
            for (var i = highScore.Length -1 ; i >= 0; i--) // highscore[0]…min ,highscore[length]…max
        {
                if (highScore[i] < score)
                {
                    _tmp = highScore[i];
                    highScore[i] = score;
                    score = _tmp;
                }
            }
        Save();
    }
    public void Save()
    {
        // save
        for (int i = 0; i < highScore.Length; i++) {
            PlayerPrefs.SetInt("highScoreKey" + i.ToString(), highScore[i]);
        }
        for (int i = 9; i >= 0; i--)
        {
            Debug.Log(i + ":" + highScore[i]);
        }
        PlayerPrefs.Save();
    }

    public void Reset()
    {
        //reset key
        PlayerPrefs.DeleteAll();
    }
}

