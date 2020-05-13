using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullet_Explosion : MonoBehaviour
{
    public AudioClip aa;
    AudioSource ad;
    // Start is called before the first frame update
    void Start()
    {
        ad = GetComponent<AudioSource>();
        ad.PlayOneShot(aa);
        Destroy(gameObject, 3.45f);
    }

}
