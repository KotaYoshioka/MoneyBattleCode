using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoubuzinFallStone : MonoBehaviour
{

    private string ownerID;

    private PhotonView pv;

    //音関係
    private AudioSource audioSource;
    public AudioClip fallSE;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        pv = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.position.y <= -12)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// ヒット処理
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ヒット側の処理であり、本人じゃないなら、ダメージ
        if(PhotonNetwork.LocalPlayer.UserId != ownerID)
        {
            GameObject enemy = collision.gameObject;
            if(enemy.tag == "Player")
            {
                if (enemy.GetComponent<PhotonView>().Owner.UserId != ownerID)
                {
                    enemy.GetComponent<CharaBase>().Damage(KoubuzinData.OTOSHI_DAMAGE);
                }
            }else if(enemy.tag == "StoneWall")
            {
                enemy.GetComponent<StoneWallScript>().Damage(KoubuzinData.OTOSHI_DAMAGE);
            }
        }
    }

    public void SetOwnerID(string ownerID)
    {
        //this.ownerID = ownerID;
        if(ownerID!=null)pv.RPC(nameof(AsyncSetDatas), RpcTarget.All, ownerID);
    }
    [PunRPC]
    public void AsyncSetDatas(string ownerID)
    {
        this.ownerID = ownerID;
    }
}
