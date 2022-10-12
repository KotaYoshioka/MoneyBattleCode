using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarumanStanField : MonoBehaviour
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
            obj.GetComponent<PlayerBase>().SetStan(DarumanData.FELLOVER_STAN_SECS);
        }
    }

    public void SetMaster(GameObject a)
    {
        master = a;
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(DarumanData.FELLOVER_LIVE_SECS);
        Destroy(gameObject);
    }
}
