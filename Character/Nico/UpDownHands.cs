using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownHands : MonoBehaviour
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
        if(obj.tag == "Player" && master != obj)
        {
            obj.GetComponent<PlayerBase>().Damage(NicoData.UNKNOWN_DAMAGE);
            if (gameObject.name.Substring(0, 2) != "Up")
            {
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, NicoData.UNKNOWN_FORCE));
            }
            else
            {
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -NicoData.UNKNOWN_FORCE * 2));
            }
        }else if(obj.tag == "StoneWall")
        {
            obj.GetComponent<StoneWallScript>().Damage(NicoData.UNKNOWN_DAMAGE);
        }
    }
    public void SetMaster(GameObject a)
    {
        master = a;
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
