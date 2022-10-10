
using Photon.Pun;

public class MP_RoundManager : SP_RoundManager
    {
        protected override void Start()
        {
            if (Equals(photonView.Owner, PhotonNetwork.MasterClient))
            {
               // base.Start();
               print("blah");
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