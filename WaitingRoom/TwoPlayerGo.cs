using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TwoPlayerGo : MonoBehaviourPunCallbacks
{

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                GetComponent<PhotonView>().RPC(nameof(GameStart), RpcTarget.All);
            }
        }
    }

    [PunRPC]
    public void GameStart()
    {
        PhotonNetwork.IsMessageQueueRunning = false;
        SceneManager.LoadScene("BattleGround");
    }
}
