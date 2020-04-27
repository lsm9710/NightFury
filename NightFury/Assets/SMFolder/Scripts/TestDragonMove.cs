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
    public float rotSpeed = 0.05f;
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
        if (moveSpeed <= 80f)
        {
            moveSpeed += acceleration;
            if (moveSpeed >= 80f)
            {
                moveSpeed = 80f;
            }
        }

        drag = 1.0f;
        //rbDragon.AddForce(new Vector3(0, 0, 1), ForceMode.Impulse);
        moveSpeed *= drag;
        dragon.transform.localPosition += dragon.transform.forward * moveSpeed * Time.deltaTime;

    }

    public float acceleration = 2.5f;
    public float deceleration = 0.98f;

    public void SlowDown()
    {

        drag = deceleration;
        moveSpeed *= drag;
        dragon.transform.localPosition += dragon.transform.forward * moveSpeed * Time.deltaTime;
    }

    //public float offset = 0.05f;
    Vector3 angle;
    //상한궤도
    public float orbit = 20f;
    private void RotTheDragon()
    {
        
        //만약 testGrab에 isGrabed가 true 라면 다음 행동을 취하겠다.

        if (testGrab.isGrabed_L == true)
        {
            angle = leftHand.localEulerAngles;

            if (180 < angle.x && angle.x <= 360) angle.x -= 360;
            if (180 < angle.y && angle.y <= 360) angle.y -= 360;
            if (180 < angle.z && angle.z <= 360) angle.z -= 360;

            if (Mathf.Abs(angle.x) < orbit) angle.x = 0f;
            if (Mathf.Abs(angle.y) < orbit) angle.y = 0f;
            if (Mathf.Abs(angle.z) < orbit) angle.z = 0f;

            dragon.transform.Rotate(transform.right, angle.x * rotSpeed, Space.World);
            dragon.transform.Rotate(transform.up, angle.y * rotSpeed, Space.World);
            dragon.transform.Rotate(transform.forward, angle.z * rotSpeed, Space.World);
        }
        //dragon.transform.rotation = Quaternion.Lerp(transform.localRotation, lr, rotSpeed * Time.deltaTime);
        //  dragon.transform.rotation = leftHand.localRotation;

        //float x= Anglefunction(Vector3.forward, transform.forward);
        //x *= offset;
        //rotSpeed += x;

        //dragon.transform.rotation = Quaternion.Euler(rotSpeed, 0, 0);

        //print("Angle:" + angle);
        //GoForward();
    }
    }

    //private void Anglefunction()
    //{
        //float x = Mathf.Acos(Vector3.Dot(v1, v2)) * Mathf.Rad2Deg;
        //x = Mathf.Clamp(x, 30, 80);

        //print(x + "!!!!!!!!!!!!");
        //return x;
       

    //private void CheckLocalRot()
    //{
    //    lr = leftCtrlAnchor.transform.localRotation;
    //}

//}
