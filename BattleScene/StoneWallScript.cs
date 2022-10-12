using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゴール前の壁の処理
/// </summary>
public class StoneWallScript : MonoBehaviour
{
    private int hp = 750;

    /// <summary>
    /// 壁がダメージを食らって、0になった時、壊れる
    /// </summary>
    /// <param name="a"></param>
    public void Damage(int a)
    {
        hp = hp - a;
        if (hp <= 0)
        {
            hp = 0;
        }
        if (hp == 0)
        {
            GetComponent<PhotonView>().RPC("RPC_Destroy", RpcTarget.AllViaServer);
        }
    }

    /// <summary>
    /// 壁が破壊される同期処理
    /// </summary>
    [PunRPC]
    void RPC_Destroy()
    {
        Destroy(gameObject);
    }
}
