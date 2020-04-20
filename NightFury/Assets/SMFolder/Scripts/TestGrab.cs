using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGrab : MonoBehaviour
{
    //고삐 콜라이더에 닿았을때 사용자의 조작에 따라 물체를 쥐겠다
    //고삐
    public Transform grabObj_L;
    public Transform grabObj_R;
    //손
    public Transform trL;
    public Transform trR;

    private bool isTouched_L = false;
    private bool isTouched_R = false;
    public bool isGrabed_L = false;
    private bool isGrabed_R = false;

    // Update is called once per frame
    void Update()
    {
        //왼손으로 물체를 잡을때
        if (isTouched_L && OVRInput.Get(OVRInput.Button.PrimaryHandTrigger,OVRInput.Controller.LTouch))
        {
            grabObj_L.SetParent(trL);
            grabObj_L.GetComponent<Rigidbody>().isKinematic = true;
            isGrabed_L = true;
        }

        //오른손으로 물체를 잡을때
        if (isTouched_R && OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            grabObj_R.SetParent(trR);
            grabObj_R.GetComponent<Rigidbody>().isKinematic = true;
            isGrabed_R = true;
            print("coll.CompareTag(Ball_R)");
        }

        //왼손으로 물체를 놓을때
        else if (isGrabed_L && OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger))
        {
            grabObj_L.SetParent(null);
            Vector3 _velocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch);
            grabObj_L.GetComponent<Rigidbody>().velocity = _velocity;
            grabObj_L.GetComponent<Rigidbody>().isKinematic = false;

            isGrabed_L = false;
            isTouched_L = false;
            grabObj_L = null;
        }
        //오른손으로 물체를 놓을때
        else if (isGrabed_R && OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger))
        {
            grabObj_R.SetParent(null);
            Vector3 _velocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);
            grabObj_R.GetComponent<Rigidbody>().velocity = _velocity;
            grabObj_R.GetComponent<Rigidbody>().isKinematic = false;

            isGrabed_R = false;
            isTouched_R = false;
            grabObj_R = null;
        }
    }
    void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Ball_L"))
        {
            grabObj_L = coll.transform;
            isTouched_L = true;
        }
        if (coll.CompareTag("Ball_R"))
        {
            grabObj_R = coll.transform;
            print("coll.CompareTag(Ball_R)");
            isTouched_R = true;
        }
    }
}
