using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GachaScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        int a = 99999;
        switch (DataBase.nowSelectGacha) {
            case 1:
                a = 1000;
                break;
            case 2:
                a = 2000;
                break;
            case 3:
                a = 5000;
                break;
            case 4:
                a = 10000;
                break;
        }
        if (a <= DataBase.GetPoint())
        {
            DataBase.PlusPoint(-a);
            Destroy(GameObject.Find("OpeningBGM").gameObject);
            DataBase.nowSelectLobby = 0;
            SceneManager.LoadScene("GachaScene");
        }
    }
}
