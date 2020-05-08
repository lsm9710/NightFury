using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// 생성 할 부\ㅡㄹ록 갯수 정해 == 작수여야해
/// 블록 가로세로 리미트 정하고 그걸로 랜덤 때릴거임
/// 기둥쓰 한개당 몇개 박을거냐?!
/// 뭐부터 해볼까
/// 랜덤으로 키워서 그거 위에 붙이는거부터 하나 해보께오





//그냥 하이어라키에 있는애들 이름이든 뭐든 해서 갖다가 배열이나 리스트에 넣어

// 순서대로 순번째랑 그다음번째랑 스케일 나누기2값  해서 방향 정한데다가 붙이ㅏ면 되자나

public class gustnWKd123 : MonoBehaviour
{

    GameObject[] Toatkfkdgody;

    private void Start()
    {
        Toatkfkdgody = GameObject.FindGameObjectsWithTag("Cube");

        for (int i = 0; i < Toatkfkdgody.Length -1; i++)
        {
            //인덱스 2번의 로컬스케일 Y를 둘로 나눈값을 저장한다
            float a = Toatkfkdgody[i].transform.localScale.y / 2;
            //인덱스 3번의 로컬스케일 Y를 둘로 나눈값을 저장한다
            float b = Toatkfkdgody[i + 1].transform.localScale.y / 2;

            
            //새로 지정해줄 y포지션값을 저장할 변수
            Vector3 wlqqhsownj = Vector3.up * (a + b);

            Toatkfkdgody[0].transform.position = Toatkfkdgody[0].transform.parent.position;
            //3번의 포지션을 2번과 똑같게 하고
            Toatkfkdgody[i + 1].transform.position = Toatkfkdgody[i].transform.position;
            //3번의 Y만 2개의 값을 둘로 나눠 합친값 만큼 더해준다
            Toatkfkdgody[i + 1].transform.position += wlqqhsownj;


            //만약에 Length보다 커졌으면 그만
            if (Toatkfkdgody.Length < i + 1) break;
        }
    }
    void Update()
    {
        
    }


    //다음에 할일
    //스케일을 랜덤으로 지정해서 생성하기
    //몇개?
    //짝수로 10개 이상
    //포지션을 붙이면서 리스트에 추가
    //위아래 따로 리스트관리
    
    //플레이어의 높이와 부모 오브젝트의 높이차 비교
}
