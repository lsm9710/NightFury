using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int score;
    private int topScore;

    public Text scoreUI;
    public Text topUI;

    public static string scoreSaveText = "TopScore";

    public static ScoreManager instance;

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            //scoreUI.text = "Score : " + score;
            //// -> 스코어가 탑스코어보다 크면 최대점수를 갱신하겠다
            //if (score >= topScore)
            //{
            //    topScore = score;
            //    //저장을 하고싶다 최고점수를
            //    PlayerPrefs.SetInt(scoreSaveText, topScore);
            //    topUI.text = "TopScore : " + topScore;
            //}
        }
    }
    private void Awake()
    {
        instance = this;
    }
}
