using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyAPonkan : MonoBehaviour
{
    GameObject master;
    // Start is called before the first frame update
    void Start()
    {
        
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
            obj.GetComponent<PlayerBase>().Damage(CrazyAData.PONKAN_DAMAGE);
            obj.GetComponent<PlayerBase>().SetStan(CrazyAData.PONKAN_STAN_SECS);
            Destroy(gameObject);
        }else if(obj.tag == "StoneWall")
        {
            obj.GetComponent<StoneWallScript>().Damage(CrazyAData.PONKAN_DAMAGE);
            Destroy(gameObject);
        }else if(obj.tag == "Stage")
        {
            Destroy(gameObject);
        }
    }
    public void SetMaster(GameObject a)
    {
        master = a;
    }
}
