using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamiyaBatou : MonoBehaviour
{
    GameObject master;
    public GameObject bakuhu;
    public AudioClip rakka, bakuha;
    AudioSource myAudio;
    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
        myAudio.PlayOneShot(rakka);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Bomb")
        {
            bakuhu.SetActive(true);
            myAudio.PlayOneShot(bakuha);
            gameObject.GetComponent<SpriteRenderer>().color = Color.clear;
            bakuhu.GetComponent<KamiyaBatouBakuhu>().SetMaster(master);
        }
    }
    public void SetMaster(GameObject a)
    {
        master = a;
    }
}
