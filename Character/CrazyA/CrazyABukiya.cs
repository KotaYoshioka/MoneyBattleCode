using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyABukiya : MonoBehaviour
{
    public GameObject a, b, c;
    GameObject master;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Emit");
        StartCoroutine("Delete");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Emit()
    {
        yield return new WaitForSeconds(0.5f);
        a.GetComponent<Rigidbody2D>().AddForce(new Vector2(200, 600));
        a.GetComponent<Rigidbody2D>().gravityScale = 1;
        a.GetComponent<CrazyABuki>().SetMaster(master);
        b.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1300));
        b.GetComponent<Rigidbody2D>().gravityScale = 1;
        b.GetComponent<CrazyABuki>().SetMaster(master);
        c.GetComponent<Rigidbody2D>().AddForce(new Vector2(-200, 600));
        c.GetComponent<Rigidbody2D>().gravityScale = 1;
        c.GetComponent<CrazyABuki>().SetMaster(master);
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }
    public void SetMaster(GameObject a)
    {
        master = a;
    }
}
