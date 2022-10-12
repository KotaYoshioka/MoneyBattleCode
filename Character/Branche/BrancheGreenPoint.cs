using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrancheGreenPoint : MonoBehaviour
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
        if(obj.tag == "Stage" || obj.tag == "StoneWall")
        {
            master.GetComponent<PlayerBase>().Branche3();
            Destroy(gameObject);
        }
    }

    public void SetMaster(GameObject a)
    {
        master = a;
    }
}
