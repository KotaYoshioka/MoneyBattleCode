using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mono : CharaBase
{
    bool levitationMode = false;

    //重力操作
    //右左に当たり判定が出現し、それにぶつかった相手を吹き飛ばす。
    public override void Ability1()
    {
        pv.RPC(nameof(GravityControl), RpcTarget.All, gameObject.transform.position.x);
    }
    [PunRPC]
    public void GravityControl(float nowX)
    {
        GameObject zoneA = (GameObject)Instantiate(Resources.Load("Character/Mono/MonoPower"), new Vector3(nowX - 2.6f, 0), Quaternion.identity);
        GameObject zoneB = (GameObject)Instantiate(Resources.Load("Character/Mono/MonoPower"), new Vector3(nowX + 13.2f, 0), Quaternion.identity);
        zoneA.GetComponent<MonoPower>().SetMaster(pv.Owner.UserId);
        zoneA.GetComponent<MonoPower>().Right(false);
        zoneB.GetComponent<MonoPower>().SetMaster(pv.Owner.UserId);
        zoneB.GetComponent<MonoPower>().Right(true);
    }

    //無重力固定
    //近くに当たり判定が出現し、それにぶつかった相手をその場で固定する。
    public override void Ability2()
    {
        pv.RPC(nameof(ZeroStop), RpcTarget.All, gameObject.transform.position.x, gameObject.transform.position.y);
    }
    [PunRPC]
    public void ZeroStop(float nowX, float nowY)
    {
        GameObject zoneZ = (GameObject)Instantiate(Resources.Load("Character/Mono/MonoStop"), new Vector3(nowX - 2, nowY + 5), Quaternion.identity);
        zoneZ.GetComponent<MonoStop>().SetMaster(pv.Owner.UserId);
    }

    //浮遊
    //その場で一定時間、重力が無くなる。
    public override void Ability3()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        levitationMode = true;
        abilityStop = true;
        StartCoroutine(nameof(LevitationEnd));
    }
    IEnumerator LevitationEnd()
    {
        yield return new WaitForSeconds(MonoData.FUYU_LONG_SECS);
        levitationMode = false;
        abilityStop = false;
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    //浮遊で下に自由に移動する
    private new void Update()
    {
        base.Update();
        if (levitationMode && Input.GetKey(KeyCode.S))
        {
            //速度の取得
            float newspeed = GetSpeed(false);
            if (newspeed == 0) return;
            //実際に下に移動する
            rigid.velocity = new Vector2(rigid.velocity.x, newspeed);
        }
    }

    //浮遊で上に自由に移動する
    protected override void Jump()
    {
        if (levitationMode)
        {
            //速度の取得
            float newspeed = GetSpeed(true);
            if (newspeed == 0) return;
            //実際に上に移動する
            rigid.velocity = new Vector2(rigid.velocity.x, newspeed);
        }
        else
        {
            base.Jump();
        }
    }

    public override void Ability4()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GetComponent<Rigidbody2D>().gravityScale = 0;
        TempStan(3.2f);
        pv.RPC(nameof(GravityBullet), RpcTarget.All, directionRight, gameObject.transform.position.x, gameObject.transform.position.y);
        StartCoroutine(nameof(Recoil));
    }
    [PunRPC]
    public void GravityBullet(bool right,float nowX,float nowY)
    {
        GameObject bullet = (GameObject) Instantiate(Resources.Load("Character/Mono/GravityBullet"), new Vector3(nowX  + (right?2.5f:-2.5f), nowY + 1.5f), Quaternion.identity);
        bullet.GetComponent<MonoGravityBullet>().SetMaster(pv.Owner.UserId);
        bullet.GetComponent<MonoGravityBullet>().Right(right);
    }
    IEnumerator Recoil()
    {
        yield return new WaitForSeconds(MonoData.DAN_STOP_AIR_SECS);
        GetComponent<Rigidbody2D>().gravityScale = 1;
        GetComponent<Rigidbody2D>().AddForce(new Vector2((directionRight?-1:1) * MonoData.DAN_RECOIL,0));
    }
}
