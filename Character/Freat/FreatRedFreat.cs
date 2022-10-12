using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreatRedFreat : MonoBehaviour
{
    GameObject master;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Delete");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if(obj.tag == "Player" && master != obj)
        {
            obj.GetComponent<PlayerBase>().SetSpeedBonus(FreatData.ODO_SLOW_LEVEL,1000);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if(obj.tag == "Player" && master != obj)
        {
            obj.GetComponent<PlayerBase>().EndSpeedBonus(FreatData.ODO_SLOW_LEVEL);
        }
    }
    public void SetMaster(GameObject a)
    {
        master = a;
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(FreatData.ODO_LIVE_SECS);
        Destroy(gameObject);
    }
}
