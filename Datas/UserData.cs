using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : MonoBehaviour
{
    //現在、選択中のキャラ
    private static int selectCharaID = 0;

    //キャラの所持関係
    private static bool[] charaHave = {true,true,true,true,true,true,true,true,true,true,true};

    //GetterとSetter
    public static int GetSelectChara()
    {
        return selectCharaID;
    }
    public static void SetSelectChara(int id)
    {
        selectCharaID = id;
    }
    public static bool GetHaveChara(int id)
    {
        return charaHave[id];
    }
}
