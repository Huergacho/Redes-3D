using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "WeaponStats", menuName = "~/Documents/GitHub/Redes-3D/Assets/_Main/Scripts/Gun/WeaponStats.cs/WeaponStats", order = 0)]
public class WeaponStats : ScriptableObject {
    [SerializeField] private float fireRate;
    public float FireRate => fireRate;

    [SerializeField] private int weaponDamage;
    public int WeaponDamage => weaponDamage;

    [SerializeField] private SP_Bullet bulletPrefab;
    public SP_Bullet BulletPrefab => bulletPrefab; 
}