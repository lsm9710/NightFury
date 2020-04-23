using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBS_Player : MonoBehaviour
{
    CharacterController charCtr;

    public float jumpPower = 7.0f;
    float yVelocity;
    float gravity = -9.8f;  
    int jumpCnt;
    public float speed = 7.0f;
    void Start()
    {
        charCtr = GetComponent<CharacterController>();    
    }
    void Update()
    {
        Vector3 dir = SBS_Ctr.GetAxisVec3XZ();
        dir = Camera.main.transform.TransformDirection(dir);

        if (charCtr.collisionFlags == CollisionFlags.Below) yVelocity = 0.0f;

        if (Input.GetButtonDown("Jump"))
        {
            yVelocity = jumpPower;
        }

        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;
        charCtr.Move(dir * speed * Time.deltaTime);
    }
}
