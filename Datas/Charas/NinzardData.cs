using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinzardData : MonoBehaviour
{
    //くない
    //ダメージ
    public readonly static int KUNAI_DAMAGE = 20;
    //壁へのダメージ
    public readonly static int KUNAI_WALL_DAMAGE = 15;
    //飛ばす力
    public readonly static int KUNAI_SPEED = 500;

    //身代わり
    //走る力
    public readonly static int DECOI_SPEED = 400;
    //透明の時間
    public readonly static int DECOI_INVISIBLE_SECS = 3;

    //まきびし
    //ダメージ
    public readonly static int MAKIBISHI_DAMAGE = 10;
    //スロー
    public readonly static int MAKIBISHI_SLOW_POWER = -40;
    //スロー時間
    public readonly static int MAKIBISHI_SLOW_SECS = 3;
    //残留時間
    public readonly static int MAKIBISHI_LIVE_SECS = 13;

    //退却術
    //後退する力
    public readonly static int BACK_FORCE = 1500;
}
