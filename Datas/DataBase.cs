using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataBase : MonoBehaviour
{
    //技の説明
    /*
    private static string[,] wazaDetail = { { "", "", "", "", "" },
                                            { "コウブジンの説明",
                                                  "足が遅くなる代わりに" + CharaAbiData.Koubuzin(0) + "秒間無敵になり、その後" + CharaAbiData.Koubuzin(1) + "秒間食らうダメージを"+ CharaAbiData.Koubuzin(2) + "減少させる。",
                                                  "向いている方向に地面から岩石が２つ飛び出る。当たったプレイヤーに"+ CharaAbiData.Koubuzin(3)+ "ダメージを与えて上へ吹っ飛ばす。",
                                                  "自分の近く、向いている方向側に岩石を落す。当たったプレイヤーに"+CharaAbiData.Koubuzin(5) + "ダメージを与える。",
                                                  "向いている方向に岩石の壁を出現させる。一定時間後、壁は消える。" } ,
                                                { "モノの説明",
                                                  "自分の近く、左右にいるプレイヤーの重力を操って吹き飛ばし、"+ CharaAbiData.Mono(8) +"ダメージ与える。",
                                                  "自分から一定範囲内にいる全てのプレイヤーの重力を失わせて"+ CharaAbiData.Mono(2) + "秒間完全にその場に固定させる。",
                                                  "自分の重力を無くして、回転しながら" + CharaAbiData.Mono(4) + "秒間自由に浮遊できるようになる。",
                                                  "一点に重力を集中させ高圧力の重力弾を作り出し発射する。自分も反動で吹き飛ぶ。ぶつかった相手に"+ CharaAbiData.Mono(5) +"ダメージを与える。" },
                                                { "フリートの説明",
                                                  "その場に赤いかかしを設置する。赤いかかしから一定範囲内に入ったプレイヤーに"+ CharaAbiData.Freat(1) +"%スローを与える。赤いかかしは" + CharaAbiData.Freat(1) + "秒間残る",
                                                  "その場に壁となる大きなかかしを設置する。大きなかかしは、必ず左に倒れる。大きなかかしは"+CharaAbiData.Freat(2) + "秒間残る",
                                                  "その場に自分と似ているかかしを設置する。そのかかしは、" + CharaAbiData.Freat(3) + "秒後に爆発する。その爆発に当たったプレイヤーは" + CharaAbiData.Freat(4) + "ダメージを食らう。",
                                                  "自分と全く同じかかしを、自分左右ランダムな位置にそれぞれ1体ずつ配置する。特に効果を持たない。" + CharaAbiData.Freat(5) + "秒後、消える。",},
                                                {"アクムの説明",
                                                 "自分から一定範囲内にいるプレイヤーを強い力で自分の方向へ引き寄せる。",
                                                 "向いている方向にひたすら歩き続ける影が現れる。影はプレイヤーにぶつかると" + CharaAbiData.Akumu(5) + "ダメージを与え、さらにその影の位置に自分はテレポートする。歩く影は" + CharaAbiData.Akumu(4) + "秒後に消滅する。",
                                                 "大きな体のトラウマを召喚して、向いている方向に走らせる。トラウマにぶつかったプレイヤーに"+ CharaAbiData.Akumu(7) +"ダメージと"+ CharaAbiData.Akumu(8) + "秒のスタンを与える。トラウマは自陣側の壁にもダメージを与えてしまう。トラウマは"+ CharaAbiData.Akumu(9) +"秒後に消滅する。",
                                                 "[パッシブ]自分が死亡した時、相手から見えない影になり、移動することができる。復活するとき、その影の位置から復活する。",},
                                                {"ブランチェの説明",
                                                 "その場に木を植える。木の近くにいるとき、" + CharaAbiData.Branche(0) + "秒ごとに" + CharaAbiData.Branche(1) + "回復する。自分以外のプレイヤーはこの木の恩恵を受けることはできない。この木は"+CharaAbiData.Branche(2) + "秒後に消滅する。",
                                                 "近くに木の葉を散らす。木の葉にぶつかったプレイヤーは"+ CharaAbiData.Branche(3) +"ダメージを与える。木の葉は" + CharaAbiData.Branche(4) + "秒後に消滅する。" ,
                                                 "相手に見えない緑のポインターを前方に飛ばす。ポインターが存在する状態でもう一度この技を使用すると、そのポインターの位置に自分はテレポートする。ポインターはステージオブジェクトにぶつかると壊れる。",
                                                 "自分の体力を" + CharaAbiData.Branche(8) + "ダメージ削って、枝を投げる。枝にぶつかったプレイヤーに" + CharaAbiData.Branche(6) +"ダメージ与える。"} ,
                                                {"クレガのアイツの説明",
                                                 "その場に漫画のコマを展開する。その中に入っている間、相手は"+ CharaAbiData.CrazyA(0) +"%スローが発生し、自分は"+ CharaAbiData.CrazyA(1) +"%移動速度上昇が発生する。漫画のコマは" + CharaAbiData.CrazyA(2) +"秒後に消滅する。",
                                                 "武器屋をその場に召喚して、3つの武器を縦横に投げてもらう。武器にぶつかったプレイヤーは" + CharaAbiData.CrazyA(3) +"ダメージが当たり、武器が回転して投げられるため、多段ダメージを食らうこともある。" ,
                                                 "その場からポンカンを向いている方向に投げつける。ぶつかったプレイヤーに" + CharaAbiData.CrazyA(6) +"ダメージと" + CharaAbiData.CrazyA(7) +"秒スタンを与える。ポンカンはステージの壁にぶつかると壊れる。",
                                                 "使用後即時に体力が尽き、遺影が置かれる。その遺影は" + CharaAbiData.CrazyA(4) + "秒後に爆発し、当たったプレイヤーに"  + CharaAbiData.CrazyA(5) +"ダメージを与える。また、この方法で自分が死亡した時、次の復活時間は半分になる。"},
                                                {"ダルマンの説明",
                                                 "向いている方向に回転しながら突進する。突進中、プレイヤーへの当たり判定ができる。また、突進中に操作することはできない。ぶつかったプレイヤーに"+ CharaAbiData.Daruman(1) +"ダメージを多段で与える。",
                                                 "その場で上に飛びながら大回転をする。突進中、プレイヤーへの当たり判定ができる。また、突進中に操作することはできない。ぶつかったプレイヤーに"+ CharaAbiData.Daruman(1) +"ダメージを多段で与える。",
                                                 "[パッシブ]攻撃を食らった際に、"+ CharaAbiData.Daruman(3) +"割の確立で近くにいるプレイヤーを"+ CharaAbiData.Daruman(5) +"秒スタンさせる。",
                                                 "[パッシブ]攻撃を食らった際に、"+ CharaAbiData.Daruman(4) +"割の確立でその攻撃を避ける。"} ,
                                                {"驚く望田の説明",
                                                 "目の前にフキダシを出して、ぶつかったプレイヤーに" + CharaAbiData.Odomoti(0) + "ダメージを与える。また、衝突した飛び道具を消すことができる。",
                                                 "[パッシブ]CDが完了しているときに攻撃を食らうと、目の前にフキダシを出す。ぶつかったプレイヤーに" + CharaAbiData.Odomoti(1) + "ダメージを与える。この技で出たフキダシがプレイヤー、または石の壁にダメージを与えれなかったとき、この技のCDは完了する。",
                                                 "目の前にフキダシを出して、ぶつかったプレイヤーにサイレンスを" + CharaAbiData.Odomoti(4) + "秒与えて、自分に"+ CharaAbiData.Odomoti(8) +"秒スタンを与える。",
                                                 "目の前にフキダシを出して、石の壁に" + CharaAbiData.Odomoti(6) + "ダメージを与える。この技はプレイヤーにダメージを与えないが、この技を"+CharaAbiData.Odomoti(9)+"回ぶつけられたプレイヤーは即死する。"},
                                                {"ニコの説明",
                                                 "自分の重力を反転させる。",
                                                 "自分の真下と真上から無数の手が現れる。ぶつかったプレイヤーに" + CharaAbiData.Nico(0) +"ダメージを与え、反対方向へ大きく吹き飛ばす。",
                                                 "自分の真下から左側へ、真上から右側へ伸びる手を召喚する。ぶつかったプレイヤーに"+ CharaAbiData.Nico(2) +"ダメージを与え、技を発動した際に向いていた方向に吹き飛ばす。",
                                                 "発動した時、自分の両側から中央に迫る無数の手の壁を設置し、自分はステージの中央にテレポートする。その無数の手の壁にぶつかったプレイヤーは中央にテレポートさせられた後、死亡する。この技はマネーを所持している間、使用することができない。" },
                                                {"カミヤの説明",
                                                 "腕と足を回転させた後、その場で爆発する。爆風を食らったプレイヤーに" + CharaAbiData.Kamiya(0) +"ダメージと"+ CharaAbiData.Kamiya(8) +"秒サイレンスを与える。自分にも"+ CharaAbiData.Kamiya(1) +"ダメージ与える。また、腕が回転している間は操作することができない。",
                                                 "自分から一定範囲内にいるプレイヤーに対して、" + CharaAbiData.Kamiya(3) + "%スローを与え、さらに、"+ CharaAbiData.Kamiya(4) +"秒毎に" + CharaAbiData.Kamiya(2) +"ダメージ与える。この技は"+ CharaAbiData.Kamiya(5) +"秒後に消滅する。" ,
                                                 "自分周辺の上からランダムに馬のお面爆弾が5つ落ちてくる。プレイヤーやステージにぶつかった瞬間に爆発する。爆風を食らったプレイヤーは" + CharaAbiData.Kamiya(6) + "ダメージ食らう。この爆弾の爆風は自分も食らう。",
                                                 CharaAbiData.Kamiya(7) + "秒間、馬のダミーを設置して、当たり判定が無くなり、一切ダメージを食らわない状態になる。その間、移動することも技を出すこともできない。"} ,
                                                {"ニンザードの説明",
                                                 "その場からくないを前方になげる。くないは徐々に下に落ちていく。ぶつかったプレイヤーに"+ CharaAbiData.Ninzard(0) +"ダメージを与える。石の壁にぶつけた時だけ、ダメージが"+ CharaAbiData.Ninzard(9) +"になる。",
                                                 "その場から、現在の自分の移動方向に動く分身を置き、自分は透明になる。"+ CharaAbiData.Ninzard(3) +"秒後に透明が解除され、分身が消える。",
                                                 "自分の真下からまきびしを落す。ぶつかったプレイヤーに"+ CharaAbiData.Ninzard(4) +"ダメージを与え、" + CharaAbiData.Ninzard(5) +"%スローを"+ CharaAbiData.Ninzard(7) +"秒与える。このまきびしは" + CharaAbiData.Ninzard(6) +"秒後に消滅する。",
                                                 "その場から飛びながらバックする。"} };
    */
    //現在のキャラの総数
    private static int charaMax = 11;
    //キャラの基礎サイズ
    private static float[] charaSize = { 0, 0.5f, 0.5f, 0.45f,0.7f,0.5f,0.25f,0.6f,0.5f,0.45f,0.7f,0.6f};
    //スタンの配置位置
    private static float[,] stanSize = { { 0, 0, 0, 0 }, { 0.3f, 0, -0.3f, 0 }, { 0.66f, 1.5f, -0.5f, 1.5f },{ 0, 1, 0, 1 },{ 0, 0, 0, 0 }, { 0, -0.5f, 0, -0.5f }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 },
                                          { 0, 0, 0, 0 },{ 0, 0, 0, 0 },{ 0, 0, 0, 0 }  };
    //レア度関連
    //そのキャラのレア度
    private static string[] charaRare = { "初期","スーパーレア","スーパーレア","レジェンドレア","アンコモン","レア","コモン","アンコモン","ウルトラレア","シークレットレア","レア"};
    //各レア度存在数
    private static int[] rareValue = { 0, 1, 2, 2, 2, 1, 1, 1 };
    //レアの所属番号
    private static int[] common = {0,7};
    private static int[] uncommon = {0,5,8};
    private static int[] rare = {0,6,11};
    private static int[] superRare = {0,2,3 };
    private static int[] ultraRare = { 0,9};
    private static int[] legendRare = {0,4 };
    private static int[] secretRare = {0,10};
    //確率のふり幅
    private static long[,] kakuMax = { {0,0,0,0,0,0},{8000,9324,9824,9974,9994,9999 },{3000,6040,9240,9840,9940,9990 },{1000,2050,4550,9050,9850,9950 },{650,1500,2700,5200,9200,9900 } };
    //所持しているか否か
    private static bool[] charaGet = new bool[charaMax + 1];
    //現在のキャラページ数
    private static int page = 1;
    //現在のキャラ最大ページ数
    private static int maxPage = 1;

    //勝ったか負けたか
    private static bool win;
    //試合時間、秒と分
    public static int seconds, minutes;
    //試合が終わったばかりか否か
    public static bool gameEnd;
    //これが最初のプレイか
    public static bool firstGame = true;

    private static int point = 0;
    public static int superMode = 0;
    //現在、サーバーに接続中か否か
    public static bool server = false;
    //現在、ロビーのどのタブを開いているか
    public static int nowSelectLobby = 0;
    //今、なんのガチャをやろうとしているのか
    public static int nowSelectGacha = 4;
    //引いたレア度
    public static string gachaRare = "";
    [System.Serializable]
    public class SaveData
    {
        //これまで勝った試合の回数
        public long winValue;
        //これまで負けた試合の回数
        public long loseValue;
        //今持っているエクスプローラー
        public bool[] nowHave = new bool[charaMax + 1];
        //各キャラの使用回数
        public long[] charaUse = new long[charaMax + 1];
        //一番長かった試合時間
        public int longGame;
        //一番短かった試合時間
        public int shortGame;
        //各キャラの勝利数
        public long[] charaWin = new long[charaMax + 1];
        //各キャラの敗北数
        public long[] charaLose = new long[charaMax + 1];
        //現在持っているポイント数
        public long point;
        //現在持っているチケット数
        public long ticket;
    }

    //セーブ等
    public static string filePath = Application.persistentDataPath + "/" + ".savedata.json";
    public static SaveData save = new SaveData();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void WinUp()
    {
        save.winValue = save.winValue + 1;
    }

    public static void Save()
    {
        Debug.Log("セーブしました。");
        string json = JsonUtility.ToJson(save);

        StreamWriter streamWriter = new StreamWriter(filePath);
        streamWriter.Write(json);
        streamWriter.Flush();
        streamWriter.Close();
    }

    public static void Load()
    {
        if (File.Exists(filePath))
        {
            StreamReader streamReader;
            streamReader = new StreamReader(filePath);
            string data = streamReader.ReadToEnd();
            streamReader.Close();
            save = JsonUtility.FromJson<SaveData>(data);
            Debug.Log(save.winValue + "ロードしました。");
        }
    }

    public static string GetWazaDetail(int id,int wazaID)
    {
        return "";
    }

    public static bool CheckChara(int id)
    {
        return save.nowHave[id];
    }

    public static void SetChara(int id)
    {
        save.nowHave[id] = true;
    }

    public static float GetCharaSize(int id)
    {
        return charaSize[id];
    }

    public static bool GetWin()
    {
        return win;
    }

    public static void IWin()
    {
        win = true;
    }

    public static void ResetWin()
    {
        win = false;
    }
    public static void PlusCharaUse(int id)
    {
        save.charaUse[id] = save.charaUse[id] + 1;
    }

    public static long GetCharaUse(int id)
    {
        return save.charaUse[id];
    }

    public static void PlusCharaWin(int id)
    {
        save.charaWin[id] = save.charaWin[id] + 1;
    }

    public static long GetCharaWin(int id)
    {
        return save.charaWin[id];
    }

    public static void PlusCharaLose(int id)
    {
        save.charaLose[id] = save.charaLose[id] + 1;
    }

    public static long GetCharaLose(int id)
    {
        return save.charaLose[id];
    }

    public static void PlusPoint(int value)
    {
        save.point = save.point + value;
    }

    public static long GetPoint()
    {
        return save.point;
    }

    public static void PlusTicket(int value)
    {
        save.ticket = save.ticket + value;
    }

    public static long GetTicket()
    {
        return save.ticket;
    }

    //ガチャシステム
    public static int GachaChara(long number)
    {
        int charaID = 0;
        if (number <= kakuMax[nowSelectGacha, 0])
        {
            charaID = CharaGacha(1);
            gachaRare = "コモン";
        }
        else if (number <= kakuMax[nowSelectGacha, 1])
        {
            charaID = CharaGacha(2);
            gachaRare = "アンコモン";
        }
        else if (number <= kakuMax[nowSelectGacha, 2])
        {
            charaID = CharaGacha(3);
            gachaRare = "レア";
        }
        else if (number <= kakuMax[nowSelectGacha, 3])
        {
            charaID = CharaGacha(4);
            gachaRare = "スーパーレア";
        }
        else if (number <= kakuMax[nowSelectGacha, 4])
        {
            charaID = CharaGacha(5);
            gachaRare = "ウルトラレア";
        }
        else if (number <= kakuMax[nowSelectGacha, 5])
        {
            charaID = CharaGacha(6);
            gachaRare = "レジェンドレア";
        }
        else
        {
            charaID = CharaGacha(7);
            gachaRare = "シークレットレア";
        }
        return charaID;
    }

    static int CharaGacha(int type)
    {
        int charaID = 0;
        int rnd = Random.Range(1, rareValue[type] + 1);
        switch (type)
        {
            case 1:
                charaID = common[rnd];
                break;
            case 2:
                charaID = uncommon[rnd];
                break;
            case 3:
                charaID = rare[rnd];
                break;
            case 4:
                charaID = superRare[rnd];
                break;
            case 5:
                charaID = ultraRare[rnd];
                break;
            case 6:
                charaID = legendRare[rnd];
                break;
            case 7:
                charaID = secretRare[rnd];
                break;
        }
        return charaID;
    }

    public static float GetStanSize(int id,int number)
    {
        return stanSize[id, number];
    }
}
