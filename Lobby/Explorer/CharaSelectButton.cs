using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaSelectButton : MonoBehaviour
{
    private int charaID;
    private GameObject detail;

    //音声関係
    private AudioSource audioSource;
    [SerializeField] AudioClip selectSE;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// ボタンを押された時、特定のキャラを選択する。
    /// </summary>
    public void OnClick()
    {
        UserData.SetSelectChara(charaID);
        detail.GetComponent<DetailPanel>().Refresh(charaID);
        //音
        AudioData.PlaySE(audioSource, selectSE);
    }

    /// <summary>
    /// ボタンに必要なデータをセットする。
    /// </summary>
    public void SetMetadata(int charaID,GameObject detailTab)
    {
        this.charaID = charaID;
        GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/SelectIcon/" + CharaData.GetCharaNameEnglish(charaID));
        detail = detailTab;
    }
    /// <summary>
    /// 持っていない状態にする
    /// </summary>
    public void SetMisyoji(int charaID)
    {
        GetComponent<Button>().interactable = false;
        GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/SelectIcon/Misyutoku");
    }
}
