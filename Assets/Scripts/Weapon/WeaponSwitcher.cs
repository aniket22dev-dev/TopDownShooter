using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public enum Weapons { None, Pistol, Shotgun, Rifle }
    public GameObject[] Model_weapon;
    public Weapons currentWeapon;
    void Start()
    {
        currentWeapon = Weapons.None;
    }


    public void ChangeWeapon()
    {
        foreach (GameObject Gun in Model_weapon)
        {
            Gun.SetActive(false);
        }

        switch (currentWeapon)
        {
            case Weapons.Pistol:
                Debug.Log("Equipped Pistol");
                Model_weapon[1].SetActive(true);
                break;

            case Weapons.Shotgun:
                Debug.Log("Equipped Shotgun");
                Model_weapon[2].SetActive(true);
                break;

            case Weapons.Rifle:
                Debug.Log("Equipped Rifle");
                Model_weapon[3].SetActive(true);
                break;
        }
    }
}
