using Photon.Pun;
using UnityEngine;

    public class MP_GenericPool : SP_GenericPool
    {
        protected override void Awake()
        {
            if (photonView.IsMine)
            {
                base.Awake();
            }
            else
            {
                Destroy(this);
            }
        }

        protected override GameObject InstancePoolObject(GameObject objectToSpawn, Transform desiredPos)
        {
            return PhotonNetwork.Instantiate(objectToSpawn.name, desiredPos.position,Quaternion.identity);
        }
    }