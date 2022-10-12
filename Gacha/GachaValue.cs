using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaValue : MonoBehaviour
{
    Text myText;
    // Start is called before the first frame update
    void Start()
    {
        myText = GetComponent<Text>();
        Refresh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Refresh()
    {
        switch (DataBase.nowSelectGacha)
        {
            case 1:
                myText.text = "80%\r\n13.24%\r\n5%\r\n1.5%\r\n0.2%\r\n0.05%\r\n0.01%\r\n";
                break;
            case 2:
                myText.text = "30%\r\n30.4%\r\n32%\r\n6%\r\n1%\r\n0.5%\r\n0.1%\r\n";
                break;
            case 3:
                myText.text = "10%\r\n10.5%\r\n25%\r\n45%\r\n8%\r\n1%\r\n0.5%\r\n";
                break;
            case 4:
                myText.text = "6.5%\r\n8.5%\r\n12%\r\n25%\r\n40%\r\n7%\r\n1%\r\n";
                break;
        }
    }
}
