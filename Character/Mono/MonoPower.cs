using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoPower : MonoBehaviour
{
    private string ownerID;

    //右に飛ばすかどうか
    private bool right = false;

    GameObject master;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Delete");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ヒット側の処理か
        if(PhotonNetwork.LocalPlayer.UserId != ownerID)
        {
            GameObject enemy = collision.gameObject;
            if(enemy.tag == "Player")
            {
                if(enemy.GetComponent<PhotonView>().Owner.UserId != ownerID)
                {
                    enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(MonoData.CONT_FORCE * (right?1:-1),0));
                    enemy.GetComponent<CharaBase>().TempStuck(MonoData.CONT_STUCK_SECS);
                    enemy.GetComponent<CharaBase>().Damage(MonoData.CONT_DAMAGE);
                }
            }
        }
    }

    public void Right(bool right)
    {
        this.right = right;
    }

    public void SetMaster(string ownerID)
    {
        this.ownerID = ownerID;
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
