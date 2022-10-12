using ExitGames.Client.Photon;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoBehaviourPunCallbacks
{
    //使用しているキャラ
    GameObject player;
    CharaBase playerData;

    //UI
    [SerializeField] AbilityImage[] abilities;
    [SerializeField] FaceImage face;
    [SerializeField] TextMeshProUGUI HPText;
    [SerializeField] TextMeshProUGUI gameEndText;
    [SerializeField] GameObject moneyImage;
    [SerializeField] TextMeshProUGUI instructionText;

    //カウントダウン
    [SerializeField]GameObject countdownPanel;
    private TextMeshProUGUI countdown;
    private PhotonView pv;
   
    void Start()
    {
        pv = GetComponent<PhotonView>();

        //プレイヤーの召喚
        PhotonNetwork.IsMessageQueueRunning = true;
        Vector3 spawnLocation = new Vector3(7.3f, 6.7f);
        if (!PhotonNetwork.IsMasterClient)
        {
            spawnLocation = new Vector2(-8.3f, 6.7f);
        }
        else
        {
            var timer = new ExitGames.Client.Photon.Hashtable();
            timer["remain"] = 3;
            PhotonNetwork.CurrentRoom.SetCustomProperties(timer);
        }
        player = PhotonNetwork.Instantiate("Character/" + CharaData.GetCharaNameEnglish(UserData.GetSelectChara()), spawnLocation, Quaternion.identity);
        playerData = player.GetComponent<CharaBase>();
        if (!PhotonNetwork.IsMasterClient) playerData.TurnAround();

        //UI設定
        int charaID = playerData.GetCharaID();
        for(int i = 0; i < 4; i++)
        {
            abilities[i].SetImage(charaID, i);
        }
        face.SetImage(charaID);
        playerData.SetUI(this,abilities, face,HPText,moneyImage,instructionText);

        //カウントダウン設定
        countdownPanel.SetActive(true);
        countdown = countdownPanel.GetComponentInChildren<TextMeshProUGUI>();
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        if(propertiesThatChanged["remain"] != null)
        {
            int count = (int)propertiesThatChanged["remain"];
            if(count == 0)
            {
                countdownPanel.SetActive(false);
                playerData.SetKey(true);
            }
            else
            {
                countdown.text = count.ToString();
                if (PhotonNetwork.IsMasterClient)
                {
                    StartCoroutine(nameof(Countdown),count-1);
                }
            }
        }
    }

    IEnumerator Countdown(int count)
    {
        yield return new WaitForSeconds(1);
        var timer = new ExitGames.Client.Photon.Hashtable();
        timer["remain"] = count;
        PhotonNetwork.CurrentRoom.SetCustomProperties(timer);
    }

    /// <summary>
    /// ゲーム終了の文字を表示すると同時に、操作を止める処理。
    /// </summary>
    public void GameEnd(bool isWinnerP1)
    {
        pv.RPC(nameof(AsyncGameEnd), RpcTarget.All,isWinnerP1);
    }
    [PunRPC]
    public void AsyncGameEnd(bool isWinnerP1)
    {
        playerData.SetKey(false);
        gameEndText.text = "Player" + (isWinnerP1?1:2) + "の勝利！";
        StartCoroutine(nameof(BackTitleDelay));
    }
    IEnumerator BackTitleDelay()
    {
        yield return new WaitForSeconds(5);
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Lobby");
    }

    public void SendSetInstruction(bool money)
    {
        pv.RPC(nameof(SetInstruction), RpcTarget.All,money);
    }
    [PunRPC]
    public void SetInstruction(bool money)
    {
        if (!money)
        {
            instructionText.text = "マネーを取れ！";
            return;
        }
        if (playerData.GetMoney())
        {
            instructionText.text = (PhotonNetwork.IsMasterClient?"左":"右") + "に持っていけ！";
            return;
        }
        else
        {
            instructionText.text = "敵からマネーを奪え！";
        }
    }
}
