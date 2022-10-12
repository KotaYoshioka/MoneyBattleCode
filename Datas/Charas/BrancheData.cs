using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrancheData : MonoBehaviour
{
    //植林
    //回復間隔
    public readonly static int PLANT_HEAL_INTERVAL_SECS = 2;
    //回復力
    public readonly static int PLANT_HEAL_POWER = 30;
    //消滅時間
    public readonly static int PLANT_LIVE_SECS = 12;

    //木の葉
    //ダメージ
    public readonly static int LEAF_DAMAGE = 20;
    //消滅時間
    public readonly static int LEAF_LIVE_SECS = 40;

    //生え変わり
    //ポインターの移動速度
    public readonly static int NEWLIFE_SPEED = 300;

    //枝折り
    //ダメージ
    public readonly static int STICK_DAMAGE = 60;
    //速度
    public readonly static int STICK_SPEED = 600;
    //自分が食らうダメージ
    public readonly static int STICK_SELF_DAMAGE = 30;
}
