using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AkumuData : MonoBehaviour
{
    //導き
    //引き寄せる力
    public readonly static int MICHIBIKI_PULL_FORCE = 1500;
    //残留時間
    public readonly static float MICHIBIKI_LIVE_SECS = 0.2f;
    //スタン
    public readonly static float MICHIBIKI_STAN_SECS = 0.8f;

    //歩く影
    //歩く速度
    public readonly static int SHADOW_WALK_SPEED = 7;
    //残留時間
    public readonly static int SHADOW_LIVE_SECS = 15;
    //ダメージ
    public readonly static int SHADOW_DAMAGE = 60;
    //スタン
    public readonly static float SHADOW_STAN_SECS = 0.4f;

    //トラウマ
    //ダメージ
    public readonly static int TRAUMA_DAMAGE = 180;
    //スタン
    public readonly static int TRAUMA_STAN_SECS = 4;
    //残留時間
    public readonly static int TRAUMA_LIVE_SECS = 10;
    //トラウマの速度
    public readonly static int TRAUMA_SPEED = 15;
}
