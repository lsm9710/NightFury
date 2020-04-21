using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestDragonMove : MonoBehaviour
{
    //사용자 앵커의 회전값에 따라 회전시키고 싶다.

    //왼손의 앵커를 저장할 변수
    public GameObject leftCtrlAnchor;

    //무엇을 회전시킬것인가?
    //드래곤
    //드래곤을 저장할 변수
    public GameObject dragon;

    //회전속도
    public float rotSpeed = 5f;

    //고삐를 쥐고 있는지 알아야한다
    TestGrab testGrab;

    //컨트롤러의 회전값을 저장할 변수
    Quaternion lr;

    // Start is called before the first frame update
    void Start()
    {
        testGrab = GetComponent<TestGrab>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckLocalRot();

        //Quaternion dir = new Quaternion(Mathf.FloorToInt(leftCtrlAnchor.transform.rotation.x)
        //    , Mathf.FloorToInt(leftCtrlAnchor.transform.rotation.y)
        //    , Mathf.FloorToInt(leftCtrlAnchor.transform.rotation.z),0);
        //만약 testGrab에 isGrabed가 true 라면 다음 행동을 취하겠다.
        if (testGrab.isGrabed_L == true)
        {
            dragon.transform.rotation = Quaternion.Lerp(transform.rotation, lr, rotSpeed * Time.deltaTime);
            
            //dragon.transform.rotation = Quaternion.Lerp(transform.rotation, dir ,rotSpeed * Time.deltaTime);
        }
    }

    private void CheckLocalRot()
    {
        lr = leftCtrlAnchor.transform.localRotation;
    }
}
