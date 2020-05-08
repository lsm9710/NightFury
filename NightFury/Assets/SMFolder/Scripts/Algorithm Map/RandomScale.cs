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

    //플레이어의 높이와 부모 오브젝트의 높이차 비교
    // Start is called before the first frame update
    void Start()
    {
        blockFactory = Resources.Load<GameObject>("Algorithm Map/TestCube");
        float a = Random.Range(min, max);
        //오브젝트 풀로 생성하자
        for (int i = 0; i < amount/2; i++)
        {
            //뭘 생성할건데? => 블록 => 블록은 뭔데?
            block = Instantiate(blockFactory);
            blockPool.Add(block);
            block.transform.SetParent(pillar);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
