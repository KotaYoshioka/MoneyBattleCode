using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrancheEda : MonoBehaviour
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
        if (obj.tag == "Player" && obj != master)
        {
            obj.GetComponent<PlayerBase>().Damage(BrancheData.STICK_DAMAGE);
            Destroy(gameObject);
        }
        else if (obj.tag == "StoneWall")
        {
            obj.GetComponent<StoneWallScript>().Damage(BrancheData.STICK_DAMAGE);
            Destroy(gameObject);
        }
        else if(obj.tag == "Stage")
        {
            Destroy(gameObject);
        }
    }

    public void SetMaster(GameObject b)
    {
        master = b;
    }
}
