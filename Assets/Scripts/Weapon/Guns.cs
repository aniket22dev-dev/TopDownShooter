using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/WeaponData")]
public class Guns : ScriptableObject
{
    public string weaponName;

    // Firing
    public float fireRate;  // shots per second
    public float damage;
    public float range;     // max raycast distance
    

    // Pellets (shotgun = 8, others = 1)
    public int pelletsPerShot;
    public float spreadAngle;  // degrees of random spread

    // Effects (assign in Inspector)
    public GameObject muzzleFlashPrefab;
    public GameObject bulletTrailPrefab;  // TrailRenderer prefab
    public GameObject impactEffectPrefab;
    public GameObject impactEffectEnemy;
    public float muzzleFlashDuration; // seconds
}
