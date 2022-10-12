using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorerManager : MonoBehaviour
{
    [SerializeField] GameObject charaList;
    [SerializeField] GameObject charaButton;
    [SerializeField] GameObject detailTab;


    void Start()
    {
        //キャラの選択ボタンの形成
        for(int i = 0; i < CharaData.GetCharaLength(); i++)
        {
            GameObject button = Instantiate(charaButton);
            CharaSelectButton buttonData = button.GetComponent<CharaSelectButton>();
            //キャラ所持時、普通のボタンを形成
            if (UserData.GetHaveChara(i))
            {
                buttonData.SetMetadata(i, detailTab);
            }
            //持っていない場合、未所持を表示
            else
            {
                buttonData.SetMisyoji(i);
            }
            buttonData.transform.SetParent(charaList.transform);
        }    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
