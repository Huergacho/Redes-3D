using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "WeaponStats", menuName = "~/Documents/GitHub/Redes-3D/Assets/_Main/Scripts/Gun/WeaponStats.cs/WeaponStats", order = 0)]
public class WeaponStats : ScriptableObject {
[Header("Stats")]
    [SerializeField] private float fireRate;
    public float FireRate => fireRate;

    [SerializeField] private int weaponDamage;
    public int WeaponDamage => weaponDamage;
[Header("Prefabs")]
    [SerializeField] private MP_Bullet mpBulletPrefab;
    public MP_Bullet MpBulletPrefab => mpBulletPrefab;

    [SerializeField] private SP_Bullet _spBulletPrefab;
    public SP_Bullet SpBulletPrefab => _spBulletPrefab;

}