using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FaceImage : MonoBehaviour
{
    Image faceImage;

    TextMeshProUGUI deathCD;

    void Start()
    {
        faceImage = GetComponent<Image>();
        deathCD = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetImage(int charaID)
    {
        faceImage.sprite = Resources.Load<Sprite>("Images/CharaIcon/" + CharaData.GetCharaNameEnglish(charaID));
    }

    public void SetDeathCD(int cd)
    {
        if(cd == 0)
        {
            deathCD.text = "";
            faceImage.color = new Color(1, 1, 1);
        }
        else
        {
            deathCD.text = cd.ToString();
            faceImage.color = new Color(0.3f, 0.3f, 0.3f);
        }
    }
}
