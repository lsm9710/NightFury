using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SBS_RiderMove : MonoBehaviour
{
    // 목표 : 움직이고 싶다.
    //필요속성 : 속도,캐릭터컨트롤

    public float moveSpeed = 7.0f;
    public float jumpPower = 5.0f;
    public float gravity = -10.0f;
    float yVelocity = 0;
    CharacterController charCtr;
    void Start()
    {
        charCtr = GetComponent<CharacterController>();    
    }
    
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);

        dir = Camera.main.transform.TransformDirection(dir);

        if (charCtr.collisionFlags == CollisionFlags.Below) yVelocity = 0;

        if (Input.GetButtonDown("Jump")) yVelocity = jumpPower;

        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        charCtr.Move(dir * moveSpeed * Time.deltaTime);
    }
}
