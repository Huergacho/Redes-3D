using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Photon.Pun;
using UnityEngine;

public class SP_Weapon : MonoBehaviourPun
{
    [SerializeField] protected WeaponStats stats;
    [SerializeField] protected Transform firePoint;
    private float currFireCooldown;
    private bool canStartTimer;
    // Start is called before the first frame update
    void Start()
    {
        stats.BulletPrefab.damage = stats.WeaponDamage;
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
        stats.BulletPrefab.damage = stats.WeaponDamage;
    }
    
    public void Shoot()
    {
        if (currFireCooldown <= 0)
        {
            BulletInstantiate();
            currFireCooldown = stats.FireRate;
            canStartTimer = true;
        }
    }

    protected virtual void BulletInstantiate()
    {
        var bulletClone = Instantiate(stats.BulletPrefab, firePoint.position, firePoint.rotation);
    }
}
