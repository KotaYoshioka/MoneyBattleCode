using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OdomotiSorehanaidaro : MonoBehaviour
{
    GameObject master;
    bool result = false;
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
            obj.GetComponent<PlayerBase>().Damage(MochitaData.NAIDARO_DAMAGE);
            result = true;
        }else if(obj.tag == "Damage")
        {
            Destroy(obj);
        }else if(obj.tag == "StoneWall")
        {
            obj.GetComponent<StoneWallScript>().Damage((int)MochitaData.NAIDARO_DAMAGE);
            result = true;
        }
    }

    public void SetMaster(GameObject newmaster)
    {
        master = newmaster;
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(MochitaData.NAIDARO_LIVE_SECS);
        master.GetComponent<PlayerBase>().Odomoti2Get(result);
        Destroy(gameObject);
    }
}
