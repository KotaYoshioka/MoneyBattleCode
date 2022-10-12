using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBase : MonoBehaviour
{
    private int myID;
    private int hp;
    private int maxHp;
    private float speed;
    private int jump;
    private int maxJump;
    private int jumpLimit;
    private int damageGuard;
    private int dead;
    private int maxDead;
    private int motitaCounter = 0;
    private bool stan = false;
    private bool silence = false;
    private bool nowDead = false;
    private bool nowBlack = false;
    private bool nowTree = false;
    private bool hanten = false;
    private float speedBonus;
    private bool muteki = false;
    private int playerNumber = 2;
    private bool mono3Mode = false;
    private AudioSource myAudio;
    public AudioClip sound1, sound2, sound3, sound4, sound5;
    public AudioClip stanSound, slowSound;
    public PhotonView myPV;
    public CircleCollider2D cc;
    private GameObject myCamera;
    private GameObject waza1, waza2, waza3, waza4, waza5;
    private GameObject hpText;
    private GameObject respawn;
    private GameObject money;
    private GameObject stanImage;
    private GameObject treePoint;
    private GameObject nicoA, nicoB;
    private GameObject black;
    private int cd1, cd2, cd3, cd4, cd3b, cd5;
    private int maxCd1, maxCd2, maxCd3, maxCd4;
    private Animator myAnim;
    private bool right = false;
    private bool nowHave = false;
    // Start is called before the first frame update
    void Start()
    {
        GameObject myCanvas = GameObject.Find("Canvas");
        myAudio = GetComponent<AudioSource>();
        maxHp = CharaData.GetHP(myID);
        speed = CharaData.GetSpeed(myID);
        hp = maxHp;
        maxDead = CharaData.GetRespawn(myID);
        dead = maxDead;
        jump = CharaData.GetJump(myID);
        maxJump = CharaData.GetJumpTime(myID);
        jumpLimit = maxJump;
        waza1 = myCanvas.transform.Find("Waza1").gameObject;
        waza2 = myCanvas.transform.Find("Waza2").gameObject;
        waza3 = myCanvas.transform.Find("Waza3").gameObject;
        waza4 = myCanvas.transform.Find("Waza4").gameObject;
        waza5 = myCanvas.transform.Find("Waza5").gameObject;
        respawn = myCanvas.transform.Find("Image").transform.Find("Face").transform.Find("Text").gameObject;
        maxCd1 = CharaData.GetCooldown(myID, 1);
        maxCd2 = CharaData.GetCooldown(myID, 2);
        maxCd3 = CharaData.GetCooldown(myID, 3);
        maxCd4 = CharaData.GetCooldown(myID, 4);
        hpText = myCanvas.transform.Find("Hp").gameObject;
        myCamera = GameObject.Find("Main Camera").gameObject;
        if (myPV.IsMine)
        {
            hpText.GetComponent<Text>().text = "HP：" + hp.ToString();
            money = myCanvas.transform.Find("MoneyImage").gameObject;
            stanImage = myCanvas.transform.Find("Stan").gameObject;
            black = myCanvas.transform.Find("Black").gameObject;
            money.SetActive(false);
            stanImage.SetActive(false);
            black.SetActive(false);
        }
        myPV.RPC("RPC_AddAnimator",RpcTarget.AllBuffered);
        myPV.RPC("DeleteMyGravity", RpcTarget.AllBuffered);
        myPV.RPC("Myname", RpcTarget.AllBuffered, gameObject.name,playerNumber,gameObject.layer);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (myPV.IsMine)
        {
            myCamera.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10);
            if (nowDead == false && stan == false)
            {
                //左右移動
                if (Input.GetKey(KeyCode.A))
                {
                    //移動は実装済み
                    if (mono3Mode == false)
                    {
                        if (hanten == false)
                        {
                            gameObject.transform.localScale = new Vector3(DataBase.GetCharaSize(myID), DataBase.GetCharaSize(myID), 0);
                        }
                        else
                        {
                            gameObject.transform.localScale = new Vector3(DataBase.GetCharaSize(myID), -DataBase.GetCharaSize(myID), 0);
                        }
                        right = false;
                    }

                }
                if (Input.GetKey(KeyCode.D))
                {
                    //移動は実装済み
                    if (mono3Mode == false)
                    {
                        if (hanten == false)
                        {
                            gameObject.transform.localScale = new Vector3(-DataBase.GetCharaSize(myID), DataBase.GetCharaSize(myID), 0);
                        }
                        else
                        {
                            gameObject.transform.localScale = new Vector3(-DataBase.GetCharaSize(myID), -DataBase.GetCharaSize(myID), 0);
                        }
                    }
                }
            }
        }
    }

    void Update()
    {
        if (myPV.IsMine)
        {
            if (nowDead == false && stan == false)
            {
                //ジャンプ(モノの場合、上移動)
                if (Input.GetKeyDown(KeyCode.W))
                {
                    if (mono3Mode == false)
                    {
                        if (jumpLimit != 0)
                        {
                            if (hanten == false)
                            {
                                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jump));
                            }
                            else
                            {
                                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -jump));
                            }
                            jumpLimit = jumpLimit - 1;
                        }
                    }
                    else
                    {
                        if (speed + speedBonus <= 0)
                        {
                            speedBonus = -speed;
                        }
                        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, speed + speedBonus);
                    }
                }
                //モノの場合のみの下移動
                if(Input.GetKeyDown(KeyCode.S) && mono3Mode == true)
                {
                    if (speed + speedBonus <= 0)
                    {
                        speedBonus = -speed;
                    }
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, -speed - speedBonus);
                }
                //歩きアニメーション調整
                if (Input.GetKeyUp(KeyCode.A))
                {
                    if (!(Input.GetKey(KeyCode.D)))
                    {
                        myAnim.SetBool("IsWalking", false);
                    }
                }
                if (Input.GetKeyUp(KeyCode.D))
                {
                    if (!(Input.GetKey(KeyCode.A)))
                    {
                        myAnim.SetBool("IsWalking", false);
                    }
                }
                if (mono3Mode == false && nowBlack == false && silence == false)
                {
                    //1つ目の技
                    if (Input.GetKeyDown(KeyCode.Alpha1) && cd1 == 0)
                    {
                        switch (myID)
                        {
                            //フリート
                            case 3:
                                myPV.RPC("RPC_Freat1", RpcTarget.AllViaServer, gameObject.transform.position.x,gameObject.transform.position.y, gameObject.name);
                                break;
                            //アクム
                            case 4:
                                myPV.RPC("RPC_Akumu1", RpcTarget.AllViaServer, gameObject.transform.position.x);
                                break;
                            //ブランチェ
                            case 5:
                                myPV.RPC("RPC_Branche1", RpcTarget.AllViaServer, gameObject.transform.position.x,gameObject.transform.position.y, gameObject.name);
                                break;
                            //クレガのアイツ
                            case 6:
                                myPV.RPC("RPC_CrazyA1", RpcTarget.AllViaServer, gameObject.transform.position.x, gameObject.transform.position.y, gameObject.name);
                                break;
                            //ダルマン
                            case 7:
                                myPV.RPC("RPC_DarumanStart", RpcTarget.AllViaServer);
                                myAnim.SetTrigger("Waza1");
                                if (right == true)
                                {
                                    //*gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(CharaAbiData.Daruman(0), 0));
                                }
                                else
                                {
                                    //*gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(- CharaAbiData.Daruman(0), 0));
                                }
                                StartCoroutine("Daruman1");
                                SetStan(1.5f);
                                break;
                            //驚く望田
                            case 8:
                                myPV.RPC("RPC_Odomoti1", RpcTarget.AllViaServer, right,gameObject.transform.position.x, gameObject.transform.position.y, gameObject.name);
                                break;
                            //ニコ
                            case 9:
                                myPV.RPC("RPC_Nico1", RpcTarget.AllViaServer);
                                if (hanten == false)
                                {
                                    gameObject.GetComponent<Rigidbody2D>().gravityScale = -1;
                                    gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, -DataBase.GetCharaSize(myID), 0);
                                    hanten = true;
                                }
                                else
                                {
                                    gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
                                    gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, DataBase.GetCharaSize(myID), 0);
                                    hanten = false;
                                }
                                break;
                            //カミヤ
                            case 10:
                                myAnim.SetTrigger("Waza1");
                                myPV.RPC("RPC_Kamiya1_0", RpcTarget.AllViaServer);
                                SetStan(1);
                                StartCoroutine(Kamiya1_1());
                                break;
                            //ニンザード
                            case 11:
                                myPV.RPC("RPC_Ninzard1", RpcTarget.AllViaServer, right, gameObject.transform.position.x, gameObject.transform.position.y, gameObject.name);
                                break;
                        }
                        cd1 = maxCd1;
                        StartCoroutine("CD1");
                    }

                    //2つ目の技
                    if (Input.GetKeyDown(KeyCode.Alpha2) && cd2 == 0)
                    {
                        switch (myID)
                        {
                            //フリート
                            case 3:
                                myPV.RPC("RPC_Freat2", RpcTarget.AllViaServer, right,gameObject.transform.position.x, gameObject.transform.position.y,playerNumber);
                                break;
                            //アクム
                            case 4:
                                myPV.RPC("RPC_Akumu2", RpcTarget.AllViaServer, right,gameObject.transform.position.x,gameObject.transform.position.y, gameObject.name);
                                break;
                            //ブランチェ
                            case 5:
                                myPV.RPC("RPC_Branche2", RpcTarget.AllViaServer, right,gameObject.transform.position.x, gameObject.transform.position.y, gameObject.name);
                                break;
                            //クレガのアイツ
                            case 6:
                                myPV.RPC("RPC_CrazyA2", RpcTarget.AllViaServer, gameObject.transform.position.x, gameObject.transform.position.y, gameObject.name);
                                break;
                            //ダルマン
                            case 7:
                                myPV.RPC("RPC_DarumanStart", RpcTarget.AllViaServer);
                                myAnim.SetTrigger("Waza2");
                                //*gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, CharaAbiData.Daruman(2)));
                                StartCoroutine("Daruman2");
                                SetStan(1f);
                                break;
                            //ニコ
                            case 9:
                                myPV.RPC("RPC_Nico2", RpcTarget.AllViaServer,gameObject.transform.position.x, gameObject.name);
                                break;
                            //カミヤ
                            case 10:
                                myPV.RPC("RPC_Kamiya2", RpcTarget.AllViaServer, gameObject.name);
                                break;
                            //ニンザード
                            case 11:
                                myPV.RPC("RPC_Ninzard2", RpcTarget.AllViaServer, right, gameObject.transform.position.x, gameObject.transform.position.y, gameObject.layer,GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y);
                                StartCoroutine(Ninzard2_2());
                                break;
                        }
                        if (myID != 8)
                        {
                            cd2 = maxCd2;
                            StartCoroutine("CD2");
                        }
                    }

                    //3つ目の技
                    if (Input.GetKeyDown(KeyCode.Alpha3) && cd3 == 0)
                    {
                        switch (myID)
                        {
                            //フリート
                            case 3:
                                myPV.RPC("RPC_Freat3", RpcTarget.AllViaServer, right, gameObject.transform.position.x, gameObject.transform.position.y, gameObject.name);
                                break;
                            //アクム
                            case 4:
                                myPV.RPC("RPC_Akumu4", RpcTarget.AllViaServer, right, gameObject.transform.position.x, gameObject.transform.position.y, gameObject.name);
                                break;
                            //ブランチェ
                            case 5:
                                if (nowTree == false)
                                {
                                    treePoint = null;
                                    if (right == true)
                                    {
                                        treePoint = (GameObject)Instantiate(Resources.Load("Character/Branche/GreenPoint"), new Vector3(gameObject.transform.position.x + 2, gameObject.transform.position.y), Quaternion.identity);
                                        //*treePoint.GetComponent<Rigidbody2D>().AddForce(new Vector2(CharaAbiData.Branche(5), 0));
                                    }
                                    else
                                    {
                                        treePoint = (GameObject)Instantiate(Resources.Load("Character/Branche/GreenPoint"), new Vector3(gameObject.transform.position.x - 2, gameObject.transform.position.y), Quaternion.identity);
                                        //*treePoint.GetComponent<Rigidbody2D>().AddForce(new Vector2(-CharaAbiData.Branche(5), 0));
                                    }
                                    treePoint.GetComponent<BrancheGreenPoint>().SetMaster(gameObject);
                                    nowTree = true;
                                }
                                else
                                {
                                    nowTree = false;
                                    gameObject.transform.position = treePoint.transform.position;
                                    Destroy(treePoint);
                                }
                                break;
                            //クレガのアイツ
                            case 6:
                                myPV.RPC("RPC_CrazyA4", RpcTarget.AllViaServer, right, gameObject.transform.position.x, gameObject.transform.position.y, gameObject.name);
                                break;
                            //驚く望田
                            case 8:
                                myPV.RPC("RPC_Odomoti3", RpcTarget.AllViaServer, right, gameObject.transform.position.x, gameObject.transform.position.y, gameObject.name);
                                //*SetStan(CharaAbiData.Odomoti(8));
                                break;
                            //ニコ
                            case 9:
                                myPV.RPC("RPC_Nico3", RpcTarget.AllViaServer, right,gameObject.transform.position.x, gameObject.name);
                                break;
                            //カミヤ
                            case 10:
                                myPV.RPC("RPC_Kamiya3", RpcTarget.AllViaServer, gameObject.transform.position.x, gameObject.name,Random.Range(-20f,20f), Random.Range(-20f, 20f), Random.Range(-20f, 20f), Random.Range(-20f, 20f), Random.Range(-20f, 20f));
                                break;
                            //ニンザード
                            case 11:
                                myPV.RPC("RPC_Ninzard3", RpcTarget.AllViaServer, gameObject.transform.position.x, gameObject.transform.position.y,gameObject.name);
                                break;

                        }
                        if (myID != 7)
                        {
                            cd3 = maxCd3;
                            if (nowTree != true)
                            {
                                StartCoroutine("CD3");
                            }
                            else
                            {
                                cd3b = 1;
                                StartCoroutine("CD3Branche");
                            }
                        }
                    }

                    //4つ目の技
                    if (Input.GetKeyDown(KeyCode.Alpha4) && cd4 == 0)
                    {
                        switch (myID)
                        {
                            //フリート
                            case 3:
                                myPV.RPC("RPC_Freat4", RpcTarget.AllViaServer, gameObject.transform.position.x, gameObject.transform.position.y,Random.Range(-13f,-4f),Random.Range(4f,13f));
                                break;
                            //ブランチェ
                            case 5:
                                myPV.RPC("RPC_Branche4", RpcTarget.AllViaServer, right, gameObject.transform.position.x, gameObject.transform.position.y, gameObject.name);
                                //*Damage((int)CharaAbiData.Branche(8));
                                break;
                            //クレガのアイツ
                            case 6:
                                myPV.RPC("RPC_CrazyA3", RpcTarget.AllViaServer, gameObject.transform.position.x, gameObject.transform.position.y, gameObject.name);
                                dead = dead / 2;
                                Damage(hp);
                                break;
                            //驚く望田
                            case 8:
                                myPV.RPC("RPC_Odomoti4", RpcTarget.AllViaServer, right, gameObject.transform.position.x, gameObject.transform.position.y, gameObject.name);
                                break;
                            //ニコ
                            case 9:
                                if (nowHave == false)
                                {
                                    myPV.RPC("RPC_Nico4", RpcTarget.AllViaServer, gameObject.transform.position.x, gameObject.name);
                                    gameObject.transform.position = new Vector2(-5, -2.5f);
                                }
                                break;
                            //カミヤ
                            case 10:
                                GetComponent<Rigidbody2D>().gravityScale = 0;
                                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                                //*SetStan(CharaAbiData.Kamiya(7));
                                myPV.RPC("RPC_Kamiya4", RpcTarget.AllViaServer, gameObject.transform.position.x,gameObject.transform.position.y, gameObject.name);
                                StartCoroutine(Kamiya4_2());
                                break;
                            //ニンザード
                            case 11:
                                if(right == true)
                                {
                                    //*GetComponent<Rigidbody2D>().AddForce(new Vector2(-CharaAbiData.Ninzard(8),400));
                                }
                                else
                                {
                                    //*GetComponent<Rigidbody2D>().AddForce(new Vector2(CharaAbiData.Ninzard(8), 400));
                                }
                                break;
                        }
                        if (myID != 4 && myID != 7 && myID != 9)
                        {
                            cd4 = maxCd4;
                            StartCoroutine("CD4");
                        }else if(myID == 9 && nowHave == false)
                        {
                            cd4 = maxCd4;
                            StartCoroutine("CD4");
                        }
                    }

                    //5つ目の技
                    if (Input.GetKeyDown(KeyCode.Alpha5) && cd5 == 0)
                    {
                        if(playerNumber == 1)
                        {
                            gameObject.transform.position = new Vector2(-147,7);
                        }
                        else
                        {
                            gameObject.transform.position = new Vector2(145,7);
                        }
                        cd5 = 240;
                        StartCoroutine("CD5");
                    }
                }
            }
            //var velocity = GetComponent<Rigidbody2D>().velocity;
            //GetComponent<PhotonTransformViewClassic>().SetSynchronizedValues(speed: velocity, turnSpeed: 0);
        }

    }

    //ダメージを食らった時の命令
    public void Damage(int a)
    {
        if (nowDead != true && nowBlack != true)
        {
            if (myID == 8 && cd2 == 0)
            {
                cd2 = maxCd2;
                StartCoroutine("CD2");
                if (myPV.IsMine)
                {
                    myPV.RPC("RPC_Odomoti2", RpcTarget.AllViaServer, right, gameObject.transform.position.x, gameObject.transform.position.y, gameObject.name);
                }
            }
            //ダメージボーナス計算
            if (a - damageGuard <= 0)
            {
                a = 0;
            }
            else
            {
                a = a - damageGuard;
            }
            //無敵の処理
            if (muteki == true)
            {
                a = 0;
            }
            //ダルマンの時
            if (myID == 7)
            {
                int darukoro = Random.Range(0, 100);
                int okiagari = Random.Range(0, 100);
                //*if (okiagari <= CharaAbiData.Daruman(4) * 10)
                {
                    a = 0;
                }
                //*if (darukoro <= CharaAbiData.Daruman(3) * 10)
                {
                    myPV.RPC("RPC_Daruman3", RpcTarget.AllViaServer, gameObject.transform.position.x, gameObject.transform.position.y, gameObject.name);
                }
            }
            //ダメージを与える。
            hp = hp - a;
            if (myPV.IsMine)
            {
                myPV.RPC("RPC_Damage", RpcTarget.AllViaServer, a, gameObject.transform.position.x, gameObject.transform.position.y, Random.Range(-100f, 100f), Random.Range(400f, 450f));
                hpText.GetComponent<Text>().text = "HP：" + hp.ToString();
            }
            //体力が0を下回った時の処理
            if (hp <= 0)
            {
                hp = 0;
                if (myPV.IsMine)
                {
                    hpText.GetComponent<Text>().text = "HP： 0";
                }
                Die();
            }
        }
    }

    //回復
    public void Heal(int a)
    {
        if(a + hp >= maxHp)
        {
            a = maxHp - hp;
        }
        hp = hp + a;
        if (myPV.IsMine)
        {
            myPV.RPC("RPC_Heal", RpcTarget.AllViaServer, a, gameObject.transform.position.x, gameObject.transform.position.y, Random.Range(-100f, 100f), Random.Range(400f, 450f));
            hpText.GetComponent<Text>().text = "HP：" + hp.ToString();
        }
    }

    public void Die()
    {
        if(nowHave == true)
        {
            nowHave = false;
            money.SetActive(false);
            PhotonNetwork.Instantiate("Money", new Vector3(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
        }
        nowDead = true;
        if (myPV.IsMine)
        {
            myPV.RPC("RPC_Dead", RpcTarget.AllBuffered);
            if(myID == 4)
            {
                gameObject.GetComponent<SpriteRenderer>().material.color = Color.black;
                nowDead = false;
                nowBlack = true;
            }
        }
        StartCoroutine("Respawn");

    }

    public void SetID(int a)
    {
        myID = a;
    }

    public void Player1Change()
    {
        playerNumber = 1;
    }

    public int GetPlayer()
    {
        return playerNumber;
    }

    public bool GetCoinGet()
    {
        return nowHave;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;
        if(obj.tag == "Stage" && hanten == false)
        {
            jumpLimit = maxJump;
        }else if(obj.tag == "UpStage" && hanten == true)
        {
            jumpLimit = maxJump;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (myID == 7)
        {
            myPV.RPC("DarumanDamaged", RpcTarget.AllViaServer, obj.name, obj.tag, gameObject.name);
        }
    }

    public void IWin()
    {
        DataBase.IWin();
    }

    public void GetCoin()
    {
        money.SetActive(true);
        nowHave = true;
    }

    public void SetStan(float a)
    {
        stan = true;
        stanImage.SetActive(true);
        myPV.RPC("RPC_Stan", RpcTarget.AllViaServer,gameObject.name);
        StartCoroutine("Stan", a);
    }

    public void SetSilenct(float a)
    {
        silence = true;
        StartCoroutine("Silence", a);
    }

    public void SetSpeedBonus(float a,float time)
    {
        float b = speed * (a / 100);
        speedBonus = speedBonus + b;
        if(a <= 0)
        {
            myPV.RPC("RPC_Slow", RpcTarget.AllViaServer, gameObject.name);
        }
        StartCoroutine(SpeedBonus(time,a));
    }

    public void EndSpeedBonus(float a)
    {
        float b = speed * (a / 100);
        speedBonus = speedBonus - b;
        StopCoroutine("SpeedBonus");
    }
    //クールダウン
    IEnumerator CD1()
    {
        waza1.transform.Find("Text").GetComponent<Text>().text = cd1.ToString();
        yield return new WaitForSeconds(1);
        cd1 = cd1 - 1;
        if(cd1 != 0)
        {
            StartCoroutine("CD1");
        }
        else
        {
            waza1.transform.Find("Text").GetComponent<Text>().text = "";
        }
    }
    IEnumerator CD2()
    {
        waza2.transform.Find("Text").GetComponent<Text>().text = cd2.ToString();
        yield return new WaitForSeconds(1);
        cd2 = cd2 - 1;
        if (cd2 != 0)
        {
            StartCoroutine("CD2");
        }
        else
        {
            waza2.transform.Find("Text").GetComponent<Text>().text = "";
        }
    }
    IEnumerator CD3()
    {
        waza3.transform.Find("Text").GetComponent<Text>().text = cd3.ToString();
        yield return new WaitForSeconds(1);
        cd3 = cd3 - 1;
        if (cd3 != 0)
        {
            StartCoroutine("CD3");
        }
        else
        {
            waza3.transform.Find("Text").GetComponent<Text>().text = "";
        }
    }
    IEnumerator CD3Branche()
    {
        waza3.transform.Find("Text").GetComponent<Text>().text = cd3b.ToString();
        yield return new WaitForSeconds(1);
        cd3b = cd3b - 1;
        if (cd3b != 0)
        {
            StartCoroutine("CD3Branche");
        }
        else
        {
            waza3.transform.Find("Text").GetComponent<Text>().text = "";
            cd3 = 0;
        }
    }
    IEnumerator CD4()
    {
        waza4.transform.Find("Text").GetComponent<Text>().text = cd4.ToString();
        yield return new WaitForSeconds(1);
        cd4 = cd4 - 1;
        if (cd4 != 0)
        {
            StartCoroutine("CD4");
        }
        else
        {
            waza4.transform.Find("Text").GetComponent<Text>().text = "";
        }
    }
    IEnumerator CD5()
    {
        waza5.transform.Find("Text").GetComponent<Text>().text = cd5.ToString();
        yield return new WaitForSeconds(1);
        cd5 = cd5 - 1;
        if (cd5 != 0)
        {
            StartCoroutine("CD5");
        }
        else
        {
            waza5.transform.Find("Text").GetComponent<Text>().text = "";
        }
    }
    IEnumerator Respawn()
    {
        if (myPV.IsMine)
        {
            respawn.GetComponent<Text>().text = dead.ToString();
        }
        yield return new WaitForSeconds(1);
        dead = dead - 1;
        if(dead != 0)
        {
            StartCoroutine("Respawn");
        }
        else
        {
            hp = maxHp;
            dead = maxDead;
            if (myPV.IsMine)
            {
                respawn.GetComponent<Text>().text = "";
                hpText.GetComponent<Text>().text = "HP：" + hp.ToString();
            }
            if (myID != 4)
            {
                gameObject.transform.position = new Vector2(0, 0);
            }
            nowDead = false;
            if(myID == 4)
            {
                nowBlack = false;
            }
            myPV.RPC("RPC_Alive", RpcTarget.AllBuffered);
        }
        StopCoroutine("SpeedBonus");
        speedBonus = 0;
    }
    IEnumerator Stan(float a)
    {
        yield return new WaitForSeconds(a);
        stanImage.SetActive(false);
        stan = false;
    }
    IEnumerator Silence(float a)
    {
        yield return new WaitForSeconds(a);
        silence = false;
    }
    IEnumerator SpeedBonus(float a,float b)
    {
        yield return new WaitForSeconds(a);
        EndSpeedBonus(b);
    }
    //アニメーション同期
    [PunRPC]
    void RPC_AddAnimator()
    {
        myAnim = gameObject.GetComponent<Animator>();
        
    }
    [PunRPC]
    void Myname(string a,int b,int c)
    {
        gameObject.name = a;
        gameObject.tag = "Player";
        playerNumber = b;
        gameObject.layer = c;
    }
    [PunRPC]
    void DeleteMyGravity()
    {
        if (!myPV.IsMine)
        {
            Destroy(GetComponent<Rigidbody2D>());
        }
    }
    [PunRPC]
    void RPC_Dead()
    {
        gameObject.tag = "Deadman";
        gameObject.layer = 9;
        gameObject.GetComponent<SpriteRenderer>().material.color = Color.clear;
    }
    [PunRPC]
    void RPC_Alive()
    {
        gameObject.tag = "Player";
        if (playerNumber == 1)
        {
            gameObject.layer = 10;
        }
        else
        {
            gameObject.layer = 11;
        }
        gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;
    }
    [PunRPC]
    void RPC_Damage(int a,float px,float py,float pxx,float pyy)
    {
        GameObject b = (GameObject)Instantiate(Resources.Load("Damage"),new Vector3(px,py),Quaternion.identity);
        b.GetComponent<TextMesh>().text = a.ToString();
        b.GetComponent<Rigidbody2D>().AddForce(new Vector2(pxx, pyy));
        b.GetComponent<MeshRenderer>().sortingOrder = 20;
    }
    [PunRPC]
    void RPC_Heal(int a, float px, float py, float pxx, float pyy)
    {
        GameObject b = (GameObject)Instantiate(Resources.Load("Heal"), new Vector3(px, py), Quaternion.identity);
        b.GetComponent<TextMesh>().text = a.ToString();
        b.GetComponent<Rigidbody2D>().AddForce(new Vector2(pxx, pyy));
        b.GetComponent<MeshRenderer>().sortingOrder = 20;
    }
    [PunRPC]
    void RPC_Stan(string naming)
    {
        GameObject stanEffect = (GameObject)Instantiate(Resources.Load("Effect/StanEffect"));
        myAudio.PlayOneShot(stanSound);
        stanEffect.GetComponent<StanEffect>().SetMaster(GameObject.Find(naming).gameObject,myID,right);
    }
    [PunRPC]
    void RPC_Slow(string naming)
    {
        GameObject slowEffect = (GameObject)Instantiate(Resources.Load("Effect/SlowEffect"));
        myAudio.PlayOneShot(slowSound);
        slowEffect.GetComponent<StanEffect>().SetMaster(GameObject.Find(naming).gameObject, myID,right);
    }

    /////////////////////////////////////////////////////////フリート///////////////////////////////////////////////////////////////////////
    [PunRPC]
    void RPC_Freat1(float px,float py,string naming)
    {
        GameObject a = (GameObject) Instantiate(Resources.Load("Character/Freat/RedFreat"), new Vector3(px, py), Quaternion.identity);
        myAudio.PlayOneShot(sound1);
        a.GetComponent<FreatRedFreat>().SetMaster(GameObject.Find(naming).gameObject);
    }
    [PunRPC]
    void RPC_Freat2(bool rightn,float px,float py,int number)
    {
        GameObject a = (GameObject)Instantiate(Resources.Load("Character/Freat/BigFreat"),new Vector3(px,py),Quaternion.identity);
        myAudio.PlayOneShot(sound1);
        if (rightn == true)
        {
            a.transform.localScale = new Vector3(-1, 1, 0);
        }
        if(number == 1)
        {
            a.layer = 11;
        }
        else
        {
            a.layer = 10;
        }
    }
    [PunRPC]
    void RPC_Freat3(bool rightn, float px, float py,string naming)
    {
        GameObject a = (GameObject)Instantiate(Resources.Load("Character/Freat/ResFreat"), new Vector3(px, py + 0.3f), Quaternion.identity);
        myAudio.PlayOneShot(sound1);
        a.GetComponent<FreatRes>().SetMaster(GameObject.Find(naming).gameObject);
    }
    [PunRPC]
    void RPC_Freat4( float px, float py,float a,float b)
    {
        Instantiate(Resources.Load("Character/Freat/FFreat"), new Vector3(px + b, py), Quaternion.identity);
        myAudio.PlayOneShot(sound1);
        Instantiate(Resources.Load("Character/Freat/FFreat"), new Vector3(px + a, py), Quaternion.identity);
    }
    /////////////////////////////////////////////////////////アクム///////////////////////////////////////////////////////////////////////
    [PunRPC]
    void RPC_Akumu1(float px)
    {
        Instantiate(Resources.Load("Character/Akumu/AkumuPower"), new Vector3(px - 4, 4), Quaternion.identity);
        myAudio.PlayOneShot(sound1);
        GameObject a = (GameObject)Instantiate(Resources.Load("Character/Akumu/AkumuPower"), new Vector3(px + 19, 4), Quaternion.identity);
        a.GetComponent<AkumuPower>().SetRight();
    }
    [PunRPC]
    void RPC_Akumu2(bool rightn,float px,float py,string naming)
    {
        myAudio.PlayOneShot(sound2);
        GameObject a = (GameObject)Instantiate(Resources.Load("Character/Akumu/AkumuKage"), new Vector3(px, py), Quaternion.identity);
        if (rightn == true)
        {
            a.transform.localScale = new Vector3(-0.7f, 0.7f, 0);
            a.GetComponent<AkumuKage>().Right();
        }
        a.GetComponent<AkumuKage>().SetMaster(GameObject.Find(naming).gameObject);
    }
    [PunRPC]
    void RPC_Akumu4(bool rightn, float px, float py, string naming)
    {
        myAudio.PlayOneShot(sound3);
        GameObject a = (GameObject)Instantiate(Resources.Load("Character/Akumu/Trauma"), new Vector3(px, py + 3), Quaternion.identity);
        if (rightn == true)
        {
            a.transform.localScale = new Vector3(-1.1f, 1.1f, 0);
            a.GetComponent<TraumaDash>().Right();
        }
        a.GetComponent<TraumaDash>().SetMaster(GameObject.Find(naming).gameObject);
    }
    /////////////////////////////////////////////////////////ブランチェ///////////////////////////////////////////////////////////////////////
    [PunRPC]
    void RPC_Branche1(float px,float py,string naming)
    {
        GameObject a = (GameObject)Instantiate(Resources.Load("Character/Branche/Tree"), new Vector3(px, py), Quaternion.identity);
        myAudio.PlayOneShot(sound1);
        a.GetComponent<BrancheTree>().SetMaster(GameObject.Find(naming).gameObject);
    }
    [PunRPC]
    void RPC_Branche2(bool rightn,float px, float py, string naming)
    {
        GameObject a = null;
        myAudio.PlayOneShot(sound2);
        if (rightn == true)
        {
            a = (GameObject)Instantiate(Resources.Load("Character/Branche/Leaf"), new Vector3(px + 4, py + 1.5f), Quaternion.identity);
        }
        else
        {
            a = (GameObject)Instantiate(Resources.Load("Character/Branche/Leaf"), new Vector3(px - 4, py + 1.5f), Quaternion.identity);
        }

        a.GetComponent<BrancheLeaf>().SetMaster(GameObject.Find(naming).gameObject);
    }
    public void Branche3()
    {
        nowTree = false;
    }
    [PunRPC]
    void RPC_Branche4(bool rightn, float px, float py, string naming)
    {
        GameObject a = null;
        myAudio.PlayOneShot(sound4);
        if (rightn == true)
        {
            a = (GameObject)Instantiate(Resources.Load("Character/Branche/Eda"), new Vector3(px + 1, py ), Quaternion.identity);
            //*a.GetComponent<Rigidbody2D>().AddForce(new Vector2(CharaAbiData.Branche(7), 0));
        }
        else
        {
            a = (GameObject)Instantiate(Resources.Load("Character/Branche/Eda"), new Vector3(px - 1, py), Quaternion.identity);
            //*a.GetComponent<Rigidbody2D>().AddForce(new Vector2(- CharaAbiData.Branche(7), 0));
        }

        a.GetComponent<BrancheEda>().SetMaster(GameObject.Find(naming).gameObject);
    }
    /////////////////////////////////////////////////////////クレガのアイツ///////////////////////////////////////////////////////////////////////
    [PunRPC]
    void RPC_CrazyA1(float px, float py, string naming)
    {
        GameObject a = (GameObject)Instantiate(Resources.Load("Character/CrazyA/Koma"), new Vector3(px, py), Quaternion.identity);
        myAudio.PlayOneShot(sound1);
        a.GetComponent<CrazyAKoma>().SetMaster(GameObject.Find(naming).gameObject);
    }
    [PunRPC]
    void RPC_CrazyA2(float px, float py, string naming)
    {
        GameObject a = (GameObject)Instantiate(Resources.Load("Character/CrazyA/huti"), new Vector3(px, py), Quaternion.identity);
        myAudio.PlayOneShot(sound2);
        a.GetComponent<CrazyABukiya>().SetMaster(GameObject.Find(naming).gameObject);
    }
    [PunRPC]
    void RPC_CrazyA3(float px, float py, string naming)
    {
        GameObject a = (GameObject)Instantiate(Resources.Load("Character/CrazyA/Iei"), new Vector3(px, py), Quaternion.identity);
        myAudio.PlayOneShot(sound3);
        a.GetComponent<CrazyIei>().SetMaster(GameObject.Find(naming).gameObject);
    }
    [PunRPC]
    void RPC_CrazyA4(bool rightn, float px, float py, string naming)
    {
        GameObject a = null;
        myAudio.PlayOneShot(sound4);
        if (rightn == true)
        {
            a = (GameObject)Instantiate(Resources.Load("Character/CrazyA/Ponkan"), new Vector3(px, py), Quaternion.identity);
            a.GetComponent<Rigidbody2D>().AddForce(new Vector2(300, 400));
        }
        else
        {
            a = (GameObject)Instantiate(Resources.Load("Character/CrazyA/Ponkan"), new Vector3(px, py), Quaternion.identity);
            a.GetComponent<Rigidbody2D>().AddForce(new Vector2(-300, 400));
        }

        a.GetComponent<CrazyAPonkan>().SetMaster(GameObject.Find(naming).gameObject);
    }
    /////////////////////////////////////////////////////////ダルマン///////////////////////////////////////////////////////////////////////
    IEnumerator Daruman1()
    {
        yield return new WaitForSeconds(1.5f);
        myPV.RPC("RPC_DarumanEnd", RpcTarget.AllViaServer);
    }
    IEnumerator Daruman2()
    {
        yield return new WaitForSeconds(1f);
        myPV.RPC("RPC_DarumanEnd", RpcTarget.AllViaServer);
    }
    [PunRPC]
    void DarumanDamaged(string naming,string tagging,string masterName)
    {
        GameObject obj = GameObject.Find(naming).gameObject;
        
        if (tagging == "Player" && naming != masterName)
        {
            //*obj.GetComponent<PlayerBase>().Damage((int)CharaAbiData.Daruman(1));
        }
        else if(tagging == "StoneWall")
        {
            //*obj.GetComponent<StoneWallScript>().Damage((int)CharaAbiData.Daruman(1),masterName);
        }
    }
    [PunRPC]
    void RPC_DarumanStart()
    {
        myAudio.PlayOneShot(sound1);
        gameObject.layer = 8;
        cc.enabled = true;
    }
    [PunRPC]
    void RPC_DarumanEnd()
    {
        cc.enabled = false;
        if(playerNumber == 1)
        {
            gameObject.layer = 10;
        }
        else
        {
            gameObject.layer = 11;
        }
    }
    [PunRPC]
    void RPC_Daruman3(float px, float py, string naming)
    {
        GameObject a = (GameObject)Instantiate(Resources.Load("Character/Daruman/StanField"), new Vector3(px, py), Quaternion.identity);
        a.GetComponent<DarumanStanField>().SetMaster(GameObject.Find(naming).gameObject);
    }
    /////////////////////////////////////////////////////////驚く望田///////////////////////////////////////////////////////////////////////
    [PunRPC]
    void RPC_Odomoti1(bool rightn, float px, float py, string naming)
    {
        GameObject a = null;
        myAudio.PlayOneShot(sound1);
        if (rightn == true)
        {
            a = (GameObject)Instantiate(Resources.Load("Character/OdorokuMotita/So"), new Vector3(px + 4, py), Quaternion.identity);
        }
        else
        {
            a = (GameObject)Instantiate(Resources.Load("Character/OdorokuMotita/So"), new Vector3(px - 4, py), Quaternion.identity);
        }

        a.GetComponent<OdomotiSorehanaidaro>().SetMaster(GameObject.Find(naming).gameObject);
    }
    [PunRPC]
    void RPC_Odomoti2(bool rightn, float px, float py, string naming)
    {
        GameObject a = null;
        myAudio.PlayOneShot(sound1);
        if (rightn == true)
        {
            a = (GameObject)Instantiate(Resources.Load("Character/OdorokuMotita/I"), new Vector3(px + 4, py), Quaternion.identity);
        }
        else
        {
            a = (GameObject)Instantiate(Resources.Load("Character/OdorokuMotita/I"), new Vector3(px - 4, py), Quaternion.identity);
        }

        a.GetComponent<OdomotiIteenaoi>().SetMaster(GameObject.Find(naming).gameObject);
    }
    public void Odomoti2Get(bool a)
    {
        if(a == false)
        {
            StopCoroutine(CD2());
            waza2.transform.Find("Text").GetComponent<Text>().text = "";
            cd2 = 0;
        }
    }
    [PunRPC]
    void RPC_Odomoti3(bool rightn, float px, float py, string naming)
    {
        GameObject a = null;
        myAudio.PlayOneShot(sound1);
        if (rightn == true)
        {
            a = (GameObject)Instantiate(Resources.Load("Character/OdorokuMotita/Da"), new Vector3(px + 4, py), Quaternion.identity);
        }
        else
        {
            a = (GameObject)Instantiate(Resources.Load("Character/OdorokuMotita/Da"), new Vector3(px - 4, py), Quaternion.identity);
        }

        a.GetComponent<OdomotiDamarete>().SetMaster(GameObject.Find(naming).gameObject);
    }
    [PunRPC]
    void RPC_Odomoti4(bool rightn, float px, float py, string naming)
    {
        GameObject a = null;
        myAudio.PlayOneShot(sound1);
        if (rightn == true)
        {
            a = (GameObject)Instantiate(Resources.Load("Character/OdorokuMotita/Ze"), new Vector3(px + 4, py), Quaternion.identity);
        }
        else
        {
            a = (GameObject)Instantiate(Resources.Load("Character/OdorokuMotita/Ze"), new Vector3(px - 4, py), Quaternion.identity);
        }

        a.GetComponent<OdomotiKowarenena>().SetMaster(GameObject.Find(naming).gameObject);
    }
    public void MotitaCounter()
    {
        motitaCounter = motitaCounter + 1;
        //*if(motitaCounter == CharaAbiData.Odomoti(9))
        {
            Damage(9999);
        }
    }
    /////////////////////////////////////////////////////////ニコ///////////////////////////////////////////////////////////////////////
    [PunRPC]
    void RPC_Nico1()
    {
        myAudio.PlayOneShot(sound1);
    }
    [PunRPC]
    void RPC_Nico2(float px, string naming)
    {
        myAudio.PlayOneShot(sound2);
        GameObject a = (GameObject)Instantiate(Resources.Load("Character/Nico/UpHands"), new Vector3(px, 15f), Quaternion.identity);
        GameObject b = (GameObject)Instantiate(Resources.Load("Character/Nico/DownHands"), new Vector3(px, -8.5f), Quaternion.identity);
        //myAudio.PlayOneShot(sound1);
        a.GetComponent<UpDownHands>().SetMaster(GameObject.Find(naming).gameObject);
        b.GetComponent<UpDownHands>().SetMaster(GameObject.Find(naming).gameObject);
    }
    [PunRPC]
    void RPC_Nico3(bool rightn,float px,string naming)
    {
        myAudio.PlayOneShot(sound3);
        GameObject a = (GameObject)Instantiate(Resources.Load("Character/Nico/Hand"), new Vector3(px, -4), Quaternion.identity);
        RPC_Nico3G(rightn,px,naming);
        if(rightn == true)
        {
            a.GetComponent<NicoHands>().SetRight();
        }
        a.GetComponent<NicoHands>().SetMaster(GameObject.Find(naming).gameObject);
        StartCoroutine(Nico3_2(rightn,px,naming));
    }
    IEnumerator Nico3_2(bool rightn,float px,string naming)
    {
        yield return new WaitForSeconds(1.0f);
        GameObject a = (GameObject)Instantiate(Resources.Load("Character/Nico/Hand"), new Vector3(px -0.92f, -0.75f), Quaternion.Euler(0,0,28));
        a.GetComponent<SpriteRenderer>().sortingOrder = a.GetComponent<SpriteRenderer>().sortingOrder - 1;
        if (rightn == true)
        {
            a.GetComponent<NicoHands>().SetRight();
        }
        a.GetComponent<NicoHands>().SetMaster(GameObject.Find(naming).gameObject);
        StartCoroutine(Nico3_3(rightn, px, naming));
    }
    IEnumerator Nico3_3(bool rightn, float px, string naming)
    {
        yield return new WaitForSeconds(1.0f);
        GameObject a = (GameObject)Instantiate(Resources.Load("Character/Nico/Hand"), new Vector3(px - 2.88f, 1.63f), Quaternion.Euler(0, 0, 47.4f));
        a.GetComponent<SpriteRenderer>().sortingOrder = a.GetComponent<SpriteRenderer>().sortingOrder - 2;
        if (rightn == true)
        {
            a.GetComponent<NicoHands>().SetRight();
        }
        a.GetComponent<NicoHands>().SetMaster(GameObject.Find(naming).gameObject);
        StartCoroutine(Nico3_4(rightn, px, naming));
    }
    IEnumerator Nico3_4(bool rightn, float px, string naming)
    {
        yield return new WaitForSeconds(1.0f);
        GameObject a = (GameObject)Instantiate(Resources.Load("Character/Nico/Hand"), new Vector3(px - 5.43f, 2.44f), Quaternion.Euler(0, 0, 82));
        a.GetComponent<SpriteRenderer>().sortingOrder = a.GetComponent<SpriteRenderer>().sortingOrder - 3;
        if (rightn == true)
        {
            a.GetComponent<NicoHands>().SetRight();
        }
        a.GetComponent<NicoHands>().SetMaster(GameObject.Find(naming).gameObject);
        StartCoroutine(Nico3_5(rightn, px, naming));
    }
    IEnumerator Nico3_5(bool rightn, float px, string naming)
    {
        yield return new WaitForSeconds(1.0f);
        GameObject a = (GameObject)Instantiate(Resources.Load("Character/Nico/Hand"), new Vector3(px - 7.88f, 3.97f), Quaternion.Euler(0, 0, 47.4f));
        a.GetComponent<SpriteRenderer>().sortingOrder = a.GetComponent<SpriteRenderer>().sortingOrder - 4;
        if (rightn == true)
        {
            a.GetComponent<NicoHands>().SetRight();
        }
        a.GetComponent<NicoHands>().SetMaster(GameObject.Find(naming).gameObject);
        StartCoroutine(Nico3_6(rightn, px, naming));
    }
    IEnumerator Nico3_6(bool rightn, float px, string naming)
    {
        yield return new WaitForSeconds(1.0f);
        GameObject a = (GameObject)Instantiate(Resources.Load("Character/Nico/Hand"), new Vector3(px - 9.75f, 6.48f), Quaternion.Euler(0, 0, 28));
        a.GetComponent<SpriteRenderer>().sortingOrder = a.GetComponent<SpriteRenderer>().sortingOrder - 5;
        if (rightn == true)
        {
            a.GetComponent<NicoHands>().SetRight();
        }
        a.GetComponent<NicoHands>().SetMaster(GameObject.Find(naming).gameObject);
        StartCoroutine(Nico3_7(rightn, px, naming));
    }
    IEnumerator Nico3_7(bool rightn, float px, string naming)
    {
        yield return new WaitForSeconds(1.0f);
        GameObject a = (GameObject)Instantiate(Resources.Load("Character/Nico/Hand"), new Vector3(px -10.27f, 9.96f), Quaternion.Euler(0, 0, 0));
        a.GetComponent<SpriteRenderer>().sortingOrder = a.GetComponent<SpriteRenderer>().sortingOrder - 6;
        if (rightn == true)
        {
            a.GetComponent<NicoHands>().SetRight();
        }
        a.GetComponent<NicoHands>().SetMaster(GameObject.Find(naming).gameObject);
    }
    void RPC_Nico3G(bool rightn, float px, string naming)
    {
        GameObject a = (GameObject)Instantiate(Resources.Load("Character/Nico/GHand"), new Vector3(px, 10), Quaternion.identity);
        if (rightn == true)
        {
            a.GetComponent<NicoHands>().SetRight();
        }
        a.GetComponent<NicoHands>().SetMaster(GameObject.Find(naming).gameObject);
        StartCoroutine(Nico3_2G(rightn, px, naming));
    }
    IEnumerator Nico3_2G(bool rightn, float px, string naming)
    {
        yield return new WaitForSeconds(1.0f);
        GameObject a = (GameObject)Instantiate(Resources.Load("Character/Nico/GHand"), new Vector3(px + 0.98f, 7.35f), Quaternion.Euler(0, 0, 28));
        a.GetComponent<SpriteRenderer>().sortingOrder = a.GetComponent<SpriteRenderer>().sortingOrder - 1;
        if (rightn == true)
        {
            a.GetComponent<NicoHands>().SetRight();
        }
        a.GetComponent<NicoHands>().SetMaster(GameObject.Find(naming).gameObject);
        StartCoroutine(Nico3_3G(rightn, px, naming));
    }
    IEnumerator Nico3_3G(bool rightn, float px, string naming)
    {
        yield return new WaitForSeconds(1.0f);
        GameObject a = (GameObject)Instantiate(Resources.Load("Character/Nico/GHand"), new Vector3(px +3.13f, 4.94f), Quaternion.Euler(0, 0, 47.4f));
        a.GetComponent<SpriteRenderer>().sortingOrder = a.GetComponent<SpriteRenderer>().sortingOrder - 2;
        if (rightn == true)
        {
            a.GetComponent<NicoHands>().SetRight();
        }
        a.GetComponent<NicoHands>().SetMaster(GameObject.Find(naming).gameObject);
        StartCoroutine(Nico3_4G(rightn, px, naming));
    }
    IEnumerator Nico3_4G(bool rightn, float px, string naming)
    {
        yield return new WaitForSeconds(1.0f);
        GameObject a = (GameObject)Instantiate(Resources.Load("Character/Nico/GHand"), new Vector3(px +6.02f, 3.97f), Quaternion.Euler(0, 0, 82));
        a.GetComponent<SpriteRenderer>().sortingOrder = a.GetComponent<SpriteRenderer>().sortingOrder - 3;
        if (rightn == true)
        {
            a.GetComponent<NicoHands>().SetRight();
        }
        a.GetComponent<NicoHands>().SetMaster(GameObject.Find(naming).gameObject);
        StartCoroutine(Nico3_5G(rightn, px, naming));
    }
    IEnumerator Nico3_5G(bool rightn, float px, string naming)
    {
        yield return new WaitForSeconds(1.0f);
        GameObject a = (GameObject)Instantiate(Resources.Load("Character/Nico/GHand"), new Vector3(px + 8.69f,2.45f), Quaternion.Euler(0, 0, 47.4f));
        a.GetComponent<SpriteRenderer>().sortingOrder = a.GetComponent<SpriteRenderer>().sortingOrder - 4;
        if (rightn == true)
        {
            a.GetComponent<NicoHands>().SetRight();
        }
        a.GetComponent<NicoHands>().SetMaster(GameObject.Find(naming).gameObject);
        StartCoroutine(Nico3_6G(rightn, px, naming));
    }
    IEnumerator Nico3_6G(bool rightn, float px, string naming)
    {
        yield return new WaitForSeconds(1.0f);
        GameObject a = (GameObject)Instantiate(Resources.Load("Character/Nico/GHand"), new Vector3(px +10.69f, -0.36f), Quaternion.Euler(0, 0, 28));
        a.GetComponent<SpriteRenderer>().sortingOrder = a.GetComponent<SpriteRenderer>().sortingOrder - 5;
        if (rightn == true)
        {
            a.GetComponent<NicoHands>().SetRight();
        }
        a.GetComponent<NicoHands>().SetMaster(GameObject.Find(naming).gameObject);
        StartCoroutine(Nico3_7G(rightn, px, naming));
    }
    IEnumerator Nico3_7G(bool rightn, float px, string naming)
    {
        yield return new WaitForSeconds(1.0f);
        GameObject a = (GameObject)Instantiate(Resources.Load("Character/Nico/GHand"), new Vector3(px + 11.43f, -3.99f), Quaternion.Euler(0, 0, 0));
        a.GetComponent<SpriteRenderer>().sortingOrder = a.GetComponent<SpriteRenderer>().sortingOrder - 6;
        if (rightn == true)
        {
            a.GetComponent<NicoHands>().SetRight();
        }
        a.GetComponent<NicoHands>().SetMaster(GameObject.Find(naming).gameObject);
    }
    [PunRPC]
    void RPC_Nico4(float px, string naming)
    {
        myAudio.PlayOneShot(sound5);
        nicoA = (GameObject)Instantiate(Resources.Load("Character/Nico/1"), new Vector3(px - 40, 0), Quaternion.Euler(0,0,-90));
        nicoA.name = "1";
        nicoB = (GameObject)Instantiate(Resources.Load("Character/Nico/3"), new Vector3(px + 40, 0), Quaternion.Euler(0,0,90));
        nicoB.name = "3";
        nicoA.GetComponent<NicoHandser>().SetMaster(GameObject.Find(naming).gameObject);
        nicoB.GetComponent<NicoHandser>().SetMaster(GameObject.Find(naming).gameObject);
    }
    [PunRPC]
    void RPC_Nico4_3(string a,string b)
    {
        myAudio.PlayOneShot(sound4);
        Destroy(GameObject.Find(a).gameObject);
        Destroy(GameObject.Find(b).gameObject);
        if (myPV.IsMine)
        {
            black.SetActive(true);
            StartCoroutine("ReClearR");
        }
    }
    public void Nico4_2()
    {
        myPV.RPC("RPC_Nico4_3", RpcTarget.AllViaServer, nicoA.name, nicoB.name);
    }
    public void NicoBlack()
    {
        black.SetActive(true);
        StartCoroutine("ReClear");
    }
    IEnumerator ReClear()
    {
        yield return new WaitForSeconds(1.3f);
        Damage(9999);
        black.SetActive(false);
    }
    IEnumerator ReClearR()
    {
        yield return new WaitForSeconds(1.3f);
        black.SetActive(false);
    }
    /////////////////////////////////////////////////////////カミヤ///////////////////////////////////////////////////////////////////////
    [PunRPC]
    void RPC_Kamiya1_0()
    {
        myAudio.PlayOneShot(sound1);
    }
    IEnumerator Kamiya1_1()
    {
        yield return new WaitForSeconds(1);
        //*Damage((int)CharaAbiData.Kamiya(1));
        myAudio.PlayOneShot(sound2);
        myPV.RPC("RPC_Kamiya1_2", RpcTarget.AllViaServer,gameObject.transform.position.x,gameObject.transform.position.y,gameObject.name);
    }
    [PunRPC]
    void RPC_Kamiya1_2(float px, float py, string naming)
    {
        GameObject a = (GameObject)Instantiate(Resources.Load("Character/Kamiya/GuruBaku"), new Vector3(px, py), Quaternion.identity);
        a.GetComponent<KamiyaGuruguru>().SetMaster(GameObject.Find(naming).gameObject);
    }
    [PunRPC]
    void RPC_Kamiya2(string naming)
    {
        GameObject a = (GameObject)Instantiate(Resources.Load("Character/Kamiya/Range"), new Vector3(0, 0), Quaternion.identity);
        myAudio.PlayOneShot(sound3);
        a.GetComponent<KamiyaRange>().SetMaster(GameObject.Find(naming).gameObject);
    }
    [PunRPC]
    void RPC_Kamiya3(float px,string naming,float r1,float r2,float r3,float r4,float r5)
    {
        GameObject a = (GameObject)Instantiate(Resources.Load("Character/Kamiya/UmaHead"), new Vector3(px + r1, 8), Quaternion.identity);
        GameObject b = (GameObject)Instantiate(Resources.Load("Character/Kamiya/UmaHead"), new Vector3(px + r2, 8), Quaternion.identity);
        GameObject c = (GameObject)Instantiate(Resources.Load("Character/Kamiya/UmaHead"), new Vector3(px + r3, 8), Quaternion.identity);
        GameObject d = (GameObject)Instantiate(Resources.Load("Character/Kamiya/UmaHead"), new Vector3(px + r4, 8), Quaternion.identity);
        GameObject e = (GameObject)Instantiate(Resources.Load("Character/Kamiya/UmaHead"), new Vector3(px + r5, 8), Quaternion.identity);
        a.GetComponent<KamiyaBatou>().SetMaster(GameObject.Find(naming).gameObject);
        b.GetComponent<KamiyaBatou>().SetMaster(GameObject.Find(naming).gameObject);
        c.GetComponent<KamiyaBatou>().SetMaster(GameObject.Find(naming).gameObject);
        d.GetComponent<KamiyaBatou>().SetMaster(GameObject.Find(naming).gameObject);
        e.GetComponent<KamiyaBatou>().SetMaster(GameObject.Find(naming).gameObject);
    }
    [PunRPC]
    void RPC_Kamiya4(float px,float py,string naming)
    {
        GameObject master = GameObject.Find(naming).gameObject;
        myAudio.PlayOneShot(sound4);
        master.GetComponent<SpriteRenderer>().color = Color.clear;
        master.tag = "Deadman";
        master.layer = 9;
        GameObject a = (GameObject)Instantiate(Resources.Load("Character/Kamiya/UmaDummy"), new Vector3(px , py), Quaternion.identity);
    }
    IEnumerator Kamiya4_2()
    {
        yield return new WaitForSeconds(1.0f);
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        myPV.RPC("RPC_Kamiya4_3",RpcTarget.AllViaServer,gameObject.name);
    }
    [PunRPC]
    void RPC_Kamiya4_3(string naming)
    {
        GameObject master = GameObject.Find(naming).gameObject;
        master.GetComponent<SpriteRenderer>().color = Color.white;
        master.tag = "Player";
        if (playerNumber == 1)
        {
            master.layer = 10;
        }
        else
        {
            master.layer = 11;
        }
    }
    /////////////////////////////////////////////////////////ニンザード///////////////////////////////////////////////////////////////////////
    [PunRPC]
    void RPC_Ninzard1(bool rightn,float px, float py,string naming)
    {
        GameObject a = (GameObject)Instantiate(Resources.Load("Character/Ninzard/Kunai"), new Vector3(px, py), Quaternion.identity);
        a.GetComponent<NinzardKunai>().SetMaster(GameObject.Find(naming).gameObject);
        myAudio.PlayOneShot(sound1);
        if (rightn == true)
        {
            //*a.GetComponent<Rigidbody2D>().AddForce(new Vector2(CharaAbiData.Ninzard(1), 0));
            a.transform.localScale = new Vector3(-0.5f, 0.5f, 0);
        }
        else
        {
            //*a.GetComponent<Rigidbody2D>().AddForce(new Vector2(- CharaAbiData.Ninzard(1), 0));
        }
    }
    [PunRPC]
    void RPC_Ninzard2(bool rightn, float px, float py,int numbers,float pxx,float pyy)
    {
        GameObject a = (GameObject)Instantiate(Resources.Load("Character/Ninzard/Dummy"), new Vector3(px, py), Quaternion.identity);
        if (rightn == true)
        {
            //a.GetComponent<Rigidbody2D>().AddForce(new Vector2(CharaAbiData.Ninzard(2), 0));
            a.transform.localScale = new Vector3(-0.6f, 0.6f, 0);
        }
        else
        {
            //a.GetComponent<Rigidbody2D>().AddForce(new Vector2(-CharaAbiData.Ninzard(2), 0));
        }
        a.GetComponent<Rigidbody2D>().velocity = new Vector2(pxx, pyy);
        gameObject.GetComponent<SpriteRenderer>().color = Color.clear;
        if (myPV.IsMine)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(255,255,255,100);
        }
    }
    IEnumerator Ninzard2_2()
    {
        yield return new WaitForSeconds(1.0f);
        myPV.RPC("RPC_Ninzard2_3", RpcTarget.AllViaServer);
    }
    [PunRPC]
    void RPC_Ninzard2_3()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
    [PunRPC]
    void RPC_Ninzard3(float px, float py, string naming)
    {
        GameObject a = (GameObject)Instantiate(Resources.Load("Character/Ninzard/Makibishi"), new Vector3(px, py - 2), Quaternion.identity);
        myAudio.PlayOneShot(sound1);
        a.GetComponent<NinzardMakibishi>().SetMaster(GameObject.Find(naming).gameObject);
    }
}
