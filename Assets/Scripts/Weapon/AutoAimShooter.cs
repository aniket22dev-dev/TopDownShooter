using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class AutoAimShooter : MonoBehaviour
{
    [SerializeField] private FixedJoystick joystick;
    [Header("Animation")]
    [SerializeField] private PlayerAnimationController animationController;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private Slider rotationSlider;
    [SerializeField] private Transform player;

    private void Start()
    {
        rotationSlider.onValueChanged.AddListener(RotatePlayer);
        mainCamera= GameObject.FindWithTag("MainCamera");
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
            mainCamera.GetComponent<CameraMovement>().LookAtEnemy(other.transform.position);
        }
    }

    public void Lockmovement()
    {
        animationController.SetMoveSpeed(0);
        PlayerController.canMove = false;
        joystick.gameObject.SetActive(false);
        rotationSlider.gameObject.SetActive(true);
        mainCamera.GetComponent<CameraMovement>().onEnemyRadius = true;
    }
    public void UnlockMovement()
    {
        Debug.Log("working");
        PlayerController.canMove = true;
        joystick.ResetJoystick();
        joystick.gameObject.SetActive(true);
        rotationSlider.gameObject.SetActive(false);
        mainCamera.GetComponent<CameraMovement>().onEnemyRadius = false;
        mainCamera.GetComponent<CameraMovement>().SetCam();
    }


}

