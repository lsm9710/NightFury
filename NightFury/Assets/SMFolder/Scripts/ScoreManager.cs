using System;
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

    TestGrab tg;

    public static string scoreSaveText = "TopScore";

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            //      -> 스코어가 탑스코어보다 크면 최대점수를 갱신하겠다
            scoreUI.text = "Score : " + score;
            if (score >= topScore)
            {
                topScore = score;
                //목표3 : 저장을 하고싶다. -> 최고점수를 저장한다.
                PlayerPrefs.SetInt(scoreSaveText, topScore);
                topUI.text = "TopScore : " + topScore;
            }
        }
    }

    #region ----------------=========== 싱글톤============--------
    //싱글톤 구현
    //게시판(static) 등록할 속성이 필요 => 객체(ScoreManager)
    public static ScoreManager instance;

    private void Awake()
    {
        //만약에 Instance에 값이 없으면
        if (instance == null)
        {
            instance = this;
        }
        // -> Instance에 나 자신을 넣겠다.

    }
    #endregion

    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(false);
        //목표1 : 시작할때 점수를 UI에 표시하고싶다
        scoreUI.text = "Score : " + score;
        //목표 4 : 시작할때 저장된 값을 가져와서 화면에 표시하고 싶다.
        //필요속성 : 저장된 값을 받을 그릇(topScore), 그값을 표시할 UI(topUI)
        //1. 점수가 있으니까  -> 불러왔으니까
        //  -> 저장된 점수를 불러온다.
        //  -> 그 값을 topScore에 넣는다.
        topScore = PlayerPrefs.GetInt(scoreSaveText, 0);
        //2. 화면에 표시하고 싶다.
        topUI.text = "TopScore : " + topScore;

        tg = GameObject.Find("OVRControllerPrefab_L").GetComponent<TestGrab>();
    }
    public bool a = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && a == false)
        {
            StartCoroutine(ShowSuccessUI());
        }
    }

    public float endTime;
    IEnumerator ShowSuccessUI()
    {
        a = true;
        //드래곤한테 랜딩하라고 얘기하고 싶다
        tg.state = TestGrab.MoveState.Gall;
        //일정시간 뒤에 UI를 보여주고싶다
        //일정시간은 랜딩 애니메이션이 끝났을때
        yield return new WaitForSeconds(endTime);
        canvas.SetActive(true);
    }
}
