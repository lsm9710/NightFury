using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 카메라에서 Ray를 발사해서 부딪힌 자리에 Crosshair 이미지를 배치하고 싶다
public class Crosshair : MonoBehaviour
{
    // 안녕하세요
    public float customScale = 1;

    Transform mainCamTr;

    void Start()
    {
        mainCamTr = Camera.main.transform;
    }
    void Update()
    {
        // 1. 카메라에서 발사하는 ray 만들기
        Ray ray = new Ray(mainCamTr.position, mainCamTr.forward);

        // 2. 만약에 부딪힌 놈이 있으면
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            // 3. Crosshair 를 부딪힌 놈의 위치에 위치시키자
            transform.position = hit.point;
            // 4. 거리에 따라서 Crosshair 이미지의 크기를 변경해준다
            // 4-1 카메라 - Crosshair의 거리를 구하자
            float dist = Vector3.Distance(mainCamTr.position, transform.position);
            // 4-2 거리 비례해서 Crosshair의 크기를 변경하자
          //  transform.localScale = dist * Vector3.one * customScale;
        }
    }
}
