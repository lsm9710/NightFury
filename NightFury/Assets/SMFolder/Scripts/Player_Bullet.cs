using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullet : MonoBehaviour
{
    public enum BulletState
    {
        Straight,
        Tracking
    }
    public BulletState state;

    float speed;
    //드래곤의 속도에 추가로 더해줄 총알의 속도
    public float plusSpeed = 20f;
    //회전하는 속도
    public float rotSpeed = 0.5f;
    PistolGrad_Shot ps;
    TestDragonMove tm;
    TestGrab tg;

    //타겟을 감지할 반경
    public float targetRange;

    //감지한 타겟을 저장할 변수
    GameObject target;
    Vector3 targetDir;
    Vector3 targetOldDir;
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_PC
        ps = GameObject.Find("Camera").GetComponent<PistolGrad_Shot>();
        tm = GameObject.Find("Camera").GetComponent<TestDragonMove>();
        tg = GameObject.Find("Camera").GetComponent<TestGrab>();
        speed = tm.moveSpeed + plusSpeed;
        //StartCoroutine(StartLife());
        enemyList = EnemyMgr.Instance.EnemyList;
#else
        ps = GameObject.Find("OVRControllerPrefab_R").GetComponent<PistolGrad_Shot>();
        tm = GameObject.Find("OVRControllerPrefab_L").GetComponent<TestDragonMove>();
        tg = GameObject.Find("OVRControllerPrefab_L").GetComponent<TestGrab>();
        speed = tm.moveSpeed + plusSpeed;
        StartCoroutine(StartLife());
        enemyList = EnemyMgr.Instance.EnemyList;
#endif

        targetDir = transform.forward;
        
    }

    //에너미의 목록
    List<GameObject> enemyList = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        Switch();
        int closest = 0;
        for (int i = 0; i < enemyList.Count; i++)
        {
            float last = Vector3.Distance(enemyList[closest].transform.position, transform.position);
            float cur = Vector3.Distance(enemyList[i].transform.position, transform.position);

            if (cur < last)
            {
                closest = i;
            }
            //타겟을 closest로 변경
            target = enemyList[closest];
        }

        //타겟과의 거리를 저장할 변수
        float dis = Vector3.Distance(target.transform.position, transform.position);

        //타겟이 거리안에 들어오면
        if (dis <= targetRange)
        {
            //추적모드 돌입
        }
            state = BulletState.Tracking;
        if (target == null)
        {
            state = BulletState.Straight;
        }
    }

    void Switch()
    {
        switch (state)
        {
            case BulletState.Straight:
                Straight();
                break;
            case BulletState.Tracking:
                Tracking();
                break;
        }
    }

    private void Straight()
    {
        Vector3 dir = transform.forward;
        transform.position += dir * speed * Time.deltaTime;
    }

    float fOldTime = 0f;
    public float tracingTime;
    
    private void Tracking()
    {
        //타겟을 향해 쫓아가고싶다.
        //타겟이 어딧는데?
        //타겟의 위치

       // float fDot = Vector3.Dot(dir, gameObject.transform.forward);
       // transform.position += dir * speed * Time.deltaTime;

        fOldTime += Time.deltaTime;
        if (fOldTime > tracingTime)
        {
            
            targetDir = (target.transform.position - transform.position).normalized;

            //타겟을 향해 회전하고 싶다
       /*     if (fDot > 0.1f)
            {
                gameObject.transform.Rotate(Vector3.up, -10.0f);
            }
            else if (fDot < 0.1f)
            {
                gameObject.transform.Rotate(Vector3.up, 10.0f);
            }
            */
            fOldTime = 0f;
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), rotSpeed * Time.deltaTime);
            speed *=1.1f;
        }

        if (speed > 140) speed = 140;

        transform.position += transform.forward * speed * Time.deltaTime;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetDir),rotSpeed * Time.deltaTime);

    }

    //폭발공장주소
    public GameObject boom;
    //무언가에 충돌했으면 아프지 ㅇㅇ
    private void OnCollisionEnter(Collision collision)
    {
        print("나 얘랑 충돌헀어" +collision.gameObject.name);
        //충돌한게 에너미일때만 점수를 추가
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            ScoreManager.instance.Score += 10;
        }
        //다시 리스트에 넣고싶다
        ps.pistolBullet.Add(gameObject);
        //폭발이펙트랑 사운드를 넣고 싶다.
        GameObject explosion = Instantiate(boom);
        explosion.transform.position = collision.contacts[0].point;
        explosion.transform.forward = collision.contacts[0].normal;
        ps.GuidedBombHittingSuccess();


        //궁극기 게이지를 채우고 싶다
        tg.ultimate += 10;
        //나를 비활성화 하고싶다
        gameObject.SetActive(false);
    }
    //살아갈 시간
    public float lifeTime = 6f;
    IEnumerator StartLife()
    {
        yield return new WaitForSeconds(lifeTime);
        //날 비활성화 하고
        gameObject.SetActive(false);
        //플레이어의 TestGrad에 리스트에 다시 넣겠다.
        ps.pistolBullet.Add(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, targetRange);
    }
}
