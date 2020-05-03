using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonShot : MonoBehaviour
{
    //앞으로 계속 날아가고 싶다
    //날아갈 속도
    public float speed;
    //충돌 여부를 체크할 불값
    public bool isImpact = false;
    //TestGrab을 불러올 변수
    TestGrab testgrab;

    // Start is called before the first frame update
    void Start()
    {
        testgrab = GameObject.Find("OVRControllerPrefab_L").GetComponent<TestGrab>();
        StartCoroutine(StartLife());
    }

    // Update is called once per frame
    void Update()
    {
        //날아갈 방향
        Vector3 dir = transform.forward;
        transform.position += dir * speed * Time.deltaTime;
        
        //만약 충돌하고나면 
        if (isImpact)
        {
            StartCoroutine(Disable());
        }
    }
    //파티클이 전부 꺼지는 시점
    public float endParticl = 1.5f;
    IEnumerator Disable()
    {
        yield return new WaitForSeconds(endParticl);
        //날 비활성화 하고
        gameObject.SetActive(false);
        //플레이어의 TestGrad에 리스트에 다시 넣겠다.
        testgrab.fireBallListPool.Add(gameObject);
        //불값 초기화
        isImpact = false;
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
}
