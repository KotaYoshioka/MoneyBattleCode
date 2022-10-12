using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AkumuPower : MonoBehaviour
{
    private bool right = false;
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
        if(obj.tag == "Player")
        {
            obj.GetComponent<PlayerBase>().SetStan(AkumuData.MICHIBIKI_STAN_SECS);
            if(right == false)
            {
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(AkumuData.MICHIBIKI_PULL_FORCE,0));
            }
            else
            {
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(-AkumuData.MICHIBIKI_PULL_FORCE, 0));
            }
        }
    }

    public void SetRight()
    {
        right = true;
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(AkumuData.MICHIBIKI_LIVE_SECS);
        Destroy(gameObject);
    }
}
