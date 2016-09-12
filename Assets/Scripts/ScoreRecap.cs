using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreRecap : MonoBehaviour {

    public int highScore; // High Score saved in game memory
    //public int score; // Local high score saved if higher than high score

    [SerializeField]
    Text menuScoreEnd;

    [SerializeField] // Future feature, sauvegarde de score multiple
    int[] bestScoresLevel1;
    int[] bestScoresLevel2;
    int[] bestScoresLevel3;

    private static ScoreRecap alreadyCreated; // Variable anti doublon

    void Awake()
    {
        DontDestroyOnLoad(this);
        checkDoublon();
    }
    // Use this for initialization
    void Start()
    {
        InitialisationScores();
        UpdateMenuScore(highScore);
    }
    void InitialisationScores()
    {
        // Initialisation High Score Level 1
        for (int i = 0; i < bestScoresLevel1.Length; i++)
        {
            bestScoresLevel1[i] = PlayerPrefs.GetInt("bestScoresLevel1-" + i.ToString(), 0);
        }
        // Initialisation High Score Level 2
        for (int i = 0; i < bestScoresLevel2.Length; i++)
        {
            bestScoresLevel2[i] = PlayerPrefs.GetInt("bestScoresLevel2-" + i.ToString(), 0);
        }
        // Initialisation High Score Level 3
        for (int i = 0; i < bestScoresLevel3.Length; i++)
        {
            bestScoresLevel3[i] = PlayerPrefs.GetInt("bestScoresLevel3-" + i.ToString(), 0);
        }
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

    public void SubmitScoreLevel1(int score)
    {
        int temp;
        //int temp2;
        for (var i = bestScoresLevel1.Length - 1; i >= 0; i--)
        {
            if (bestScoresLevel1[i] < score)
            {
                temp = bestScoresLevel1[i];
                bestScoresLevel1[i] = score;
                score = temp;
            }
        }
        for (int i = 0; i < bestScoresLevel1.Length; i++)
        {
            PlayerPrefs.SetInt("bestScoresLevel1-" + i.ToString(), bestScoresLevel1[i]);
        }
        PlayerPrefs.Save();
    }
    public void SubmitScoreLevel2(int score)
    {
        int temp;
        //int temp2;
        for (var i = bestScoresLevel2.Length - 1; i >= 0; i--)
        {
            if (bestScoresLevel2[i] < score)
            {
                temp = bestScoresLevel2[i];
                bestScoresLevel2[i] = score;
                score = temp;
            }
        }
        for (int i = 0; i < bestScoresLevel2.Length; i++)
        {
            PlayerPrefs.SetInt("bestScoresLevel2-" + i.ToString(), bestScoresLevel2[i]);
        }
        PlayerPrefs.Save();
    }
    public void SubmitScoreLevel3(int score)
    {
        int temp;
        //int temp2;
        for (var i = bestScoresLevel3.Length - 1; i >= 0; i--)
        {
            if (bestScoresLevel3[i] < score)
            {
                temp = bestScoresLevel3[i];
                bestScoresLevel3[i] = score;
                score = temp;
            }
        }
        for (int i = 0; i < bestScoresLevel3.Length; i++)
        {
            PlayerPrefs.SetInt("bestScoresLevel3-" + i.ToString(), bestScoresLevel3[i]);
        }
        PlayerPrefs.Save();
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
