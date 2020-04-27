using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGrab : MonoBehaviour
{
    //가속과 감속 상태를 제어할 State 만들기
    public enum MoveState
    {
        Idle,
        Flight,
        SlowDown,
        FlameThrow
    }
    public MoveState state;

    //고삐 콜라이더에 닿았을때 사용자의 조작에 따라 물체를 쥐겠다
    //고삐
    public Transform grabObj_L;
    //public Transform grabObj_R;
    //손
    public Transform trL;
    public Transform trR;

    public bool isTouched_L = false;
    public bool isTouched_R = false;
    public bool isGrabed_L = false;
    public bool isGrabed_R = false;

    //레이를 발사할 오리진 포스
    public Transform dragonMouth;
    //레이를 발사할 거리
    public float rayDist = 5.5f;
    //물체를 감지할 범위
    public float catchRange = 5f;

    //파이어볼 공장
    public GameObject firBallFactory;
    //발사할 위치는 레이를 발사하는 곳에서 시작한다.
    [HideInInspector]
    public List<GameObject> fireBallListPool = new List<GameObject>();

    //탄창은 몇발로 할건데?
    int amount = 10;

    Animator anim;

    public GameObject camPos;
    TestDragonMove tdm;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
        tdm = GetComponent<TestDragonMove>();


        //리스트로 탄창을 만들어 보자
        for (int i = 0; i < amount; i++)
        {
            //만들어서 넣고
            bullet = Instantiate(firBallFactory);
            fireBallListPool.Add(bullet);
            //비활성화 시킨다
            bullet.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //왼손으로 물체를 잡을때
        GrabedOBJLTouch();
        //오른손으로 물체를 잡을때
        //GrabedOBJRTouch();
        //왼손으로 물체를 놓을때
        DropedOBJLTouch();
        //오른손으로 물체를 놓을때
        //DropedOBJRTouch();

        Switch();
    }

    void Switch()
    {
        switch (state)
        {
            case MoveState.Idle:
                break;
            case MoveState.Flight:
                tdm.GoForward();
                break;
            case MoveState.SlowDown:
                tdm.SlowDown();
                break;
            case MoveState.FlameThrow:
                anim.SetTrigger("FlameThrow");
                ShootTheRay();
                break;
        }
    }

    //모든걸 태워버릴 레이 발사
    private void ShootTheRay()
    {
        int layer = 1 << LayerMask.NameToLayer("Enmy");

        Collider[] hit = Physics.OverlapSphere(dragonMouth.position, catchRange, layer);
        if (hit.Length > 0)
        {
            //충돌한 물체의 이름을 적어보자
        }
    }


    //private void DropedOBJRTouch()
    //{
    //    //오른손으로 물체를 놓을때
    //    if (isGrabed_R && OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger))
    //    {
    //        grabObj_R.SetParent(null);
    //        Vector3 _velocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);
    //        grabObj_R.GetComponent<Rigidbody>().velocity = _velocity;
    //        grabObj_R.GetComponent<Rigidbody>().isKinematic = false;

    //        isGrabed_R = false;
    //        isTouched_R = true;
    //        grabObj_R.parent = camPos.transform;
    //    }
    //}

    private void DropedOBJLTouch()
    {
        //왼손으로 물체를 놓을때
        if (isGrabed_L && OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger))
        {
            grabObj_L.SetParent(null);
            //Vector3 _velocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch);
            //grabObj_L.GetComponent<Rigidbody>().velocity = _velocity;
            grabObj_L.GetComponent<Rigidbody>().isKinematic = false;

            isGrabed_L = false;
            isTouched_L = true;
            grabObj_L.parent = camPos.transform;
        }
    }

    //private void GrabedOBJRTouch()
    //{
    //    //오른손으로 물체를 잡을때
    //    if (isTouched_R && OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
    //    {
    //        grabObj_R.SetParent(trR);
    //        grabObj_R.GetComponent<Rigidbody>().isKinematic = true;
    //        isGrabed_R = true;
    //    }
    //}

    private void GrabedOBJLTouch()
    {
        //왼손으로 물체를 잡을때
        if (isTouched_L && OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
        {
            grabObj_L.SetParent(trL);
            grabObj_L.GetComponent<Rigidbody>().isKinematic = true;
            isGrabed_L = true;
            PullThrottleOrNot();
            FireBall();
            FlameThrow();
        }
    }

    GameObject bullet;
    //파이어볼을 쏠거야
    private void FireBall()
    {
        //X버튼을 누를때 파이어볼을 쏠거야
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch))
        {
            //만약 탄창에 총알이 있다면
            if (fireBallListPool.Count > 0)
            {
                //리스트를 사용하여 오브젝트풀에서 총알하나 뽑자
                bullet = fireBallListPool[0];

                //제 위치에 가져다놓고
                bullet.transform.position = dragonMouth.transform.position;
                bullet.transform.forward = dragonMouth.transform.forward;
                //파이어볼의 최대 사거리만큼 레이를 발사해
                //레이사이에 검출되는녀석을 Target으로 만든다.
                DetectRayCast();

                //활성화 시키고
                bullet.SetActive(true);
                //탄창에서 제거하자
                fireBallListPool.Remove(bullet);
            }
        }
    }

    public EffectSettings effectSettings;
    private void DetectRayCast()
    {
        RaycastHit hit;
        if (Physics.Raycast(dragonMouth.transform.position, dragonMouth.transform.forward,out hit) && hit.collider.tag != "Wall")
        {
            effectSettings = bullet.GetComponentInChildren<EffectSettings>();

            effectSettings.Target = hit.collider.gameObject;
        }
    }

    //스로틀을 당기고 있는지 여부를 체크하는 함수
    public void PullThrottleOrNot()
    {
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
        {
            state = MoveState.Flight;
        }
        else if (!OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
        {
            state = MoveState.SlowDown;
        }
    }

    
    private void FlameThrow()
    {
        //Y를 누르면 화염방사를 실행하고싶다.
        if (/*OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch)
            && */OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.LTouch))
        {
            //공격애니메이션을 실행시킨다.
            state = MoveState.FlameThrow;
        }
    }


    //화염방사 이펙트는 애니메이션에서 끄고 켜겠다.

    void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Ball_L"))
        {
            grabObj_L = coll.transform;
            isTouched_L = true;
        }
        //if (coll.CompareTag("Ball_R"))
        //{
        //    grabObj_R = coll.transform;
        //    isTouched_R = true;
        //}
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(dragonMouth.position, dragonMouth.position + dragonMouth.forward * rayDist);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(dragonMouth.position, catchRange);
    }
}
