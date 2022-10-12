using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamiyaBatouBakuhu : MonoBehaviour
{
    CircleCollider2D cc;
    ParticleSystem ps;
    GameObject master;
    public GameObject leader;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CircleCollider2D>();
        StartCoroutine("DeleteAtari");
        StartCoroutine("Delete");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if(obj.tag == "Player")
        {
            obj.GetComponent<PlayerBase>().Damage(KamiyaData.BATO_DAMAGE);
        }else if(obj.tag == "StoneWall")
        {
            obj.GetComponent<StoneWallScript>().Damage(KamiyaData.BATO_DAMAGE);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMaster(GameObject a)
    {
        master = a;
        ps = GetComponent<ParticleSystem>();
        ps.Play();
    }
    IEnumerator DeleteAtari()
    {
        yield return new WaitForSeconds(0.2f);
        cc.enabled = false;
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(1.4f);
        Destroy(leader);
    }
}
