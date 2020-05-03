using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impact : MonoBehaviour
{
    //임팩트 파티클을 저장할 변수
    public GameObject impact;
    //부모 컴포넌트
    public PhotonShot ph;

    private void Start()
    {
        ph = gameObject.GetComponentInParent<PhotonShot>();
    }
    public void OnCollisionEnter(Collision collision)
    {
        //무언가에 충돌하면 impact를 켜준다
        impact.SetActive(true);
        print(collision.gameObject.name);
        ph.isImpact = true;
    }
}
