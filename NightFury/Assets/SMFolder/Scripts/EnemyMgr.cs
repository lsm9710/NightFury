using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMgr : MonoBehaviour
{
    public List<GameObject> EnemyList = new List<GameObject>();

    public static EnemyMgr Instance = null;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        AddList();
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void AddList()
    {
        //에너미를 검색해서 tag가 에너미인 녀석들을 모두 넣겠다
        GameObject[] a = GameObject.FindGameObjectsWithTag("ENEMY");
        for (int i = 0; i < a.Length; i++)
        {
            EnemyList.Add(a[i]);
        }
    }

}
