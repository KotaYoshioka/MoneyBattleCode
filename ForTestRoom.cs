using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForTestRoom : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        //サーバーに接続
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinOrCreateRoom("room", new Photon.Realtime.RoomOptions(), TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        GameObject a = PhotonNetwork.Instantiate("Character/" + CharaData.GetCharaNameEnglish(UserData.GetSelectChara()), new Vector3(0, 0), Quaternion.identity);
        a.GetComponent<PlayerBase>().SetID(UserData.GetSelectChara());
    }
}
