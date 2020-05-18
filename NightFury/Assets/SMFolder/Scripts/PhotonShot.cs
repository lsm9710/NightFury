using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonShot : MonoBehaviour
{
    //앞으로 계속 날아가고 싶다
    //날아갈 속도
    float speed;
    //드래곤의 속도에 더해줄 추가적인 속도
    public float plusSpeed;
    //충돌 여부를 체크할 불값
    public bool isImpact = false;
    //TestGrab을 불러올 변수
    TestGrab testgrab;
    TestDragonMove tm;
    //여기서 다시 임팩트를 꺼줘야하니까 임팩트오브젝트를 저장할 변수
    public GameObject impact;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_PC
        //testgrab = GameObject.Find("Camera").GetComponent<TestGrab>();
        //tm = GameObject.Find("Camera").GetComponent<TestDragonMove>();
        //StartCoroutine(StartLife());
#else
        testgrab = GameObject.Find("OVRControllerPrefab_L").GetComponent<TestGrab>();
        tm = GameObject.Find("OVRControllerPrefab_L").GetComponent<TestDragonMove>();
        
#endif
    }
    private void OnEnable()
    {
#if UNITY_PC
        testgrab = GameObject.Find("Camera").GetComponent<TestGrab>();
        tm = GameObject.Find("Camera").GetComponent<TestDragonMove>();
#else
        testgrab = GameObject.Find("OVRControllerPrefab_L").GetComponent<TestGrab>();
        tm = GameObject.Find("OVRControllerPrefab_L").GetComponent<TestDragonMove>();
#endif
        StartCoroutine(StartLife());
        speed = tm.moveSpeed + plusSpeed;
        //불값 초기화
        isImpact = false;
    }

    // Update is called once per frame
    void Update()
    {
        //만약 충돌하고나면 
        if (isImpact)
        {
            StartCoroutine(Disable());
        }
        else
        {
            //날아갈 방향
            Vector3 dir = transform.forward;
            transform.position += dir * speed * Time.deltaTime;
        }
    }
    //파티클이 전부 꺼지는 시점
    public float endParticl = 1.5f;
    IEnumerator Disable()
    {
        yield return new WaitForSeconds(endParticl);
        //플레이어의 TestGrad에 리스트에 다시 넣겠다.
        testgrab.fireBallListPool.Add(gameObject);
        //날 비활성화 하고
        impact.SetActive(false);
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
        testgrab.fireBallListPool.Add(gameObject);
    }
    public void OnCollisionEnter(Collision collision)
    {
        //무언가에 충돌하면 impact를 켜준다
        impact.SetActive(true);
        //트레일은 꺼야한다
        //충돌한게 에너미일때만 점수를 추가한다

        print(collision.gameObject.name);
        isImpact = true;
        print(isImpact);
        //무언가에 충돌할때마다 궁게이지를 채워주고싶다
        //궁게이지는 무엇이고, 어디에다 채워줘야하나?
        testgrab.ultimate += 10;
        //충돌한게 에너미일때만 점수를 채워준다
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            ScoreManager.instance.Score += 30;
        }
    }
}
