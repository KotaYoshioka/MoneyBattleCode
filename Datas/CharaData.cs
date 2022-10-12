using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaData
{
    //<------キャラの情報------>
    //0:キャラ名(string)
    //1:英キャラ名(string)
    //2:最大体力(int)
    //3:移動速度(float)
    //4:ジャンプ力(int)
    //5:ジャンプ回数(int)
    //6:復活時間(int)
    //7:デザイン(string)
    private static object[,] charas = {
        {"コウブジン",   "Koubuzin",    600,    5.6f,   550,    1, 12,  "熱男"},
        {"モノ",         "Mono",        300,    4.5f,   650,    2,  6,  "ラタ"},
        {"フリート",     "Freat",       250,    4.7f,   600,    1,  5,  "タコス"},
        {"アクム",       "Akumu",       500,    3.6f,   400,    2, 18,  "ラタ"},
        {"ブランチェ",   "Branche",     400,    4.2f,   350,    2, 10,  "熱男"},
        {"クレガのアイツ","CrazyA",     300,    4.7f,   600,    3, 14,  "デュクシ・松岡・デュクシ"},
        {"ダルマン",     "Daruman",     350,    3.8f,   200,    4,  8,  "みーたん"},
        {"おどろく望田", "OdorokuMotita",350,   4.3f,   300,    2,  8,  "とらのじ"},
        {"ニコ",         "Nico",        350,    4.5f,   550,    2,  7,  "ラタ"},
        {"カミヤ",       "Kamiya",      400,    4.9f,   700,    1,  6,  "カミヤ"},
        {"ニンザード",   "Ninzard",     150,    8.2f,   500,    2,  8,  "熱男"}
    };

    //<------技------->
    //0:技名(string)
    //1:クールダウン(int)
    //2:技の説明(string)
    private static object[,,] waza =
    {
        //コウブジン
        {
            {"硬化",17,"発動してから" + KoubuzinData.KOKA_SPECIAL_SECS + "秒間、無敵状態になり、その後、" + KoubuzinData.KOKA_DAMAGE_REDUCE_SECS + "秒間、" + KoubuzinData.KOKA_DAMAGE_REDUCE_POWER + "ダメージ軽減が発生する。また、この技の効果中、自分の移動速度は半減する。"},
            {"石紋",12,"向いている方向に向かって、地面から岩を1つ飛び出させる。その" + KoubuzinData.MON_SECOND_DELAY_SECS + "秒後、もう1つ岩が飛び出る。接触した敵プレイヤーを吹き飛ばし、1つ目の岩は" + KoubuzinData.MON_FIRST_DAMAGE + "ダメージ、2つ目の岩は" + KoubuzinData.MON_SECOND_DAMAGE + "ダメージ与える。"},
            {"岩石落し",6,"向いている方向の天井から岩を落す。岩にぶつかった敵プレイヤーに" + KoubuzinData.OTOSHI_DAMAGE + "ダメージを与える。"},
            {"巨石",35,"向いている方向に大きな石の壁が" + KoubuzinData.KYO_LIVE_SECS + "秒屹立する。"}
        },
        //モノ
        {
            {"重力操作",14,""},
            {"無重力固定",20,""},
            {"浮遊",12,""},
            {"超圧弾",13,""}
        },
        //フリート
        {
            {"脅かかし",16,""},
            {"大かかし",22,""},
            {"爆かかし",3,""},
            {"似かかし",4,""}
        },
        //アクム
        {
            {"導き",15,""},
            {"歩く影",7,""},
            {"トラウマ",23,""},
            {"監視",0,""}
        },
        //ブランチェ
        {
            {"植林",17,""},
            {"木の葉",5,""},
            {"生え変わり",15,""},
            {"枝折り",3,""}
        },
        //クレガのアイツ
        {
            {"漫画の中で",18,""},
            {"武器屋のすすめ",15,""},
            {"ポンカンショット",2,""},
            {"ダイナミック自爆",25,""}
        },
        //ダルマン
        {
            {"体当たり",10,""},
            {"大回転",8,""},
            {"だるまさんが転んだ",0,""},
            {"起き上がり",0,""}
        },
        //おどろく望田
        {
            {"それはないだろ！？",7,""},
            {"痛てぇなおい！！",5,""},
            {"黙れって！",16,""},
            {"全然壊れねぇな！",17,""}
        },
        //ニコ
        {
            {"逆世界",4,""},
            {"気づかぬうちに",9,""},
            {"探し物",8,""},
            {"閉幕",135,""}
        },
        //カミヤ
        {
            {"馬駆逐",6,""},
            {"重力馬",16,""},
            {"馬頭",10,""},
            {"馬化",4,""}
        },
        //ニンザード
        {
            {"くない",1,""},
            {"身代わり",10,""},
            {"まきびし",5,""},
            {"退却術",15,""}
        }
    };

    //GetterとSetter
    public static string GetCharaName(int charaID)
    {
        return (string)charas[charaID, 0];
    }
    public static string GetCharaNameEnglish(int charaID)
    {
        return (string)charas[charaID, 1];
    }
    public static int GetCharaLength()
    {
        return charas.GetLength(0);
    }

    public static int GetHP(int charaID)
    {
        return (int)charas[charaID,2];
    }
    public static float GetSpeed(int charaID)
    {
        return (float)charas[charaID, 3];
    }
    public static int GetJump(int charaID)
    {
        return (int)charas[charaID, 4];
    }
    public static int GetJumpTime(int charaID)
    {
        return (int)charas[charaID, 5];
    }
    public static int GetRespawn(int charaID)
    {
        return (int)charas[charaID, 6];
    }
    public static string GetDesign(int charaID)
    {
        return (string)charas[charaID, 7];
    }
    public static string GetAbilityName(int charaID,int abilityID)
    {
        return (string)waza[charaID, abilityID, 0];
    }
    public static int GetCooldown(int charaID,int abilityID)
    {
        return (int)waza[charaID, abilityID,1];
    }
}
