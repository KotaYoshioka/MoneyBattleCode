using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class CharaBase : MonoBehaviourPunCallbacks
{
    //共有
    [SerializeField] private int charaID;
    protected Rigidbody2D rigid;
    private SpriteRenderer render;
    private Animator animator;
    
    //非共有
    private StageCamera myCamera;
    private AbilityImage[] abilityImages;
    private FaceImage faceImage;
    private TextMeshProUGUI hpText;
    private GameObject moneyImage;
    private TextMeshProUGUI instructionText;

    //保持
    private BattleManager battleManager;

    //ステータス
    protected int maxhp;
    protected int hp;
    protected float speed;
    protected int jump;
    protected int maxJumpTime;
    protected int jumpTime;
    protected int respawnTime;

    protected int[] cd = {0,0,0,0};

    protected bool money = false;

    protected bool directionRight;

    protected bool is1P;

    //ステータス変動
    protected float speedBonus;
    protected int jumpBonus;
    protected int damageGuard;
    protected int respawnBonus;

    //異常状態
    //無敵
    protected bool invincible = false;
    //スタン
    protected bool stan = false;

    //調整・管理系
    protected bool key = false; //falseの間、操作が出来ない。
    protected bool stuck = false; //trueの間、操作が出来ない。
    protected bool abilityStop = false; //trueの間、技が打てない。

    //Photon関連
    protected PhotonView pv;

    void Start()
    {
        pv = GetComponent<PhotonView>();
        rigid = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        if (pv.IsMine)
        {
            myCamera = GameObject.Find("Maincamera").gameObject.GetComponent<StageCamera>();
            myCamera.SetTarget(gameObject);
            pv.RPC(nameof(Set1P),RpcTarget.All ,PhotonNetwork.IsMasterClient);
        }
        SetStatus();
    }

    /// <summary>
    /// charaIDを基にデータから情報をセットする。
    /// </summary>
    void SetStatus()
    {
        maxhp = CharaData.GetHP(charaID);
        hp = maxhp;
        speed = CharaData.GetSpeed(charaID);
        jump = CharaData.GetJump(charaID);
        maxJumpTime = CharaData.GetJumpTime(charaID);
        jumpTime = maxJumpTime;
        respawnTime = CharaData.GetRespawn(charaID);
        DisplayHP();
    }

    private void FixedUpdate()
    {
        if (pv.IsMine)
        {
            //操作停止
            if (!key)
            {
                return;
            }
            if (stuck)
            {
                return;
            }
            //スタン
            if (stan)
            {
                return;
            }
            //左右移動
            if (Input.GetKey(KeyCode.A))
            {
                MoveRightOrLeft(false);
                if (directionRight)
                {
                    TurnAround();
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                MoveRightOrLeft(true);
                if (!directionRight)
                {
                    TurnAround();
                }
            }
        }
    }

    /// <summary>
    /// 左右移動をする処理
    /// </summary>
    protected void MoveRightOrLeft(bool right)
    {
        //速度の取得
        float newspeed = GetSpeed(right);
        if (newspeed == 0) return;
        //実際に移動する
        rigid.velocity = new Vector2(newspeed,rigid.velocity.y);
        //アニメーション
        animator.SetBool("IsWalking", true);
    }
    /// <summary>
    /// 向きを変える。
    /// </summary>
    public void TurnAround()
    {
        directionRight = !directionRight;
        float x = gameObject.transform.localScale.x;
        float y = gameObject.transform.localScale.y;
        gameObject.transform.localScale = new Vector3(-x, y, 0);
    }

    public void Update()
    {
        if (pv.IsMine)
        {
            //操作停止
            if (!key)
            {
                return;
            }
            if (stuck)
            {
                return;
            }
            if (stan)
            {
                return;
            }


            //ジャンプ
            if (Input.GetKeyDown(KeyCode.W))
            {
                Jump();
            }


            //アニメーションストップ
            if (Input.GetKeyUp(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                animator.SetBool("IsWalking", false);
            }
            else if (Input.GetKeyUp(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                animator.SetBool("IsWalking", false);
            }


            //技止め
            if (abilityStop)
            {
                return;
            }
            //技
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (cd[0] == 0)
                {
                    Ability1();
                }
            }else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (cd[1] == 0)
                {
                    Ability2();
                }
            }else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (cd[2] == 0)
                {
                    Ability3();
                }
            }else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                if (cd[3] == 0)
                {
                    Ability4();
                }
            }
        }
    }
    /// <summary>
    /// ジャンプする処理
    /// </summary>
    protected virtual void Jump()
    {
        if(jumpTime != 0)
        {
            jumpTime--;
            rigid.AddForce(new Vector2(0,jump));
        }
    }

    //技(各キャラで定義)
    public abstract void Ability1();
    public abstract void Ability2();
    public abstract void Ability3();
    public abstract void Ability4();

    /// <summary>
    /// ダメージを受けた時の処理
    /// </summary>
    public void Damage(int damage)
    {
        if (!pv.IsMine)
        {
            return;
        }
        if (invincible)
        {
            return;
        }
        int newdamage = damage - damageGuard;
        if (newdamage <= 0)
        {
            return;
        }
        hp = hp - newdamage;
        if (hp <= 0)
        {
            hp = 0;
            Death();
        }
        DisplayHP();
    }
    /// <summary>
    /// 死亡処理
    /// </summary>
    public void Death()
    {
        //処理
        pv.RPC(nameof(AsyncDeath),RpcTarget.All);
        key = false;

        //復活時間を計算し、遅延を発生させる。
        int respawn = respawnTime + respawnBonus;
        if(respawn < 1)
        {
            respawn = 1;
        }
        faceImage.SetDeathCD(respawn);
        StartCoroutine(nameof(DelayRespawn), respawn);

        //コインを持っていた場合、落とす
        if (money)
        {
            SetMoney(false);
            PhotonNetwork.Instantiate("Money",gameObject.transform.position,Quaternion.identity);
        }
    }
    [PunRPC]
    public void AsyncDeath()
    {
        gameObject.tag = "Deadman";
        gameObject.layer = 9;
        gameObject.GetComponent<SpriteRenderer>().material.color = Color.clear;
    }
    IEnumerator DelayRespawn(int rest)
    {
        yield return new WaitForSeconds(1);
        rest--;
        faceImage.SetDeathCD(rest);
        if (rest == 0)
        {
            key = true;
            hp = maxhp;
            DisplayHP();
            gameObject.transform.position = PhotonNetwork.IsMasterClient ? new Vector3(7.3f, 6.7f) : new Vector3(-8.3f, 6.7f);
            pv.RPC(nameof(AsyncRespawn), RpcTarget.All, PhotonNetwork.IsMasterClient);
        }
        else
        {
            StartCoroutine(nameof(DelayRespawn),rest);
        }
    }
    [PunRPC]
    public void AsyncRespawn(bool P1)
    {
        gameObject.tag = "Player";
        gameObject.layer = P1 ? 10 : 11;
        gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;
    }

    /// <summary>
    /// 最新の体力を表示する。
    /// </summary>
    private void DisplayHP()
    {
        if (hpText != null)
        {
            hpText.text = "HP：" + hp.ToString();
        }
    }

    /// <summary>
    /// 指定の技にクールダウンを発生させる。
    /// </summary>
    public void AddCooldown(int index,int cooldown)
    {
        bool reduce = cd[index] == 0;
        cd[index] = cooldown;
        abilityImages[index].SetCD(cd[index]);
        if(reduce)StartCoroutine(nameof(ReduceCooldown), index);
    }
    IEnumerator ReduceCooldown(int index)
    {
        yield return new WaitForSeconds(1);
        int ncd = cd[index] - 1;
        cd[index] = ncd;
        abilityImages[index].SetCD(cd[index]);
        if (cd[index] > 0)
        {
            StartCoroutine(nameof(ReduceCooldown), index);
        }
    }
    /// <summary>
    /// 地面についたときに、ジャンプ回数を戻す。
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.tag == "Stage")
        {
            jumpTime = maxJumpTime;
        }
    }

    //変化

    /// <summary>
    /// 一時的に移動速度を変更させる
    /// </summary>
    public void TempBonusSpeed(float bonus, float delayForSecs)
    {
        speedBonus += bonus;
        StartCoroutine(TempBonusSpeedReturn(bonus, delayForSecs));
    }
    IEnumerator TempBonusSpeedReturn(float bonus,float delayForSecs)
    {
        yield return new WaitForSeconds(delayForSecs);
        speedBonus -= bonus;
    }
    /// <summary>
    /// 一時的に無敵になる
    /// </summary>
    public void TempInvincible(float delayForSecs)
    {
        invincible = true;
        StartCoroutine(TempInvincibleReturn(delayForSecs));
    }
    IEnumerator TempInvincibleReturn(float delayForSecs)
    {
        yield return new WaitForSeconds(delayForSecs);
        invincible = false;
    }
    /// <summary>
    /// 一時的にダメージ減少を増やす
    /// </summary>
    public void TempDamageGuard(int guard, float delayForSecs)
    {
        damageGuard += guard;
        StartCoroutine(TempDamageGuardReturn(guard, delayForSecs));
    }
    IEnumerator TempDamageGuardReturn(int guard,float delayForSecs)
    {
        yield return new WaitForSeconds(delayForSecs);
        damageGuard -= guard;
    }
    /// <summary>
    /// 同期で一時的にキャラの色を変える。
    /// </summary>
    public void AsyncTempColorChange(Color c, float delayForSecs)
    {
        pv.RPC(nameof(RPCColorChange), RpcTarget.All, c.r,c.g,c.b);
        StartCoroutine(AsyncTempColorChangeReturn(delayForSecs));
    }
    IEnumerator AsyncTempColorChangeReturn(float delayForSecs)
    {
        yield return new WaitForSeconds(delayForSecs);
        pv.RPC(nameof(RPCColorChange), RpcTarget.All, 1f,1f,1f);
    }
    [PunRPC]
    public void RPCColorChange(float r, float g, float b)
    {
        render.material.color = new Color(r,g,b);
    }
    //Getter と Setter
    public void SetKey(bool key)
    {
        this.key = key;
    }
    public int GetCharaID()
    {
        return charaID;
    }

    public void SetUI(BattleManager bm,AbilityImage[] abis, FaceImage fi, TextMeshProUGUI ht,GameObject mig,TextMeshProUGUI inst)
    {
        battleManager = bm;
        abilityImages = abis;
        faceImage = fi;
        hpText = ht;
        moneyImage = mig;
        instructionText = inst;
        DisplayHP();
    }

    public float GetSpeed(bool rightOrDown)
    {
        //ボーナスの加算
        //0になった場合は移動しない
        float newspeed = speed + speedBonus;
        if (newspeed <= 0)
        {
            return 0;
        }
        newspeed *= (rightOrDown ? 1 : -1);
        return newspeed;
    }

    public bool GetMoney()
    {
        return money;
    }
    public void SetMoney(bool money)
    {
        pv.RPC(nameof(AsyncSetMoney),RpcTarget.All ,money);
    }
    [PunRPC]
    public void AsyncSetMoney(bool money)
    {
        this.money = money;
        if (pv.IsMine)
        {
            moneyImage.SetActive(money);
            battleManager.SendSetInstruction(money);
        }
    }

    public bool Is1P()
    {
        return is1P;
    }
    [PunRPC]
    public void Set1P(bool P1)
    {
        is1P = P1;
    }

    public void TempStan(float delayForSecs)
    {
        stan = true;
        StartCoroutine(nameof(StanReturn), delayForSecs);
    }
    IEnumerator StanReturn(float delayForSecs)
    {
        yield return new WaitForSeconds(delayForSecs);
        stan = false;
    }

    public void TempStuck(float delayForSecs)
    {
        if (stuck)
        {
            return;
        }
        stuck = true;
        StartCoroutine(nameof(StuckReturn), delayForSecs);
    }
    IEnumerator StuckReturn(float delayForSecs)
    {
        yield return new WaitForSeconds(delayForSecs);
        stuck = false;
    }

    public void TempNoGravity(float delayForNoGravity)
    {
        pv.RPC(nameof(AsyncNoGravity), RpcTarget.All,true);
        StartCoroutine(nameof(NoGravityReturn),delayForNoGravity);
    }
    IEnumerator NoGravityReturn(float delayForNoGravity)
    {
        yield return new WaitForSeconds(delayForNoGravity);
        pv.RPC(nameof(AsyncNoGravity), RpcTarget.All, false);
    }
    [PunRPC]
    public void AsyncNoGravity(bool nogravity)
    {
        rigid.gravityScale = nogravity ? 0 : 1;
        if (nogravity) rigid.velocity = new Vector2(0, 0);
    }
}
