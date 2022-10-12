using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().sprite = Resources.Load<Sprite>("CharaIcon/" + CharaData.GetCharaNameEnglish(UserData.GetSelectChara()));  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
