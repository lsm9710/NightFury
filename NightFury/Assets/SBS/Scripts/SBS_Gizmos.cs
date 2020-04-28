using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBS_Gizmos : MonoBehaviour
{
    //목표 : 적 생성 포인트 지점을 표시하고 싶다.

       // 필요속성 : 기즈모
    public enum Type
    {
      NORMAL,
      WAYPOINT 
    }
    public Type type = Type.NORMAL;

    public Color _color = Color.blue;
    public float _radius = 0.1f;

    private void OnDrawGizmos()
    {
        if (type == Type.NORMAL)
        {
        
            Gizmos.color = _color;
      
            Gizmos.DrawSphere(transform.position, _radius);
        }
        else
        {
            Gizmos.color = _color;
        
        }

    }
}
