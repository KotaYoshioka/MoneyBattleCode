using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 見えないゴール判定の処理
/// </summary>
public class GoalWallScript : MonoBehaviour
{
    [SerializeField] BattleManager bm;
    [SerializeField] bool p1;

    /// <summary>
    /// 正しい勝利判定を触った場合
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        GameObject obj = collision.gameObject;
        if(obj.tag == "Player")
        {
            CharaBase playerdata = obj.GetComponent<CharaBase>();
            if (!playerdata.GetMoney())
            {
                return;
            }
            if(playerdata.Is1P() == p1)
            {
                Win();
            }
        }
    }

    /// <summary>
    /// 勝利判定
    /// </summary>
    private void Win()
    {
        bm.GameEnd(p1);
    }
}
