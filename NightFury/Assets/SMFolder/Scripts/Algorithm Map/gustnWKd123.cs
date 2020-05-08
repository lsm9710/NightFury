using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class gustnWKd123 : MonoBehaviour
{
    //일정갯수 맹글어서 그거 반반 찢어서 리스트에 넣는거?
    public int how_Many_Make = 100;
    GameObject 만든색기;
    List<GameObject> 담을색기 = new List<GameObject>();
    List<GameObject> 담을색기1 = new List<GameObject>();
    List<GameObject> 담을색기2 = new List<GameObject>();


    private void Start()
    {
        for (int i = 0; i < how_Many_Make; i++)
        {
            만든색기 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            만든색기.transform.position = transform.position;
            담을색기.Add(만든색기);
        }



    }

}
