using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyIei : MonoBehaviour
{
    public CircleCollider2D cc;
    public ParticleSystem ps;
    GameObject master;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Bomber");
        StartCoroutine("Delete");
        ps.Stop();
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject a = collision.gameObject;
        if(a.tag == "Player")
        {
            a.GetComponent<PlayerBase>().Damage(CrazyAData.DYNAMIC_DAMAGE);
        }else if(a.tag == "StoneWall"){
            a.GetComponent<StoneWallScript>().Damage(CrazyAData.DYNAMIC_DAMAGE);
        }
    }

    IEnumerator Bomber()
    {
        yield return new WaitForSeconds(CrazyAData.DYNAMIC_EXPLOSION_DELAY_SECS);
        ps.Play();
        gameObject.GetComponent<SpriteRenderer>().color = Color.clear;
        cc.enabled = true;
        StartCoroutine("DeleteAtari");
    }

    IEnumerator DeleteAtari()
    {
        yield return new WaitForSeconds(0.5f);
        cc.enabled = false;
    }
    IEnumerator Delete()
    {
        yield return new WaitForSeconds(CrazyAData.DYNAMIC_EXPLOSION_DELAY_SECS + 1.2f);
        Destroy(gameObject);
    }
    public void SetMaster(GameObject a)
    {
        master = a;
    }
}
