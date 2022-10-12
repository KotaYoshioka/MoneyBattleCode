using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// クリックされた時、タブを開くボタンの処理
/// </summary>
public class TabButton : MonoBehaviourPunCallbacks
{
    [SerializeField] int tabid;

    [SerializeField] TabManager manager;

    [SerializeField] GameObject waitTab;

    public void OnClick()
    {
        if(tabid == 0 && !PhotonNetwork.IsConnected)
        {
            //ロゴボタンだった場合、サーバーに接続する。
            PhotonNetwork.ConnectUsingSettings();
            waitTab.SetActive(true);
        }
        else {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.Disconnect();
            }
            manager.OpenTab(tabid);
        }
    }

    public override void OnConnectedToMaster()
    {
        if (tabid == 0)
        {
            waitTab.SetActive(false);
            manager.OpenTab(tabid);
        }
    }
}
