using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haptic : MonoBehaviour
{
    public float time;
    public float frequ;
    public float amplit;
    public float frequ_1;
    public float amplit_1;
    public float frequ_2;
    public float amplit_2;
    public float frequ_3;
    public float amplit_3;


    bool isFirst = true;
    // Start is called before the first frame update
    void Start()
    {
        //OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
        StartCoroutine(VibrateController());

    }

    // Update is called once per frame
    void Update()
    {
       


        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.LTouch))
        {
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.LTouch);
        }
        else if (OVRInput.GetUp(OVRInput.Button.Two, OVRInput.Controller.LTouch))
        {
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);

            
        }
        
    }
    IEnumerator VibrateController(/*float waitTime, float frequenct, float amplitude, OVRInput.Controller controller*/)
    {
        //isFirst = false;

        //while (true)
        //{

        yield return new WaitForSeconds(10);
        Debug.Log("1111111111111111111");
        OVRInput.SetControllerVibration(frequ, amplit, OVRInput.Controller.LTouch);

        yield return new WaitForSeconds(time);
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
        Debug.Log("2222222222222222222");
        
        Debug.Log("3333333333333333333");
        OVRInput.SetControllerVibration(frequ_1, amplit_1, OVRInput.Controller.LTouch);
        yield return new WaitForSeconds(time);
        Debug.Log("4444444444444444444444");
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
        yield return new WaitForSeconds(time);
        Debug.Log("555555555555555555555555");
        OVRInput.SetControllerVibration(frequ_2, amplit_2, OVRInput.Controller.LTouch);
        yield return new WaitForSeconds(time);
        Debug.Log("6666666666666666666666");
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
        yield return new WaitForSeconds(time);
        Debug.Log("77777777777777777777");
        OVRInput.SetControllerVibration(frequ_3, amplit_3, OVRInput.Controller.LTouch);
        //yield return new WaitForSeconds(2);
        //OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
        //}
        //isVibrate = true;
    }
}
