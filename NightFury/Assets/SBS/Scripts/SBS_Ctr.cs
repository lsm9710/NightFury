using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBS_Ctr
{ 
    public static Vector3 GetAxisVec3XZ()
    {

        Vector3 v = new Vector3(0, 0, 0);
        v.x = Input.GetAxis("Horizontal");
        v.z = Input.GetAxis("Vertical");

        return v;
    }

    public static void Move(Transform tr, Vector3 dir, float speed)
    {
        tr.position += dir.normalized * speed * Time.deltaTime;
    }
}
