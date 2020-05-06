using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impact : MonoBehaviour
{
    //임팩트 파티클을 저장할 변수
    public GameObject impact;
    //부모 컴포넌트
    public PhotonShot ph;
    TestGrab tg;

    private void Start()
    {
#if UNITY_PC
        ph = gameObject.GetComponentInParent<PhotonShot>();
        tg = GameObject.Find("Camera").GetComponent<TestGrab>();
#else
        ph = gameObject.GetComponentInParent<PhotonShot>();
        tg = GameObject.Find("OVRControllerPrefab_L").GetComponent<TestGrab>();
#endif
    }
    public void OnCollisionEnter(Collision collision)
    {
        //무언가에 충돌하면 impact를 켜준다
        impact.SetActive(true);
        print(collision.gameObject.name);
        ph.isImpact = true;
        //무언가에 충돌할때마다 궁게이지를 채워주고싶다
        //궁게이지는 무엇이고, 어디에다 채워줘야하나?
        tg.ultimate += 10;
    }
}
