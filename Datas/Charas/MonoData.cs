using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoData : MonoBehaviour
{
    //重力操作
    //ダメージ
    public readonly static int CONT_DAMAGE = 20;
    //相手を吹き飛ばす力
    public readonly static int CONT_FORCE = 1000;
    //相手が動けない時間
    public readonly static float CONT_STUCK_SECS = 0.8f;
    
    //無重力固定
    //相手へのスタン時間
    public readonly static int ZERO_STAN_SECS = 3;

    //浮遊
    //無重力時間(この技自体、アニメーション依存なので、変更の場合、アニメーションも)
    public readonly static int FUYU_LONG_SECS = 4;

    //超圧弾
    //ダメージ
    public readonly static int DAN_DAMAGE = 140;
    //弾の発射威力
    public readonly static int DAN_SPEED = 1000;
    //反動
    public readonly static int DAN_RECOIL = 600;
    //空中静止時間
    public readonly static float DAN_STOP_AIR_SECS = 1.5f;
}
