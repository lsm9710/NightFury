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
    //드래곤의 Rigidbody를 저장할 변수
    Rigidbody rbDragon;

    //회전속도
    public float rotSpeed = 5f;
    //전진속도
    public float moveSpeed = 10f;
    //감소속도? 마찰력?
    public float drag = 1.0f;

    //고삐를 쥐고 있는지 알아야한다
    TestGrab testGrab;
    public Transform leftHand;

    //컨트롤러의 회전값을 저장할 변수
    Quaternion lr;

    // Start is called before the first frame update
    void Start()
    {
        testGrab = GetComponent<TestGrab>();
        rbDragon = dragon.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //CheckLocalRot();
        RotTheDragon();
    }

    public void GoForward()
    {
        Acceleration();
        
        drag = 1.0f;
        //rbDragon.AddForce(new Vector3(0, 0, 1), ForceMode.Impulse);
        moveSpeed = moveSpeed * drag;
        dragon.transform.localPosition += dragon.transform.forward * moveSpeed * Time.deltaTime;
        print(moveSpeed);
    }

    float currentTime;
    public float stack1;
    public float stack2;
    public float stack3;
    private void Acceleration()
    {
        //currentTime += Time.deltaTime;
        moveSpeed += 2.5f;
        //if (moveSpeed >= 30f)
        //{

        //}
    }


    public void SlowDown()
    {

        drag = 0.98f;
        moveSpeed = moveSpeed * drag;
        dragon.transform.localPosition += dragon.transform.forward * moveSpeed * Time.deltaTime;
    }

    private void RotTheDragon()
    {
        //만약 testGrab에 isGrabed가 true 라면 다음 행동을 취하겠다.

        if (testGrab.isGrabed_L == true)
        {
            //dragon.transform.rotation = Quaternion.Lerp(transform.localRotation, lr, rotSpeed * Time.deltaTime);
            dragon.transform.rotation = leftHand.localRotation;
            //GoForward();
        }
    }

    private float Anglefunction(Vector3 v1, Vector3 v2)
    {
        float x = Mathf.Acos(Vector3.Dot(v1, v2))/* * Mathf.Rad2Deg*/;
        return x;
    }

    //private void CheckLocalRot()
    //{
    //    lr = leftCtrlAnchor.transform.localRotation;
    //}

}
