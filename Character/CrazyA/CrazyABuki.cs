using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyABuki : MonoBehaviour
{
    GameObject master;
    bool atari = false;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Atari");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (atari == true)
        {
            GameObject obj = collision.gameObject;
            if (obj.tag == "Player" && obj != master)
            {
                obj.GetComponent<PlayerBase>().Damage(CrazyAData.WEAPON_DAMAGE);
            }
            else if (obj.tag == "StoneWall")
            {
                obj.GetComponent<StoneWallScript>().Damage(CrazyAData.WEAPON_DAMAGE);
            }
        }
    }

    public void SetMaster(GameObject a)
    {
        master = a;
    }

    IEnumerator Atari()
    {
        yield return new WaitForSeconds(1f);
        gameObject.layer = 8;
        atari = true;
    }
}
