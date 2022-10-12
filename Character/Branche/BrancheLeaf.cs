using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrancheLeaf : MonoBehaviour
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
        if(obj.tag == "Player" && obj != master)
        {
            obj.GetComponent<PlayerBase>().Damage(BrancheData.LEAF_DAMAGE);
            Destroy(gameObject);
        }else if(obj.tag == "StoneWall")
        {
            obj.GetComponent<StoneWallScript>().Damage(BrancheData.LEAF_DAMAGE);
            Destroy(gameObject);
        }
    }

    public void SetMaster(GameObject a)
    {
        master = a;
    }
    IEnumerator Delete()
    {
        yield return new WaitForSeconds(BrancheData.LEAF_LIVE_SECS);
        Destroy(gameObject);
    }
}
