using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NicoHandser : MonoBehaviour
{
    bool move = true;
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
        if (move == true)
        {
            switch (int.Parse(gameObject.name.Substring(0, 1)))
            {
                case 1:
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x + NicoData.END_SPEED, gameObject.transform.position.y);
                    break;
                case 2:
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - NicoData.END_SPEED);
                    break;
                case 3:
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x - NicoData.END_SPEED, gameObject.transform.position.y);
                    break;
                case 4:
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + NicoData.END_SPEED);
                    break;

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if(obj.tag == "Player" && obj != master)
        {
            obj.transform.position = new Vector2(0, 0);
            obj.GetComponent<PlayerBase>().SetStan(4);
            obj.GetComponent<PlayerBase>().NicoBlack();
            master.GetComponent<PlayerBase>().Nico4_2();
        }
    }

    public void SetMaster(GameObject a)
    {
        master = a;
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(7f);
        Destroy(gameObject);
    }
}
