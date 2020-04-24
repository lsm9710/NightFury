using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIRE : MonoBehaviour
{
    //화염방사 이펙트를 저장할 변수
    public ParticleSystem fiiiiiire;
    //화염방사의 라이트를 저장할 변수
    //public Light fireLight;

    //private void Start()
    //{
    //    fireLight.enabled = false;
    //}

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
}
