using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour
{
    //카메라에서 Ray를 발사해서 부딪힌 자리에 Crosshair 이미지를 배치하고 싶다.
    Ray ray;

    RaycastHit hit;

    Transform myTransform;

    public Image ch;
    
    // Start is called before the first frame update
    void Start()
    {
        myTransform = gameObject.transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        CrossHairRay();
    }

    private void CrossHairRay()
    {
        //1. 카메라에서 발사하는 ray만들기
        ray = new Ray(myTransform.position, myTransform.forward);
        //2. 만약에 부딪힌 놈이 있으면
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                //3. Crosshair를 부딪힌 놈의 위치에 위치시키자
                transform.position = hit.point;
                //4. 거리에 따라서 Crosshair 이미지의 크기를 변경해준다.
                //4-1 카메라 - Crosshair의 거리를 구하자
                float dist = Vector3.Distance(myTransform.position, transform.position);
                //4-2 거리 비례해서 Crosshair의 크기를 변경하자
                transform.localScale = dist * Vector3.one;
                ch.color = new Color(ch.color.r, ch.color.g, ch.color.b, 1f);
            }
            else
            {
                ch.color = new Color(ch.color.r, ch.color.g, ch.color.b, 0f);
            }
        }
    }
}
