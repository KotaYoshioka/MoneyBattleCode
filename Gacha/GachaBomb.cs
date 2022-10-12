using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaBomb : MonoBehaviour
{
    public GameObject bakuhu, iwa, lightA, syosai;
    ParticleSystem ps;
    public Animator a;
    public AudioClip otiru, bakuha, getSE;
    AudioSource myAudio;
    long kakuritu;
    int charaID;
    // Start is called before the first frame update
    void Start()
    {
        iwa.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Iwa/" + DataBase.nowSelectGacha);
        ps = bakuhu.GetComponent<ParticleSystem>();
        ps.Stop();
        kakuritu = Random.Range(1, 10001);
        charaID = DataBase.GachaChara(kakuritu);
        myAudio = GetComponent<AudioSource>();
        Instantiate(Resources.Load("Gacha/" +CharaData.GetCharaNameEnglish(charaID)));
        UserData.SetSelectChara(charaID);
        DataBase.SetChara(charaID);
        DataBase.Save();
        StartCoroutine("SoundLag");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        StartCoroutine("DestroyIwa");
        StartCoroutine("LightOn");
        StartCoroutine("DestroyMe");
        bakuhu.SetActive(true);
        ps.Play();
        myAudio.PlayOneShot(bakuha);
        myAudio.PlayOneShot(getSE);
        gameObject.GetComponent<SpriteRenderer>().color = Color.clear;
    }

    IEnumerator SoundLag()
    {
        yield return new WaitForSeconds(0.2f);
        myAudio.PlayOneShot(otiru);
    }
    IEnumerator DestroyIwa()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(iwa);
    }

    IEnumerator LightOn()
    {
        yield return new WaitForSeconds(0.7f);
        lightA.SetActive(true);
        a.enabled = true;
    }
    IEnumerator DestroyMe()
    {
        yield return new WaitForSeconds(2);
        Destroy(bakuhu);
        StartCoroutine("ReallyDestroyMe");
        syosai.SetActive(true);
    }
    IEnumerator ReallyDestroyMe()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
