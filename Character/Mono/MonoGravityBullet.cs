using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoGravityBullet : MonoBehaviour
{
    private bool right;

    private string ownerID;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Launch");
        gameObject.layer = 9;
    }

    public void Right(bool right)
    {
        this.right = right;
    }

    IEnumerator Launch()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.layer = 8;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(MonoData.DAN_SPEED * (right?1:-1),0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ヒット側の処理か
        if (PhotonNetwork.LocalPlayer.UserId != ownerID)
        {
            GameObject enemy = collision.gameObject;
            if (enemy.tag == "Player")
            {
                if (enemy.GetComponent<PhotonView>().Owner.UserId != ownerID)
                {
                    enemy.GetComponent<CharaBase>().Damage(MonoData.DAN_DAMAGE);
                }
            }else if(enemy.tag == "StoneWall")
            {
                enemy.GetComponent<StoneWallScript>().Damage(MonoData.DAN_DAMAGE);
            }
        }
    }

    public void SetMaster(string ownerID)
    {
        this.ownerID = ownerID;
    }
}
