using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SBS_GameMgr : MonoBehaviour
{

    //목표: 매니져를 생성하고 접근하여 데이터를 쉽게 쓰고싶다

    //필요속성 : 인스턴스, 적생성위치, 적생성시간,적생성수,스코어,게임종료

    private static SBS_GameMgr instance;
    public static SBS_GameMgr Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<SBS_GameMgr>();
            
            return instance;
        }
    }

    private int score;

    public float createTime = 7.0f;

    public int maxEnemy = 7;

    public bool _isGameBossDie = false;

    public bool _isEnemyHqDestroy = false;
    public bool _isGameover { get; private set; }

    public Transform[] e_PointsTr;

    public Transform[] e_PointsTr2;

    public GameObject enemys;
    private void Awake()
    {
        if (Instance != this) Destroy(gameObject);
    }

    void Start()
    {
        e_PointsTr = GameObject.Find("E_SpawnPoints").GetComponentsInChildren<Transform>();
      
        if (e_PointsTr.Length > 0) StartCoroutine(this.CreateEnemy());

        e_PointsTr2 = GameObject.Find("E_SpawnPoints2").GetComponentsInChildren<Transform>();

        if (e_PointsTr2.Length > 0) StartCoroutine(this.CreateEnemy());
    }

    // Enemy 생성
    IEnumerator CreateEnemy()
    {
        while (!_isGameover)
        {
            int enemyCount = (int)GameObject.FindGameObjectsWithTag("ENEMY").Length;

            if (enemyCount < maxEnemy)
            {
 
                yield return new WaitForSeconds(createTime);

                int idx = Random.Range(1, e_PointsTr.Length);

                Instantiate(enemys, e_PointsTr[idx].position, e_PointsTr[idx].rotation);

                int idx2 = Random.Range(1, e_PointsTr2.Length);

                Instantiate(enemys, e_PointsTr2[idx].position, e_PointsTr2[idx].rotation);
            }
            else
            {
                yield return null;
            }
        }
    }

    public void AddScore(int newScore)
    {
        if (!_isGameover)
        {
            score += newScore;
          //  SBS_UIManager.Instance.UpdateScoreText(score);
        }
    }
    
    public void EndGame()
    {
        _isGameover = true;
       // SBS_UIManager.Instance.SetActiveGameoverUI(true);
    }
}