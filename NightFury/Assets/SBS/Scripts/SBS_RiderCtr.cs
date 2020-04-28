using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

//목표 : 드래곤 알을 키우고싶다 , 오프닝을 재생시키고 싶다

 //속성 : 오른손 , 잡기, 놓기
public class SBS_RiderCtr : MonoBehaviour
{ 
    public Transform rightHand;
    public float catchRange = 10.1f;
    public float rayDist = 7.0f; 
    public float throwPower = 5;
    Transform grabTr = null;
    public VideoPlayer my_video;

    void Update()
    {
        GrabObj();
        DrobObj();
    }
    void GrabObj()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            int layer = 1 << LayerMask.NameToLayer("Start");

            Ray ray = new Ray(rightHand.position, rightHand.forward);
            RaycastHit[] hit;
            hit = Physics.SphereCastAll(ray, catchRange, rayDist, layer);

            if (hit.Length > 0)
            {

                SceneManager.LoadScene("NightFury_v01");

                // my_video.Play();
            }
        }
    }
    void DrobObj()
    {
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            if (grabTr != null)
            {
                grabTr.parent = null;
                grabTr = null;
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(rightHand.position, rightHand.position + rightHand.forward * rayDist);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(rightHand.position, catchRange);
    }
}

