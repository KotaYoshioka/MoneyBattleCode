using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamiyaRange : MonoBehaviour
{
    GameObject master;
    bool damage = true;
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine("Delete");
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = master.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if(obj.tag == "Player" && obj != master)
        {
            obj.GetComponent<PlayerBase>().SetSpeedBonus(KamiyaData.GRAVITY_SLOW_LEVEL,9999);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if(obj.tag == "Player" && obj != master)
        {
            if(damage == true)
            {
                obj.GetComponent<PlayerBase>().Damage(KamiyaData.GRAVITY_DAMAGE);
                damage = false;
                StartCoroutine("Restore");
            }
        }else if(obj.tag == "StoneWall")
        {
            if (damage == true)
            {
                obj.GetComponent<StoneWallScript>().Damage(KamiyaData.GRAVITY_DAMAGE);
                damage = false;
                StartCoroutine("Restore");
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.tag == "Player" && obj != master)
        {
            obj.GetComponent<PlayerBase>().EndSpeedBonus(KamiyaData.GRAVITY_SLOW_LEVEL);
        }
    }

    public void SetMaster(GameObject a)
    {
        master = a;
    }

    IEnumerator Restore()
    {
        yield return new WaitForSeconds(KamiyaData.GRAVITY_INTERVAL);
        damage = true;
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(KamiyaData.GRAVITY_LIVE_SECS);
        Destroy(gameObject);
    }
}
