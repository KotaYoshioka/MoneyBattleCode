using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoubuzinData : MonoBehaviour
{
    //硬化
    //無敵時間
    public readonly static int KOKA_SPECIAL_SECS = 1;
    //ダメージ減少時間
    public readonly static int KOKA_DAMAGE_REDUCE_SECS = 3;
    //ダメージ減少数
    public readonly static int KOKA_DAMAGE_REDUCE_POWER = 50;

    //石紋
    //1つ目の岩のダメージ
    public readonly static int MON_FIRST_DAMAGE = 30;
    //2つ目の岩のダメージ
    public readonly static int MON_SECOND_DAMAGE = 80;
    //上方向への力
    public readonly static int MON_UPPER_FORCE = 600;
    //横方向への力
    public readonly static int MON_SIDE_FORCE = 400;
    //2つ目の石が出てくるまでの時間
    public readonly static float MON_SECOND_DELAY_SECS = 0.1f;

    //岩石落し
    //ダメージ
    public readonly static int OTOSHI_DAMAGE = 80;

    //巨石
    //存在時間
    public readonly static int KYO_LIVE_SECS = 8;
}
