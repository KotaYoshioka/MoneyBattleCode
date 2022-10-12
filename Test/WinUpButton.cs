using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinUpButton : MonoBehaviour
{
    public GameObject texter;
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
        DataBase.WinUp();
        texter.GetComponent<SuperSimpleTexter>().Refresh();
    }
}
