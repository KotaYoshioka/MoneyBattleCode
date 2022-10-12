using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyAKoma : MonoBehaviour
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.tag == "Player" && master != obj)
        {
            obj.GetComponent<PlayerBase>().EndSpeedBonus(CrazyAData.INCOMIC_SLOW_LEVEL);
            obj.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
        else if (obj.tag == "Player" && master == obj)
        {
            obj.GetComponent<PlayerBase>().EndSpeedBonus(CrazyAData.INCOMIC_ADDSPEED_LEVEL);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if(obj.tag == "Player" && master != obj)
        {
            obj.GetComponent<PlayerBase>().SetSpeedBonus(CrazyAData.INCOMIC_SLOW_LEVEL, 1000);
            obj.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
        }
        else if(obj.tag == "Player" && master == obj)
        {
            obj.GetComponent<PlayerBase>().SetSpeedBonus(CrazyAData.INCOMIC_ADDSPEED_LEVEL, 1000);
        }
    }

    public void SetMaster(GameObject a)
    {
        master = a;
    }
    IEnumerator Delete()
    {
        yield return new WaitForSeconds(CrazyAData.INCOMIC_LIVE_SECS);
        Destroy(gameObject);
    }
}
