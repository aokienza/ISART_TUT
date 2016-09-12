using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreDebug : MonoBehaviour {

    int Score;

    [SerializeField]
    ScoreRecap scoreManager;
    [SerializeField]
    Text scoreChoose;
    [SerializeField]
    Text ScoreField;
    public void ScoreSubmitDebug()
    {
        
        int scoreDebug1;
        scoreDebug1 = int.Parse(scoreChoose.text);
        Debug.Log(scoreChoose.text);
        scoreManager.SubmitScore(scoreDebug1);
    }
}
