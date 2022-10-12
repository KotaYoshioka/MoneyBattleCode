using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyAData : MonoBehaviour
{
    //漫画の中で
    //スロー
    public readonly static int INCOMIC_SLOW_LEVEL = -40;
    //速度上昇
    public readonly static int INCOMIC_ADDSPEED_LEVEL = 250;
    //消滅時間
    public readonly static int INCOMIC_LIVE_SECS = 8;

    //武器屋のすすめ
    //ダメージ
    public readonly static int WEAPON_DAMAGE = 20;

    //ポンカンショット
    //ダメージ
    public readonly static int PONKAN_DAMAGE = 20;
    //スタン
    public readonly static int PONKAN_STAN_SECS = 1;

    //ダイナミック自爆
    //爆発までの時間
    public readonly static int DYNAMIC_EXPLOSION_DELAY_SECS = 2;
    //ダメージ
    public readonly static int DYNAMIC_DAMAGE = 200;
}
