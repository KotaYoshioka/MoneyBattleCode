using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// キャラの詳細画面に関する処理
/// </summary>
public class DetailPanel : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI nameField, hpField, speedField, jumpField, jumpTimeField, respawnField, designField;
    [SerializeField]Image charaImage;
    [SerializeField] Image[] wazas;
    
    void Start()
    {
        Refresh(UserData.GetSelectChara());
    }

    /// <summary>
    /// 指定されたIDを基に、キャラの詳細を表示する
    /// </summary>
    public void Refresh(int charaID)
    {
        string ename = CharaData.GetCharaNameEnglish(charaID);
        nameField.text = "名前：" + CharaData.GetCharaName(charaID);
        hpField.text = "体力：" + CharaData.GetHP(charaID).ToString();
        speedField.text = "移動速度：" + CharaData.GetSpeed(charaID).ToString();
        jumpField.text = "ジャンプ：" + CharaData.GetJump(charaID).ToString();
        jumpTimeField.text = "ジャンプ回数：" + CharaData.GetJumpTime(charaID).ToString();
        respawnField.text = "復活時間：" + CharaData.GetRespawn(charaID).ToString();
        designField.text = "デザイン：" + CharaData.GetDesign(charaID);
        charaImage.sprite = Resources.Load<Sprite>("Images/FullChara/" + ename);
        for(int i = 0; i < 4; i++)
        {
            wazas[i].sprite = Resources.Load<Sprite>("Images/Waza/" + ename + "/" + i);
        }
    }
}
