using Photon.Pun;
using UnityEngine;

    public class MP_PointCounter : SP_PointCounter
    {
        protected override void Start()
        {
            Initialize();
        }
        [PunRPC]
        public override void UpdatePoints(int data)
        {
            if(photonView.IsMine)
            base.UpdatePoints(data);
            else
            {
                photonView.RPC("UpdatePoints",photonView.Owner);
            }
        }
    }