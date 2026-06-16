using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private WeaponSwitcher weaponSwitcher;

    private readonly int moveSpeedHash = Animator.StringToHash("MoveSpeed");
    private readonly int blendHash = Animator.StringToHash("Blend");

    public void SetMoveSpeed(float speed)
    {
      
        animator.SetFloat(moveSpeedHash, speed);
    }

    private void Update()
    {
        UpdateWeaponAnimation();
    }

    private void UpdateWeaponAnimation()
    {
        float blendValue = 0f;

        switch (weaponSwitcher.currentWeapon)
        {
            case WeaponSwitcher.Weapons.Pistol:
                blendValue = 1f;
                break;

            case WeaponSwitcher.Weapons.Rifle:
                blendValue = 2f;
                break;

            case WeaponSwitcher.Weapons.Shotgun:
                blendValue = 3f;
                break;
        }

        animator.SetFloat(blendHash, blendValue);

        // Aim Layer = Layer 1
        animator.SetLayerWeight(1, blendValue > 0 ? 1f : 0f);
    }
}
