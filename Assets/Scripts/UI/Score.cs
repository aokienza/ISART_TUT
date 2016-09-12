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
        scoreManager.SubmitScoreLevel1(scoreDebug1);
    }
    public void ScoreSubmitDebug2()
    {
        int scoreDebug2;
        scoreDebug2 = int.Parse(scoreChoose.text);
        Debug.Log(scoreChoose.text);
        scoreManager.SubmitScoreLevel2(scoreDebug2);
    }
    public void ScoreSubmitDebug3()
    {
        int scoreDebug3;
        scoreDebug3 = int.Parse(scoreChoose.text);
        Debug.Log(scoreChoose.text);
        scoreManager.SubmitScoreLevel3(scoreDebug3);
    }
}
