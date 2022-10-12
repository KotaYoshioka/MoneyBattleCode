using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamiyaData : MonoBehaviour
{
    //馬駆逐
    //ダメージ
    public readonly static int BAKU_DAMAGE = 150;
    //自分へのダメージ
    public readonly static int BAKU_SELF_DAMAGE = 100;
    //サイレンス時間
    public readonly static int BAKU_SILENCE_SECS = 3;

    //重力馬
    //継続ダメージ
    public readonly static int GRAVITY_DAMAGE = 5;
    //スロー
    public readonly static int GRAVITY_SLOW_LEVEL = -25;
    //間隔
    public readonly static float GRAVITY_INTERVAL = 0.5f;
    //存在時間
    public readonly static int GRAVITY_LIVE_SECS = 8;

    //馬頭
    //ダメージ
    public readonly static int BATO_DAMAGE = 60;

    //馬化
    //存在時間
    public readonly static int BAKA_LIVE_SECS = 1;

}
