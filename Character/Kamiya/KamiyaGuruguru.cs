using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamiyaGuruguru : MonoBehaviour
{
    GameObject master;
    CircleCollider2D cc;
    // Start is called before the first frame update
    void Start()
    {
        cc = gameObject.GetComponent<CircleCollider2D>();
        StartCoroutine("DeleteAtari");
        StartCoroutine("Delete");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if(obj.tag == "Player" && obj != master)
        {
            obj.GetComponent<PlayerBase>().Damage(KamiyaData.BAKU_DAMAGE);
            obj.GetComponent<PlayerBase>().SetSilenct(KamiyaData.BAKU_SILENCE_SECS);
        }else if(obj.tag == "StoneWall")
        {
            obj.GetComponent<StoneWallScript>().Damage(KamiyaData.BAKU_DAMAGE);
        }
    }
    public void SetMaster(GameObject a)
    {
        master = a;
    }
    IEnumerator DeleteAtari()
    {
        yield return new WaitForSeconds(0.2f);
        cc.enabled = false;
    }
    IEnumerator Delete()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
