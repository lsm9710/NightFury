using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIRE : MonoBehaviour
{
    //화염방사 이펙트를 저장할 변수
    public ParticleSystem fiiiiiire;
    //진동을 조절할 변수들
    public float time, fre, amp;

    //화염발사
    public void FlameThrow()
    {
        //화염방사 이펙트를 실행시킨다.
        fiiiiiire.Play();
        //fireLight.enabled = true;
    }

    public void FlameStop()
    {
        fiiiiiire.Stop();
        //fireLight.enabled = false;
    }

    public void Viveration()
    {
        StartCoroutine(FlamViveration(time, fre, amp, OVRInput.Controller.LTouch));
    }
        IEnumerator FlamViveration(float time, float frequ, float amplit, OVRInput.Controller controller)
        {
            OVRInput.SetControllerVibration(frequ, amplit, controller);

            yield return new WaitForSeconds(time);
            OVRInput.SetControllerVibration(0, 0, controller);
        }
}
