using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomJoin : MonoBehaviourPunCallbacks
{
    [SerializeField]Text roomnameField;

    /// <summary>
    /// ボタンを押した時、書かれた部屋名に接続しようとする
    /// </summary>
    public void OnClick()
    {
        string naming = roomnameField.text;
        if (naming != "")
        {
            PhotonNetwork.JoinRoom(naming);
        }
    }
    /// <summary>
    /// 接続できた場合、待ち画面に移動
    /// </summary>
    public override void OnJoinedRoom()
    {
        SceneManager.LoadScene("WaitingRoom");
    }
}
