using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム中に出現するお金のシステム
/// </summary>
public class MoneyScript : MonoBehaviour
{
    private PhotonView pv;

    private AudioSource audioSource;
    public AudioClip coinSE;

    void Start()
    {
        pv = GetComponent<PhotonView>();
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// コインがステージ外に移動した際、手に取れる位置まで戻すシステム。
    /// </summary>
    void Update()
    {
        if(gameObject.transform.position.y <= -4.5f)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, -4.5f);
        }else if(gameObject.transform.position.y > 0)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, 0);
        }
    }

    /// <summary>
    /// 触れたプレイヤーがコインを取得するシステム。
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if(obj.tag == "Player" && PhotonNetwork.IsMasterClient)
        {
            obj.GetComponent<CharaBase>().SetMoney(true);
            pv.RPC(nameof(RPC_Destroy), RpcTarget.All);
        }
    }

    [PunRPC]
    void RPC_Destroy()
    {
       Destroy(gameObject);
    }
}
