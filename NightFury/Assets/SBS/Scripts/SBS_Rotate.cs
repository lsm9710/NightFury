using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBS_Rotate : MonoBehaviour
{
    float mx;
    float my;
    public float rotSpeed = 300.0f;
    public float MinAngleY = -80.0f;
    public float MaxAngleY = 80.0f;
    void Update()
    {
      
        float h = GetAxisVec2XY().x;
        float v = GetAxisVec2XY().y;

        Rotate(h, v, ref mx, ref my);
       
        my = Mathf.Clamp(my, MinAngleY, MaxAngleY);

        transform.eulerAngles = new Vector3(-my, mx, 0);
    }

    void Rotate(float h, float v, ref float mx, ref float my)
    {
        mx += h * rotSpeed * Time.deltaTime;
        my += v * rotSpeed * Time.deltaTime;
    }

    Vector2 GetAxisVec2XY()
    {
        Vector2 v = new Vector2(0, 0);
        v.x = Input.GetAxis("Mouse X");
        v.y = Input.GetAxis("Mouse Y");
        return v;
    }



}