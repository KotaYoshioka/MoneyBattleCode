using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoubuzinIwa : MonoBehaviour
{
    private string ownerID;

    private bool right;

    [SerializeField] bool first;

    //音声関係
    private AudioSource audioSource;
    [SerializeField] AudioClip rumblingSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine("Delete");
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(1.2f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject enemy = collision.gameObject;
        if (PhotonNetwork.LocalPlayer.UserId != ownerID)
        {
            if(enemy.tag == "Player")
            {
                if(enemy.GetComponent<PhotonView>().Owner.UserId != ownerID)
                {
                    enemy.GetComponent<CharaBase>().Damage(first?KoubuzinData.MON_FIRST_DAMAGE:KoubuzinData.MON_SECOND_DAMAGE);
                    enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(KoubuzinData.MON_SIDE_FORCE * (right?1:-1), KoubuzinData.MON_UPPER_FORCE));
                }
            }else if(enemy.tag == "StoneWall")
            {
                //enemy.GetComponent<StoneWallScript>().Damage((int)CharaAbiData.Koubuzin(3), master.name);
            }
        }
        if(enemy.tag == "Projectile")
        {
            if(PhotonNetwork.LocalPlayer.UserId == enemy.GetComponent<PhotonView>().Owner.UserId)
            {
                enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(KoubuzinData.MON_SIDE_FORCE * (right ? 1 : -1), KoubuzinData.MON_UPPER_FORCE));
            }
        }
    }

    public void SetOwnerID(string id,bool right)
    {
        ownerID = id;
        this.right = right;
    }
}
