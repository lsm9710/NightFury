using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //NavMeshAgent 사용
public class SBS_D_Enemy : MonoBehaviour
{
    //목표 : Enemy Dragon 만들고싶다
    //필요속성 : 대기시간,현재시간,스피드,에니메이터,공격범위,NavMesh에이전트,캐릭터컨트롤,현재hp,사운드

    public List<Transform> enemy_WayPoints;
    public float waitTime = 1.7f;
    float currentTime = 0;
    float rotSpeed = 4;
    public enum eState
    {
        PATROL,
        WAIT,
        MOVE,
        ATTACK,
        DAMAGE,
        DIE
    }
    public eState state;
    public int e_Hp = 4;
    public float dmgDelayTime = 1.3f;
    public float moveSpeed = 25.0f;
    public float atkRange = 1000.0f;
    public float atkDelayTime = 1.3f;
    float atkClipLen = 0.0f;
    public Transform target;
    public GameObject enemyBreath;
    public Transform breathTr;
    CharacterController charCon;
    Animator animator;
    NavMeshAgent agent;
    AudioSource soundP;
    public AudioClip die_S;
    //public AudioClip atk_S;
    void Start()
    {
        target = GameObject.Find("NightFury").transform;
        state = eState.WAIT;
        charCon = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        soundP = GetComponent<AudioSource>();
        agent.enabled = false;
    }

    void Update()
    {
        switch (state)
        {
            case eState.WAIT:
                Wait();
                break;
            case eState.MOVE:
                Move();
                break;
            case eState.ATTACK:
                Attack();
                break;
            case eState.DAMAGE:
                break;
            case eState.DIE:
                break;
        }
    }

    //대기
    void Wait()
    {
        currentTime += Time.deltaTime;
        if (currentTime > waitTime)
        {
            state = eState.MOVE;
            animator.SetTrigger("Move");
            currentTime = 0;
        }

    }

    //이동 처리
    void Move()
    {
        // charCon.SimpleMove(dir.normalized * moveSpeed);
        Vector3 dir = target.transform.position - transform.position;

        if (agent.enabled == false) agent.enabled = true;

        agent.destination = target.transform.position;
        dir.y = 0;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir.normalized), rotSpeed * Time.deltaTime);

        if (dir.sqrMagnitude < atkRange)
        {
            agent.enabled = false;
            state = eState.ATTACK;
            currentTime = atkDelayTime + atkClipLen;
        }
    }

    // 공격상태
    void Attack()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        int attackState = Animator.StringToHash("Base Layer.Attack.Attack");

        if (stateInfo.fullPathHash == attackState) atkClipLen = stateInfo.length;

        currentTime += Time.deltaTime;

        if (currentTime > atkDelayTime + atkClipLen)
        {
            GameObject magic = Instantiate(enemyBreath);
            magic.transform.position = breathTr.position;
           // magic.transform.LookAt(target.transform.position);

            animator.SetTrigger("Attack");
            currentTime = 0.0f;
            // SBS_PlayerHealth.instance.currHp--;
            // SBS_PlayerHealth.instance.DisplayHpbar();


        }

        Vector3 dir = target.transform.position - transform.position;

        if (dir.sqrMagnitude > atkRange)
        {
            animator.SetTrigger("Move");
            state = eState.MOVE;
        }
    }
    //공격범위Show
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, atkRange);
        Gizmos.color = new Color(1.1f, 0.1f, 1.1f, 0.4f);
    }

    // Dmg,Die 코루틴
    public IEnumerator DamageProcess()
    {
        e_Hp--;

        if (e_Hp > 0)
        {
            //Damage
            state = eState.DAMAGE;
            animator.SetTrigger("Damage");
            yield return new WaitForSeconds(dmgDelayTime);
            state = eState.WAIT;
        }
        else
        {
            //Die
            Die();
        }
        yield return null;
    }
    //죽음처리
    private void Die()
    {
        soundP.PlayOneShot(die_S); //죽음 사운드
        state = eState.DIE;
        animator.SetTrigger("Die");
        Destroy(gameObject, 1.5f);
    }
}
