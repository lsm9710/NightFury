using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gustnWkd123 : MonoBehaviour
{
    public GameObject cs;

    private void Update()
    {
        Blabla(transform.position.x ,  transform.forward);
    }

    private void Blabla(float cc,Vector3 b)
    {
        Vector3 a = cs.transform.position - transform.position;

        float aaa = Vector3.Angle(a, b);


        if (aaa < 30)
        {
            print("쿠폰 내꺼");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, cs.transform.position);
    }
}
