using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBS_EnemyBreath : MonoBehaviour
{
    //목표: Enemy Dragon브레스를 쏘고싶다.
    // 필요속성 : 타겟 , 속도 
    public float speed = 3.0f;
    public Transform target;
    Vector3 dir;
    public GameObject destroyEffectPrefab;
    void Start()
    {
        target = GameObject.Find("NightFury").transform;

        dir = target.position - transform.position;
        dir.Normalize();

        /*
        if (SBS_GameManager.GAMEDATA._isPlayerDead == false)
        {
          
        }
        */

    }

    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("NightFury"))
        {        
            GameObject effect = Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity);
            
            Destroy(effect, 3f); 

          //  Destroy(gameObject);
        }       
    }
}
