using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinzardMakibishi : MonoBehaviour
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.tag == "Player" && obj != master)
        {
            obj.GetComponent<PlayerBase>().Damage(NinzardData.MAKIBISHI_DAMAGE);
            obj.GetComponent<PlayerBase>().SetSpeedBonus(NinzardData.MAKIBISHI_SLOW_POWER, NinzardData.MAKIBISHI_SLOW_SECS);
            Destroy(gameObject);
        }
    }

    public void SetMaster(GameObject a)
    {
        master = a;
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(NinzardData.MAKIBISHI_LIVE_SECS);
        Destroy(gameObject);
    }
}
