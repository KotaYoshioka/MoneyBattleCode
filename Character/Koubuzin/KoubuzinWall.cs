using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoubuzinWall : MonoBehaviour
{
    //アニメーション関係
    Animator animator;

    //音声関係
    private AudioSource audioSource;
    [SerializeField] AudioClip pileUpSE;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(nameof(Fallout));
    }

    IEnumerator Fallout()
    {
        yield return new WaitForSeconds(KoubuzinData.KYO_LIVE_SECS);
        animator.SetBool("WallStand", false);
    }

    public void Delete()
    {
        Destroy(gameObject);
    }
}

