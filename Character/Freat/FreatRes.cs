using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreatRes : MonoBehaviour
{
    ParticleSystem ps;
    CircleCollider2D cc;
    GameObject master;
    private AudioSource myAudio;
    public AudioClip ac;
    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>(); 
        ps = gameObject.transform.Find("Bomber").GetComponent<ParticleSystem>();
        cc = GetComponent<CircleCollider2D>();
        ps.Pause();
        cc.enabled = false;
        StartCoroutine("Bomb");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Bomb()
    {
        yield return new WaitForSeconds(FreatData.BAKU_EXPLOSION_DELAY_SECS);
        GetComponent<SpriteRenderer>().color = Color.clear;
        ps.Play();
        myAudio.PlayOneShot(ac);
        cc.enabled = true;
        StartCoroutine("Delete");
        StartCoroutine("DeleteAtari");
    }

    IEnumerator DeleteAtari()
    {
        yield return new WaitForSeconds(0.6f);
        cc.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if(obj.tag == "Player" && obj != master)
        {
            obj.GetComponent<PlayerBase>().Damage(FreatData.BAKU_EXPLOSION_DAMAGE);
        }else if(obj == master)
        {
            obj.GetComponent<PlayerBase>().Damage(FreatData.BAKU_EXPLOSION_DAMAGE);
        }else if(obj.tag == "StoneWall")
        {
            obj.GetComponent<StoneWallScript>().Damage(FreatData.BAKU_EXPLOSION_SELF_DAMAGE);
        }
    }

    public void SetMaster(GameObject a)
    {
        master = a;
    }
    IEnumerator Delete()
    {
        yield return new WaitForSeconds(1.3f);
        Destroy(gameObject);
    }
}
