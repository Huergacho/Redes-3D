using UnityEngine;
using Photon.Pun;
public class MP_Weapon : SP_Weapon
    {
        protected override void BulletInstantiate()
        {
            var bulletClone = PhotonNetwork.Instantiate(stats.BulletPrefab.name,firePoint.position,firePoint.rotation);
            bulletClone.GetComponent<MP_Bullet>().owner = owner;
        }
    }