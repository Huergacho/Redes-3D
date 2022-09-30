using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Photon.Pun;
using UnityEngine;

public class MP_Weapon : MonoBehaviourPun
{
    [SerializeField] private WeaponStats stats;
    [SerializeField] private Transform firePoint;
    private float currFireCooldown;
    private bool canStartTimer;
    // Start is called before the first frame update
    void Start()
    {
        stats.MpBulletPrefab.damage = stats.WeaponDamage;
        currFireCooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (canStartTimer && currFireCooldown >= 0)
        {
            currFireCooldown -= Time.deltaTime;
        }
    }

    private void ChangeWeapon(WeaponStats newWeapon)
    {
        stats = newWeapon;
        stats.MpBulletPrefab.damage = stats.WeaponDamage;
    }
    
    public void Shoot()
    {
        if (currFireCooldown <= 0)
        {
            var bulletClone = PhotonNetwork.Instantiate(stats.MpBulletPrefab.name,firePoint.position,firePoint.rotation);
            currFireCooldown = stats.FireRate;
            canStartTimer = true;
        }
    }
}
