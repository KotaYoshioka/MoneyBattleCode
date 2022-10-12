using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OdomotiKowarenena : MonoBehaviour
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
        if(obj.tag == "StoneWall")
        {
            obj.GetComponent<StoneWallScript>().Damage(MochitaData.KOWAREN_WALL_DAMAGE);
        }else if(obj.tag == "Player" && obj != master)
        {
            obj.GetComponent<PlayerBase>().MotitaCounter();
        }
    }

    public void SetMaster(GameObject a)
    {
        master = a;
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(MochitaData.KOWAREN_LIVE_SECS);
        Destroy(gameObject);
    }
}
