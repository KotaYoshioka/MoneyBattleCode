using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityImage : MonoBehaviour
{
    private Image image;

    private TextMeshProUGUI cd;

    void Start()
    {
        cd = GetComponentInChildren<TextMeshProUGUI>();
    }
    

    void Update()
    {
        
    }

    /// <summary>
    /// 指定されたキャラの技を表示する。
    /// </summary>
    public void SetImage(int charaID,int abilityID)
    {
        if(image == null) image = GetComponent<Image>();
        image.sprite = Resources.Load<Sprite>("Images/Waza/" + CharaData.GetCharaNameEnglish(charaID) + "/" + abilityID);
    }

    public void SetCD(int cooldown)
    {
        if(cooldown == 0)
        {
            cd.text = "";
            image.color = new Color(1, 1, 1);
        }
        else
        {
            cd.text = cooldown.ToString();
            image.color = new Color(0.3f, 0.3f, 0.3f);
        }
    }
}
