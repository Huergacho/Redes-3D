using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
    public class MP_GenericPool : SP_GenericPool
    {
        protected override void Awake()
        {
            if(!PhotonNetwork.IsMasterClient)
            {
                photonView.RPC(nameof(GetInstance),photonView.Owner,PhotonNetwork.MasterClient);
            }
            else
            {
                SetInstance();
            }

        }

        [PunRPC]
        public void GetInstance(Player client)
        {
            photonView.RPC(nameof(SetInstance),client);
        }

        [PunRPC]
        public void SetInstance()
        {
            Instance = this;
        }

        protected override GameObject InstancePoolObject(GameObject objectToSpawn, Transform desiredPos)
        {
            return PhotonNetwork.Instantiate(objectToSpawn.name, desiredPos.position,Quaternion.identity);
        }
    }