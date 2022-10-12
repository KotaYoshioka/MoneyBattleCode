using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NicoHands : MonoBehaviour
{
    GameObject master;
    bool right = false;
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
            obj.GetComponent<PlayerBase>().Damage(NicoData.SEARCH_DAMAGE);
            if(right == true)
            {
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(NicoData.SEARCH_FORCE,0));
            }
            else
            {
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(-NicoData.SEARCH_FORCE, 0));
            }
            Destroy(gameObject);
        }else if(obj.tag == "StoneWall")
        {
            obj.GetComponent<StoneWallScript>().Damage(NicoData.SEARCH_DAMAGE);
        }else if(obj.tag == "Damage")
        {
            Destroy(obj);
            Destroy(gameObject);
        }
    }
    public void SetMaster(GameObject a)
    {
        master = a;
    }

    public void SetRight()
    {
        right = true;
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
