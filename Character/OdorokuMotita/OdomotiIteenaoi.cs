using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OdomotiIteenaoi : MonoBehaviour
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
        if(obj.tag == "Player" && obj != master)
        {
            obj.GetComponent<PlayerBase>().Damage(MochitaData.ITEE_DAMAGE);
        }else if(obj.tag == "StoneWall")
        {
            obj.GetComponent<StoneWallScript>().Damage(MochitaData.ITEE_DAMAGE);
        }
    }
    public void SetMaster(GameObject a)
    {
        master = a;
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(MochitaData.ITEE_LIVE_SECS);
        Destroy(gameObject);
    }
}
