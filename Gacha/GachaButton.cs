using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaButton : MonoBehaviour
{
    public GameObject a;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        DataBase.nowSelectGacha = int.Parse(gameObject.name);
        a.SetActive(true);
        try
        {
            a.transform.Find("Value").gameObject.GetComponent<GachaValue>().Refresh();
        }
        catch
        {

        }
    }
}
