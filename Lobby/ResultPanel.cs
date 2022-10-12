using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{

    public Text winOrLose, gametime, useTime, useRate, getPoint, allPoint;
    // Start is called before the first frame update
    void Start()
    {
        Refresh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Refresh()
    {
        string result = null;
        int id = UserData.GetSelectChara();
        int pp = 0;
        if (DataBase.GetWin() == false)
        {
            result = "あなたの負け";
            DataBase.PlusCharaLose(id);
            pp = 100;
        }
        else
        {
            result = "あなたの勝ち";
            DataBase.PlusCharaWin(id);
            pp = 300;
        }

        winOrLose.text = result;
        gametime.text = DataBase.minutes.ToString() + "：" + DataBase.seconds.ToString();
        DataBase.PlusCharaUse(id);
        useTime.text = DataBase.GetCharaUse(id).ToString();
        useRate.text = ((DataBase.GetCharaWin(id) / DataBase.GetCharaUse(id)) * 100).ToString() + "%";
        DataBase.PlusPoint(pp + (DataBase.minutes * 40));
        getPoint.text = (pp + (DataBase.minutes * 40)).ToString();
        allPoint.text = DataBase.GetPoint().ToString();
        DataBase.Save();
    }
}
