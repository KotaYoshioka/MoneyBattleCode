using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AkumuKage : MonoBehaviour
{
    private bool right = false;
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

    private void FixedUpdate()
    {
        if (right == true)
        {
            //gameObject.transform.position = new Vector2(gameObject.transform.position.x + CharaAbiData.Akumu(3),gameObject.transform.position.y);
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(AkumuData.SHADOW_WALK_SPEED, 0);
        }
        else
        {
            //gameObject.transform.position = new Vector2(gameObject.transform.position.x - CharaAbiData.Akumu(3), gameObject.transform.position.y);
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-AkumuData.SHADOW_WALK_SPEED, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.tag == "Player" && obj != master)
        {
            obj.GetComponent<PlayerBase>().Damage(AkumuData.SHADOW_DAMAGE);
            master.transform.position = gameObject.transform.position;
            Destroy(gameObject);
        }
        else if (obj.tag == "StoneWall")
        {
            obj.GetComponent<StoneWallScript>().Damage(AkumuData.SHADOW_DAMAGE);
            Destroy(gameObject);
        }
    }
    public void Right()
    {
        right = true;
    }

    public void SetMaster(GameObject a)
    {
        master = a;
    }
    IEnumerator Delete()
    {
        yield return new WaitForSeconds(AkumuData.SHADOW_LIVE_SECS);
        Destroy(gameObject);
    }
}
