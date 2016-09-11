using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreDebug : MonoBehaviour {

    [SerializeField]
    ScoreRecap scoreManager;
    [SerializeField]
    Text scoreChoose;

    public void ScoreSubmitDebug()
    {
        
        int scoreDebug1;
        scoreDebug1 = int.Parse(scoreChoose.text);
        Debug.Log(scoreChoose.text);
        scoreManager.SubmitScore(scoreDebug1);
    }
}
