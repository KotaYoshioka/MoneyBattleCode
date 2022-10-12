using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrancheTree : MonoBehaviour
{
    private GameObject master;
    private bool healnow = true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Delete");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if(obj == master && healnow == true)
        {
            healnow = false;
            obj.GetComponent<PlayerBase>().Heal(BrancheData.PLANT_HEAL_POWER);
            StartCoroutine("CD");
        }
    }
    

    public void SetMaster(GameObject a)
    {
        master = a;
    }

    IEnumerator CD()
    {
        yield return new WaitForSeconds(BrancheData.PLANT_HEAL_POWER);
        healnow = true;
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(BrancheData.PLANT_LIVE_SECS);
        Destroy(gameObject);
    }
}
