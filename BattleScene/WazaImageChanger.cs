using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WazaImageChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().sprite = Resources.Load<Sprite>("Waza/" + CharaData.GetCharaNameEnglish(UserData.GetSelectChara()) + "/" + gameObject.name.Substring(4,1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
