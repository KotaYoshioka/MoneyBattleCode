using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreatData : MonoBehaviour
{
    //脅かかし
    //スローの力
    public readonly static int ODO_SLOW_LEVEL = -50;
    //かかしの残留時間
    public readonly static int ODO_LIVE_SECS = 6;

    //大かかし
    //残留時間
    public readonly static int DAI_LIVE_SECS = 10;

    //爆かかし
    //爆発までの時間
    public readonly static int BAKU_EXPLOSION_DELAY_SECS = 4;
    //爆発のダメージ
    public readonly static int BAKU_EXPLOSION_DAMAGE = 55;
    //自分が食らう爆発のダメージ
    public readonly static int BAKU_EXPLOSION_SELF_DAMAGE = 0;

    //似かかし
    //残留時間
    public readonly static int NI_LIVE_SECS = 20;
}
