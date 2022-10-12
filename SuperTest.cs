using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperTest : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.SerializationRate = 20;
        DataBase.ResetWin();
        if (DataBase.superMode == 0)
        {
            GameObject memory = GameObject.Find("Yeah").gameObject;
            Destroy(GameObject.Find("OpeningBGM").gameObject);
            string charaName = null;
            charaName = "Character/" + CharaData.GetCharaNameEnglish(UserData.GetSelectChara());
            GameObject c = PhotonNetwork.Instantiate(charaName, new Vector3(7.3f, 6.7f), Quaternion.identity);
            if (memory.GetComponent<OneOrTwoMemory>().AGet() == 1)
            {
                c.transform.position = new Vector2(-8.3f, 6.7f);
                c.GetComponent<PlayerBase>().Player1Change();
                c.name = "1" + CharaData.GetCharaNameEnglish(UserData.GetSelectChara());
                c.layer = 10;
            }
            else
            {
                c.name = "2" + CharaData.GetCharaNameEnglish(UserData.GetSelectChara());
                c.layer = 11;
            }
            c.GetComponent<PlayerBase>().SetID(UserData.GetSelectChara());
            Destroy(memory);
        }
        else
        {
            string charaName = null;
            charaName = "Character/" + CharaData.GetCharaNameEnglish(UserData.GetSelectChara());
            GameObject c = PhotonNetwork.Instantiate(charaName, new Vector3(7.3f, 6.7f), Quaternion.identity);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
