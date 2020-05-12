using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_HP : MonoBehaviour
{
    //최대체력
    public int maxHP = 100;

    //그래서 내 현재체력
    public float currentHP;

    //죽었을때 생성될 게임오버 UI
    public Canvas deathUI;
    //페이드 인아웃할때 사용될 UI
    public Image fadeINOUT;

    public float hp
    {
        get
        {
            return currentHP;
        }
        set
        {
            currentHP = value;
            if (currentHP <= 0)
            {
                print("GameOver");
                StartCoroutine(GameOver());
            }
        }
    }

    private void Start()
    {
        currentHP = maxHP;
        deathUI.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        FadeOutBloodOverlay();
    }

    //화면을 가릴 알파값을 담을 변수
    Color color;
    //화면을 가릴 이미지들
    public Image[] bloodOverlay;
    //회복이 시작되도록 해줄 불값
    public bool isDelayHealingStart = false;

    //피격시에 불릴 함수
    public void FieldOfView()
    {
        //한번에 짠!
        if (bloodOverlay != null)
        {
            for (int i = 0; i < bloodOverlay.Length; i++)
            {
                color = bloodOverlay[i].color;
                color.a = 1;
                bloodOverlay[i].color = color;
            }
        }
    }

    float time = 0f;
    //회복까지 걸리는 시간
    public float delayhealing = 3f;
    //피격시에 동시에 불려서 HP를 서서히 회복할 함수
    void FadeOutBloodOverlay()
    {
        if (isDelayHealingStart)
        {
            time += Time.deltaTime;

            if (time <= delayhealing)
            {
                StartCoroutine(NaturalHealing());
                //bloodOverlay의 컬러를 받아와서
                for (int i = 0; i < bloodOverlay.Length; i++)
                {
                    color = bloodOverlay[i].color;
                    //알파를 줄여한다.
                    color = new Color(0.8207547f, 0.1070411f, 0.04258633f, 1f - time / delayhealing);
                    bloodOverlay[i].color = color;
                }
            }
            else
            {
                isDelayHealingStart = false;
                time = 0f;
                for (int i = 0; i < bloodOverlay.Length; i++)
                {
                    color = bloodOverlay[i].color;
                    color = new Color(0.8207547f, 0.1070411f, 0.04258633f, 0);
                    bloodOverlay[i].color = color;
                }
            }
        }
    }

    IEnumerator NaturalHealing()
    {
        yield return new WaitForSeconds(1f);
        if (currentHP <= maxHP)
        {
            currentHP += Time.deltaTime;
        }
        else
        {
            currentHP = maxHP;
            StopCoroutine(NaturalHealing());
        }
    }

    public float fadeInTime = 2f;
    public float fadeOutTime = 2f;
    //게임오버시에 실행될 시퀀스
    IEnumerator GameOver()
    {
        //죽는 사운드
        //화면 페이드 아웃
        while (fadeINOUT.color.a < 1f)
        {
            Color c = fadeINOUT.color;
            c.a+= (Time.deltaTime/ fadeInTime);
            fadeINOUT.color = c;
            yield return null;
        }
        deathUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);

        while (fadeINOUT.color.a > 0)
        {
            Color v = fadeINOUT.color;
            v.a -= (Time.deltaTime / fadeOutTime);
            fadeINOUT.color = v;
            yield return null;
        }
    }
}
