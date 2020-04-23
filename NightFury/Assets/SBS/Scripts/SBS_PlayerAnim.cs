using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SBS_PlayerAnim : MonoBehaviour
{
    Animator animPlayer;
    void Start()
    {
        animPlayer = GetComponent<Animator>();
    }

    void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        animPlayer.SetFloat("Direction_V", v);
        animPlayer.SetFloat("Direction_H", h);

        if (Input.GetButtonDown("Jump"))
        {
            animPlayer.SetTrigger("JumpUp");
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetMouseButton(0))
        {
            animPlayer.SetBool("FireAttack", true);
        }
        else
        {
            animPlayer.SetBool("FireAttack", false);
        }
      
        if (Input.GetMouseButton(1))
        {
            animPlayer.SetBool("Attack", true);
            SBS_UIManager.Instance.GameTitleStart();
        }
        else
        {
            animPlayer.SetBool("Attack", false);
        }
       
        if(Input.GetMouseButtonDown(0))
        {
            animPlayer.SetFloat("ClawAttack", 1.0f);
        }
        else
        {
            animPlayer.SetFloat("ClawAttack", 0.0f);
        }
        if(Input.GetMouseButton(0))
        {
            animPlayer.SetFloat("FlameAttack", 1.0f);
        }
        else
        {
            animPlayer.SetFloat("FlameAttack", 0.0f);
        }
    }
}
