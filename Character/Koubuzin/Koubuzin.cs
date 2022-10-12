using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koubuzin : CharaBase
{
    //硬化
    //無敵→ダメージ軽減　この間、スロー
    public override void Ability1()
    {
        AddCooldown(0, CharaData.GetCooldown(0, 0));
        TempInvincible(KoubuzinData.KOKA_SPECIAL_SECS);
        float sum = KoubuzinData.KOKA_DAMAGE_REDUCE_SECS + KoubuzinData.KOKA_SPECIAL_SECS;
        TempBonusSpeed(-(speed / 2), sum);
        AsyncTempColorChange(new Color(0.5f, 0.5f, 0.5f), sum);
        StartCoroutine(nameof(ReduceTime));
    }
    IEnumerator ReduceTime()
    {
        yield return new WaitForSeconds(KoubuzinData.KOKA_SPECIAL_SECS);
        TempDamageGuard(KoubuzinData.KOKA_DAMAGE_REDUCE_POWER, KoubuzinData.KOKA_DAMAGE_REDUCE_SECS);
    }

    //石紋
    //向いている方向に石の波をズレて2つ出す
    public override void Ability2()
    {
        AddCooldown(1, CharaData.GetCooldown(0, 1));
        pv.RPC(nameof(FirstWave),RpcTarget.All,directionRight,gameObject.transform.position.x);
        StartCoroutine(nameof(SecondWaveDelay));
    }
    [PunRPC]
    public void FirstWave(bool right, float nowX)
    {
        GameObject iwa = (GameObject)Instantiate(Resources.Load("Character/Koubuzin/BlueStoned"), new Vector3(nowX, -12.5f), Quaternion.identity);
        if (right)
        {
            iwa.transform.localScale = new Vector3(-2, 2, 1);
        }
        iwa.GetComponent<KoubuzinIwa>().SetOwnerID(pv.Owner.UserId,right);
    }
    IEnumerator SecondWaveDelay()
    {
        yield return new WaitForSeconds(KoubuzinData.MON_SECOND_DELAY_SECS);
        pv.RPC(nameof(SecondWave), RpcTarget.All,directionRight,gameObject.transform.position.x);
    }
    [PunRPC]
    public void SecondWave(bool right, float nowX)
    {
        GameObject iwa = (GameObject)Instantiate(Resources.Load("Character/Koubuzin/GoldStoned"), new Vector3(nowX-4f, -13.5f), Quaternion.identity);
        if (right)
        {
            iwa.transform.localScale = new Vector3(-2, 2, 1);
            iwa.transform.position = new Vector2(iwa.transform.position.x + 8, iwa.transform.position.y);
        }
        iwa.GetComponent<KoubuzinIwa>().SetOwnerID(pv.Owner.UserId,right);
    }

    //岩石落し
    //落ちる岩を召喚する
    public override void Ability3()
    {
        AddCooldown(2, CharaData.GetCooldown(0, 2));
        GameObject fallstone = PhotonNetwork.Instantiate("Character/Koubuzin/Stone",new Vector3(gameObject.transform.position.x + (directionRight?4:-4),15),Quaternion.identity);
        fallstone.GetComponent<KoubuzinFallStone>().SetOwnerID(pv.Owner.UserId);  
        //pv.RPC(nameof(FallStone), RpcTarget.All, directionRight, gameObject.transform.position.x);
    }
    [PunRPC]
    public void FallStone(bool right,float nowX)
    {
        GameObject fallstone = (GameObject)Instantiate(Resources.Load("Character/Koubuzin/Stone"), new Vector3(nowX + (right?4:-4), 15), Quaternion.identity);
        fallstone.GetComponent<KoubuzinFallStone>().SetOwnerID(pv.Owner.UserId);
    }

    //巨石
    //地面から大きな石の壁を召喚する
    public override void Ability4()
    {
        AddCooldown(3, CharaData.GetCooldown(0, 3));
        pv.RPC(nameof(WallStone), RpcTarget.All, directionRight, gameObject.transform.position.x);
    }
    [PunRPC]
    public void WallStone(bool right,float nowX)
    {
        GameObject wall = (GameObject)Instantiate(Resources.Load("Character/Koubuzin/StoneWall"), new Vector3(nowX + (right?10:-10), -15), Quaternion.identity);
    }
}
