using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullet : MonoBehaviour
{
    public float speed = 80f;
    PistolGrad_Shot ps;
    // Start is called before the first frame update
    void Start()
    {
        ps = GameObject.Find("OVRControllerPrefab_R").GetComponent<PistolGrad_Shot>();
        StartCoroutine(StartLife());
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
        //다시 리스트에 넣고싶다
        ps.pistolBullet.Add(gameObject);
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
