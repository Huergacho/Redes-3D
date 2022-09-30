using UnityEngine;

namespace _Main.Scripts.SinglePlayer.Gun
{
    public class SP_Weapon : MonoBehaviour
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
                var bulletClone = Instantiate(stats.MpBulletPrefab,firePoint.position,firePoint.rotation);
                currFireCooldown = stats.FireRate;
                canStartTimer = true;
            }
        }
    }
}