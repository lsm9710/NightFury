using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SBS_Crosshair : MonoBehaviour
{
    public GameObject rayFirePos;
    public GameObject crossHair;
    public GameObject CrossHairPos;
    public GameObject hairCircle;
    public GameObject hairCross;
    public GameObject hairCrossImgObj;
    public GameObject bulletEffectFactory;
    GameObject bullEffect;  
   
    Image hairCircleColor;
    Image hairCrossColor;

    Image crossHair_Color;
 
    RaycastHit hitInfo;
    Ray ray;
 
    Vector3 orginScale;
    Vector3 ScreenCenter;

    int layerMask = 1 << LayerMask.NameToLayer("Enemy");
    public float crossScaleRatio = 0.25f;
    private float currentTime;
    private float lrTime = 0.2f;

    void Start()
    {
       // ScreenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
      // orginScale = crossHair.transform.localScale;

        //CrossHairPos = GameObject.Find("CrossHairPosition");
        //rayFirePos = GameObject.Find("RayFirePos");
        //crossHair = GameObject.Find("NightFury/Main Camera/CrossHair");
        //hairCircle = GameObject.Find("Circle");

       hairCircleColor = hairCircle.GetComponent<Image>();
       hairCrossColor = hairCross.GetComponent<Image>();
       crossHair_Color = hairCrossImgObj.GetComponent<Image>();
    }

    void Update()
    {
        crossHair.transform.position = CrossHairPos.transform.position;
       // crossHair_Color.color = Color.blue;
        //hairCircleColor.color = Color.white;
        //hairCrossColor.color = Color.white;
        // hairCircleColor.color = Color.cyan;
        //  hairCrossColor.color = Color.cyan;

        ray = new Ray(rayFirePos.transform.position, rayFirePos.transform.forward);
    
        RayFire();
    
        crossHair.transform.LookAt(Camera.main.transform);
    }
   
    public void RayFire()
    {
        crossHair.transform.position = CrossHairPos.transform.position; 
       // Vector3 hairCircleScale = new Vector3(9.0f, 9.0f, 9.0f);
       // crossHair.transform.localScale = hairCircleScale;
       

        if (Physics.Raycast(ray, out hitInfo, 50.0f, 1 << LayerMask.NameToLayer("Enemy")))
        {
          //  crossHair_Color.color = Color.blue;

            Debug.DrawRay(rayFirePos.transform.position, rayFirePos.transform.forward * 50.0f, Color.blue, 0.3f);
           
            crossHair.transform.position = hitInfo.point;

            float dist = Vector3.Distance(Camera.main.transform.position, hitInfo.point);

            // crossHair.transform.localScale = orginScale * dist * crossScaleRatio;

            crossHair_Color.color = Color.cyan;

            hairCircleColor.color = Color.cyan;
            hairCrossColor.color = Color.cyan;

        

            if (hitInfo.transform.tag == "ENEMY")
            {
                // Vector3 hairScale = new Vector3(9.0f, 9.0f, 9.0f);
                //hairCircle.transform.localScale = hairScale;
            }
            else
            {

            }

        }
        else
        {
           // hairCircleColor.color = Color.white;
          //  hairCrossColor.color = Color.white;

        }
    
        if (Input.GetMouseButtonDown(2))
        {      
            if (!hitInfo.transform.CompareTag("CrossHair"))
            {
               
                GameObject effect = Instantiate(bulletEffectFactory);
                effect.transform.position = hitInfo.point;
                effect.transform.forward = hitInfo.normal;
                Destroy(effect, 1f);
            }
        }
     
    }
}
