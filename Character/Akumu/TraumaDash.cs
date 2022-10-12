using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraumaDash : MonoBehaviour
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

    void FixedUpdate()
    {
        if (right == true)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(AkumuData.TRAUMA_SPEED, 0);
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-AkumuData.TRAUMA_SPEED, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if(obj.tag == "Player" && obj != master)
        {
            obj.GetComponent<PlayerBase>().Damage(AkumuData.TRAUMA_DAMAGE);
            obj.GetComponent<PlayerBase>().SetStan(AkumuData.TRAUMA_STAN_SECS);
        }else if(obj.tag == "StoneWall")
        {
            obj.GetComponent<StoneWallScript>().Damage(AkumuData.TRAUMA_DAMAGE);
        }
    }
    public void SetMaster(GameObject a)
    {
        master = a;
    }

    public void Right()
    {
        right = true;
    }
    IEnumerator Delete()
    {
        yield return new WaitForSeconds(AkumuData.TRAUMA_LIVE_SECS);
        Destroy(gameObject);
    }
}
