using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class MP_Bullet : SP_Bullet 
{


    protected override void Update()
    {
        if (photonView.IsMine)
        {
          base.Update();  
        }

    }
    protected override void DestroyObject()
    {
        PhotonNetwork.Destroy(gameObject);
    }
    
    protected override void OnCollisionEnter(Collision other) 
    {
        if (photonView.IsMine)
        {
            base.OnCollisionEnter(other);
        }
    }

    protected override SP_LifeController GetTargetLifeComponent(GameObject target)
    {
        return target.GetComponent<MP_LifeController>();
    }
}