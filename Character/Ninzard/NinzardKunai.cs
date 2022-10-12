using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinzardKunai : MonoBehaviour
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
        if(obj.tag == "Player" && obj != master)
        {
            obj.GetComponent<PlayerBase>().Damage(NinzardData.KUNAI_DAMAGE);
        }else if(obj.tag == "StoneWall")
        {
            obj.GetComponent<StoneWallScript>().Damage(NinzardData.KUNAI_DAMAGE);
        }else if(obj.tag == "Stage" || obj.tag == "UpStage")
        {
            Destroy(gameObject);
        }
    }

    public void SetMaster(GameObject a)
    {
        master = a;
    }
}
