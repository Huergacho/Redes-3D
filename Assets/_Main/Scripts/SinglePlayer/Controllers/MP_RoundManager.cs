
using System.Collections;
using Photon.Pun;

public class MP_RoundManager : SP_RoundManager
    {
        protected override void Start()
        {
            if (photonView.IsMine)
            {
                base.Start();
            }
        }

        // protected override void AddRound()
        // {
        //     if (photonView.IsMine)
        //     {
        //         base.AddRound();
        //     }
        // }
        //
        // protected override void CallToSpawn()
        // {
        //     if (photonView.IsMine)
        //     {
        //         base.CallToSpawn();
        //     }
        // }
    }