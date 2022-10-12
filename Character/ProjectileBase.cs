using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{

    PhotonView pv;

    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVelocity(Vector2 v)
    {
        pv.RPC(nameof(AsyncSetVelocity), RpcTarget.All,v.x,v.y);
    }
    [PunRPC]
    public void AsyncSetVelocity(float x,float y)
    {
        Vector2 v = new Vector2(x, y);
        
    }
}
