using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OdomotiDamarete : MonoBehaviour
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
            obj.GetComponent<PlayerBase>().SetSilenct(MochitaData.SHUTUP_SILENCE_SECS);
        }
    }
    public void SetMaster(GameObject a)
    {
        master = a;
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(MochitaData.SHUTUP_LIVE_SECS);
        Destroy(gameObject);
    }
}
