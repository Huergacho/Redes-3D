using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStats", menuName = "~/Documents/GitHub/Redes-3D/Assets/_Main/Scripts/Gun/WeaponStats.cs/WeaponStats", order = 0)]
public class WeaponStats : ScriptableObject {
    [SerializeField] private float fireRate;
    public float FireRate => fireRate;

    [SerializeField] private float weaponDamage;
    public float WeaponDamage => weaponDamage;

    [SerializeField] private Bullet bulletPrefab;
    public Bullet BulletPrefab => bulletPrefab; 
}