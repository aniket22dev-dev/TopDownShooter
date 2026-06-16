using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class AutoAimShooter : MonoBehaviour
{
    [SerializeField] private FixedJoystick joystick;
    [Header("Animation")]
    [SerializeField] private PlayerAnimationController animationController;

    [SerializeField] private Slider rotationSlider;
    [SerializeField] private Transform player;

    private void Start()
    {
        rotationSlider.onValueChanged.AddListener(RotatePlayer);
    }


    private void RotatePlayer(float angle)
    {
        player.rotation = Quaternion.Euler(0f, angle * 360, 0f);
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Lockmovement();
        }
    }

    public void Lockmovement()
    {
        animationController.SetMoveSpeed(0);
        PlayerController.canMove = false;
        joystick.gameObject.SetActive(false);
        rotationSlider.gameObject.SetActive(true);
    }
    public void UnlockMovement()
    {
        Debug.Log("working");
        PlayerController.canMove = true;
        joystick.ResetJoystick();
        joystick.gameObject.SetActive(true);
        rotationSlider.gameObject.SetActive(false);
    }

  
}

