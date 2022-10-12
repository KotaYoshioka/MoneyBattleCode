using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarumanData : MonoBehaviour
{
    //体当たり
    //突進力
    public readonly static int TACKLE_FORCE = 1000;
    //ダメージ
    public readonly static int TACKLE_DAMAGE = 15;

    //大回転
    //ジャンプ力
    public readonly static int CYCLE_JUMP_FORCE = 800;

    //だるまさんが転んだ
    //確率
    public readonly static int FELLOVER_PERCENTAGE = 2;
    //スタン
    public readonly static float FELLOVER_STAN_SECS = 1.5f;
    //存在時間
    public readonly static float FELLOVER_LIVE_SECS = 0.2f;

    //起き上がり
    public readonly static int AGAIN_PERCENTAGE = 2;
}
