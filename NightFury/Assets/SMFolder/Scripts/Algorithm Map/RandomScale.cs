using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomScale : MonoBehaviour
{
    //스케일을 랜덤으로 지정해서 생성하기
    //최솟값
    //최댓값
    public float min = 1f;
    public float max = 7f;
    //몇개?
    //짝수로 10개 이상
    public int amount = 10;
    //블록 공장 주소
    GameObject blockFactory;
    //블록
    GameObject block;

    //부모 기둥
    public Transform pillar;

    //포지션을 붙이면서 리스트에 추가
    //위아래 따로 리스트관리
    public List<GameObject> blockPool = new List<GameObject>();
    public List<GameObject> topBlockPool = new List<GameObject>();
    public List<GameObject> bottomBlockPool = new List<GameObject>();

    //플레이어의 높이와 부모 오브젝트의 높이차 비교
    // Start is called before the first frame update
    void Start()
    {
        blockFactory = Resources.Load<GameObject>("Algorithm Map/TestCube");
        
        RandomScaleInstant();
        SetPositionInPillar();
    }

    //위로 쌓아올릴 큐브들을 집어넣을 배열
    //GameObject[] StackingUp;
    private void SetPositionInPillar()
    {
        float a;
        float b;
        blockPool[0].transform.position = blockPool[0].transform.parent.position;
        for (int i = 0; i < blockPool.Count / 2; i++)
        {
            //만약에 절반보다 커졌으면 그만
            if (blockPool.Count <= i + 1) break;
            //인덱스 2번의 로컬스케일 Y를 둘로 나눈값을 저장한다
            a = blockPool[i].transform.localScale.y / 2;
            //인덱스 3번의 로컬스케일 Y를 둘로 나눈값을 저장한다
            b = blockPool[i + 1].transform.localScale.y / 2;
            //새로 지정해줄 y포지션값을 저장할 변수
            Vector3 topPosition = Vector3.up * (a + b);


            //3번의 포지션을 2번과 똑같게 하고
            blockPool[i + 1].transform.position = blockPool[i].transform.position;
            //3번의 Y만 2개의 값을 둘로 나눠 합친값 만큼 더해준다
            blockPool[i + 1].transform.position += topPosition;
            blockPool[i].transform.name = i.ToString();
            //위로 늘어날 리스트에 집어넣는다.
            topBlockPool.Add(blockPool[i]);
            //  blockPool.Remove(blockPool[i]);
        }

        blockPool[blockPool.Count /2 ].transform.position = blockPool[0].transform.parent.position;
        float e = blockPool[blockPool.Count / 2].transform.localScale.y /2;
        float f = blockPool[0].transform.localScale.y /2;
        Vector3 startDownPosition = Vector3.down * (e + f);
        blockPool[blockPool.Count / 2].transform.position += startDownPosition;

        for (int i = blockPool.Count /2; i < blockPool.Count -1; i++)
        {
            //만약에 절반보다 커졌으면 그만
            if (blockPool.Count <= i + 1) break;
            //인덱스 2번의 로컬스케일 Y를 둘로 나눈값을 저장한다
            float c = blockPool[i].transform.localScale.y / 2;
            //인덱스 3번의 로컬스케일 Y를 둘로 나눈값을 저장한다
            float d = blockPool[i + 1].transform.localScale.y / 2;
            //새로 지정해줄 y포지션값을 저장할 변수
            Vector3 bottomPosition = Vector3.down * (c + d);

            //blockPool[blockPool.Count / 2].transform.position += bottomPosition;
            //3번의 포지션을 2번과 똑같게 하고
            blockPool[i + 1].transform.position = blockPool[i].transform.position;
            //3번의 Y만 2개의 값을 둘로 나눠 합친값 만큼 더해준다
            blockPool[i + 1].transform.position += bottomPosition;
            blockPool[i].transform.name = i.ToString();
            //위로 늘어날 리스트에 집어넣는다.
            topBlockPool.Add(blockPool[i]);
            //  blockPool.Remove(blockPool[i]);
        }
        ////이건 포문에서 돌 내용이 아니니까 밖으로 뺀다 한번이면 충분
        ////인덱스 절반번째 녀석의 위치를 0번째 녀석의 위치에 가져다 놓고
        //blockPool[blockPool.Count / 2].transform.position = blockPool[0].transform.position;
        ////0번째 녀석의 Y스케일 높이값을 저장해놓고
        //float origin = blockPool[0].transform.localScale.y / 2;
        ////인덱스 절반번째 녀석의 높이값도 저장을 해놓고
        //float idxHalf = blockPool[blockPool.Count / 2].transform.localScale.y / 2;
        ////두개를 더해 방향은 아래로 이제 아래로 할거니까
        //Vector3 cross = Vector3.down * (origin + idxHalf);
        ////인덱스 절반번째의 위치를 정해준다
        //blockPool[blockPool.Count / 2].transform.position += cross;

        ////아래로 이쿠욧~!!
        //for (int u = 0; u < blockPool.Count / 2; u++)
        //{
        //    //너도 절반보다 커졌으면 그만
        //    if (u+1 >= blockPool.Count / 2) break;
        //    //지금 인덱스의 y값 절반을 저장해놓고
        //    float a = blockPool[u].transform.localScale.y / 2;
        //    //그 다음 인덱스의 y값 절반도 저장해놓고
        //    float b = blockPool[u + 1].transform.localScale.y / 2;
        //    //그 둘을 더해서 저장해놓고
        //    Vector3 downPosition = Vector3.down * (a + b);
        //    //다음 인덱스녀석을 지금 인덱스 녀석의 위치에 가져다놓고
        //    blockPool[u + 1].transform.position = blockPool[u].transform.position;
        //    //y만 움직여서 맞춰준다
        //    blockPool[u + 1].transform.position += downPosition;
        //    blockPool[u].transform.name = u.ToString();
        //    //아래로 늘어날 리스트에 넣어주자
        //    bottomBlockPool.Add(blockPool[u]);
        //    //blockPool.Remove(blockPool[u]);
        //}
    }

    void RandomScaleInstant()
    {
        //오브젝트 풀로 생성하자
        for (int q = 0; q < amount; q++)
        {
            //랜덤한 가로,세로,높이의 크기를 구하고
            float x = UnityEngine.Random.Range(min, max);
            float y = UnityEngine.Random.Range(min, max);
            float z = UnityEngine.Random.Range(min, max);
            //뭘 생성할건데? => 블록 => 블록은 뭔데?
            //블록공장에서 생산
            block = Instantiate(blockFactory);
            //생산한녀석의 크기를 랜덤하게 지정해주고
            block.transform.localScale = new Vector3(x, y, z);
            //리스트에 집어넣고
            blockPool.Add(block);
            //부모를 기둥으로 지정
            block.transform.SetParent(pillar);
        }
        
    }
}
