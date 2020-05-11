using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//뽝씨쓰 생성 해서 담고 붙이기
public class gustnWkd123 : MonoBehaviour
{
    List<GameObject> cubes = new List<GameObject>();
    GameObject make_Cube;
    public int make_count;
    public Material[] mat;
    void Start()
    {

        for (int i = 0; i < make_count; i++)
        {
            make_Cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            make_Cube.transform.position = transform.position;
            make_Cube.GetComponent<MeshRenderer>().material = mat[Random.Range(0, mat.Length)];
            //make_Cube.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
            make_Cube.transform.localScale = new Vector3(Random.Range(1, 5), Random.Range(1, 5), Random.Range(1, 5));
            cubes.Add(make_Cube);
            make_Cube = null;
        }
        //위로 붙여욧
        for (int i = 0; i < cubes.Count / 2; i++)
        {
            if (i + 1 >= cubes.Count)
                break;
            float a = cubes[i].transform.localScale.y / 2;
            float b = cubes[i + 1].transform.localScale.y / 2;
            Vector3 pos = Vector3.up * (a + b);
            cubes[i + 1].transform.position = cubes[i].transform.position;
            cubes[i + 1].transform.position += pos;
        }
        cubes[cubes.Count / 2].transform.position = cubes[0].transform.position;
        float origin = cubes[0].transform.localScale.y / 2;
        float newa = cubes[cubes.Count / 2].transform.localScale.y / 2;
        Vector3 gustnWkd = Vector3.down * (origin + newa);
        cubes[cubes.Count / 2].transform.position += gustnWkd;
        //아래로 붙여욧
        for (int i = cubes.Count / 2; i < cubes.Count; i++)
        {
            if (i + 1 >= cubes.Count)
                break;
            float a = cubes[i].transform.localScale.y / 2;
            float b = cubes[i + 1].transform.localScale.y / 2;
            Vector3 pos = Vector3.down * (a + b);
            cubes[i + 1].transform.position = cubes[i].transform.position;
            cubes[i + 1].transform.position += pos;
        }
    }

}