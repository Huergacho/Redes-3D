using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponStats stats;
    [SerializeField] private Transform firePoint;

    private float currFireCooldown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangeWeapon(WeaponStats newWeapon)
    {
        stats = newWeapon;
    }
    
    public void Shoot()
    {
        if (stats.FireRate >= currFireCooldown)
        {
            var bulletClone = Instantiate(stats.BulletPrefab,firePoint);
        }
    }
}
