using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : MonoBehaviour
{
    //���݁A�I�𒆂̃L����
    private static int selectCharaID = 0;

    //�L�����̏����֌W
    private static bool[] charaHave = {true,true,true,true,true,true,true,true,true,true,true};

    //Getter��Setter
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
