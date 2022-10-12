using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomCreateScript : MonoBehaviour
{
    [SerializeField]Text roomnameField;

    /// <summary>
    /// 入力された部屋名で部屋を作成する。
    /// </summary>
    public void OnClick()
    {
        string naming = roomnameField.text;
        if(naming != "" && naming != "super")
        {
            PhotonNetwork.JoinOrCreateRoom(naming, new RoomOptions(), TypedLobby.Default);
        }
    }
}
