using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StanEffect : MonoBehaviour
{
    GameObject master;
    int ID;
    bool right;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Delete");
    }

    // Update is called once per frame
    void Update()
    {
        float newX = 0;
        float newY = 0;
        if (right == false)
        {
            newX = DataBase.GetStanSize(ID,0);
            newY = DataBase.GetStanSize(ID, 1);
        }
        else
        {
            newX = DataBase.GetStanSize(ID, 2);
            newY = DataBase.GetStanSize(ID, 3);
        }
        gameObject.transform.position = new Vector2(master.transform.position.x + newX, master.transform.position.y + newY);
    }

    public void SetMaster(GameObject a,int b,bool rightn)
    {
        master = a;
        ID = b;
        right = rightn;
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(0.35f);
        Destroy(gameObject);
    }
}
