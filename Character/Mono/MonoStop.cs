using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoStop : MonoBehaviour
{
    private string ownerID;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Delete");
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
                    enemy.GetComponent<CharaBase>().TempStan(MonoData.ZERO_STAN_SECS);
                    enemy.GetComponent<CharaBase>().TempNoGravity(MonoData.ZERO_STAN_SECS);
                }
            }
        }
    }

    public void SetMaster(string ownerID)
    {
        this.ownerID = ownerID;
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
