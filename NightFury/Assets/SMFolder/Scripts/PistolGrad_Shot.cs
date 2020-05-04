using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PistolGrad_Shot : MonoBehaviour
{
    //총
    public Transform grabObj_R;
    //손
    public Transform trR;

    //총을 잡고있는지 아닌지 확인할 불변수
    public bool isTouched_R = false;
    public bool isGrabed_R = false;

    //손에서 놓았을때 되돌아갈 원래 부모
    public GameObject camPos;

    Animator anim;
    AudioSource audio;
    //오른손 UI에서 찾을 수 있는 Image
    public Image pressA;

    //총알은 리스트로 관리하자
    public List<GameObject> pistolBullet = new List<GameObject>();
    //총알공장 주소
    public GameObject bulletFactory;
    //총알
    GameObject bullet;
    //총알은 몇개나 만들건데?
    public int amount = 10;
    // Start is called before the first frame update
    void Start()
    {
        anim = grabObj_R.GetComponentInChildren<Animator>();
        audio = grabObj_R.GetComponent<AudioSource>();

        //탄창을 만들자
        for (int i = 0; i < amount; i ++)
        {
            //만들어서 넣고
            bullet = Instantiate(bulletFactory);
            pistolBullet.Add(bullet);
            //비활성화 시켜놓는다
            bullet.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //오른손으로 물체를 잡는지 체크하는 매소드
        GrabedOBJRTouch();
        //오른손으로 물체를 놓았는지 체크하는 매소드
        DropedOBJRTouch();
    }

    //오른손으로 물체를 잡는지 체크하는 매소드
    private void GrabedOBJRTouch()
    {
        //오른손으로 물체를 잡을때
        if (isTouched_R && OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            //총을 내 손에 차일드한다
            grabObj_R.SetParent(trR);
            //다른데 부딪혀서 틀어지지 않도록 키네마틱을 켠다
            grabObj_R.GetComponent<Rigidbody>().isKinematic = true;
            //쥐고 있다는 신호를 준다
            isGrabed_R = true;
            //사용자 입력으로 총을 발사하는 매소드
            PistolGunShot();
        }
    }

    //오른손으로 물체를 놓았는지 체크하는 매소드
    void DropedOBJRTouch()
    {
        //오른손으로 물체를 놓을때
        if (isGrabed_R && OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            //부모한테서 떼어낸다
            grabObj_R.SetParent(camPos.transform);
            grabObj_R.GetComponent<Rigidbody>().isKinematic = false;
            //쥐고있다는 신호는 false
            isGrabed_R = false;
            //닿고있다는 신호는 true로 초기화해서 다시 쥘 수 있게 한다
            isTouched_R = true;
        }
    }

    //발사 가능여부(쿨타임)를 감지할 불변수
    bool canUsePistol = true;
    //총구
    public Transform firPos;
    //사용자 입력으로 총을 발사하는 매소드
    private void PistolGunShot()
    {
        //A버튼을 눌러서 총을 발사한다
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            pressA.fillAmount = 1f;     //스킬버튼을 가림
            StartCoroutine(PistolCoolTime());
            anim.SetTrigger("Shoot");   //반동애니메이션
            audio.Play();
            //만약 탄창에 총알이 있다면
            if (pistolBullet.Count > 0)
            {
                //리스트를 사용하여 오브젝트풀에서 총알하나 뽑자
                bullet = pistolBullet[0];

                //제 위치에 가져다놓고
                bullet.transform.position = firPos.transform.position;
                bullet.transform.forward = firPos.transform.forward;
                //파이어볼의 최대 사거리만큼 레이를 발사해
                //레이사이에 검출되는녀석을 Target으로 만든다.
                //DetectRayCast();

                //활성화 시키고
                bullet.SetActive(true);
                //오디오를 재생시키자
                //GetComponent<AudioSource>().clip = fireball;
                //GetComponent<AudioSource>().Play();
                //부모한테서 떼 내줘야한다.
                //bullet.transform.parent = null;
                //탄창에서 제거하자
                pistolBullet.Remove(bullet);
            }
            canUsePistol = false;     //스킬을 사용했으면 사용할 수 없는 상태로 바꿈
        }
    }

    public float cool = 0.6f;
    IEnumerator PistolCoolTime()
    {
        while(pressA.fillAmount > 0)
        {
            pressA.fillAmount -= 1 * Time.smoothDeltaTime / cool;
            yield return null;
        }
        canUsePistol = true;
        yield break;
    }
    void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Pistol"))
        {
            grabObj_R = coll.transform;
            isTouched_R = true;
        }
    }
}
