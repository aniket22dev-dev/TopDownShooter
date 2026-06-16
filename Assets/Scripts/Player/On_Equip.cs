using UnityEngine;

public class On_Equip : MonoBehaviour
{
    private WeaponSwitcher _weaponSwitcher;

   
    private void Start()
    {
       
        _weaponSwitcher = FindAnyObjectByType<WeaponSwitcher>();

        if (_weaponSwitcher == null)
            Debug.LogError("WeaponSwitcher not found anywhere in the scene!");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_weaponSwitcher == null) return;

        if (collision.collider.CompareTag("Pistol"))
            EquipWeapon(WeaponSwitcher.Weapons.Pistol, collision.collider);
        else if (collision.collider.CompareTag("Rifle"))
            EquipWeapon(WeaponSwitcher.Weapons.Rifle, collision.collider);
        else if (collision.collider.CompareTag("Shotgun"))
            EquipWeapon(WeaponSwitcher.Weapons.Shotgun, collision.collider);
        else
            Debug.Log("Not a weapon pickup");
    }
    

    private void EquipWeapon(WeaponSwitcher.Weapons weapon, Collider pickup)
    {
        _weaponSwitcher.currentWeapon = weapon;
        _weaponSwitcher.ChangeWeapon();
        Debug.Log($"Equipped: {weapon}");
        pickup.gameObject.SetActive(false);
    }
}