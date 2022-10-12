using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultOKButton : MonoBehaviour
{
    public GameObject tab;
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
        tab.SetActive(false);
        DataBase.gameEnd = false;
    }
}
