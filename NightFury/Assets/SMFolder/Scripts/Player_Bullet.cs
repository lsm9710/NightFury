using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullet : MonoBehaviour
{
    float speed;
    //드래곤의 속도에 추가로 더해줄 총알의 속도
    public float plusSpeed = 20f;
    PistolGrad_Shot ps;
    TestDragonMove tm;
    TestGrab tg;
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_PC
        ps = GameObject.Find("Camera").GetComponent<PistolGrad_Shot>();
        tm = GameObject.Find("Camera").GetComponent<TestDragonMove>();
        tg = GameObject.Find("Camera").GetComponent<TestGrab>();
        speed = tm.moveSpeed + plusSpeed;
        StartCoroutine(StartLife());
#else
        ps = GameObject.Find("OVRControllerPrefab_R").GetComponent<PistolGrad_Shot>();
        tm = GameObject.Find("OVRControllerPrefab_L").GetComponent<TestDragonMove>();
        tg = GameObject.Find("OVRControllerPrefab_L").GetComponent<TestGrab>();
        speed = tm.moveSpeed + plusSpeed;
        StartCoroutine(StartLife());
#endif
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = transform.forward;
        transform.position += dir * speed * Time.deltaTime;
    }

    //무언가에 충돌했으면
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("뭐야?");
        //다시 리스트에 넣고싶다
        ps.pistolBullet.Add(gameObject);
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
}
