using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private CharacterController characterController;

    [Header("Joystick")]
    private Rigidbody rb;
    [SerializeField] private FixedJoystick fixedJoystick;
    public bool isJoystick;
    public static bool canMove=true;
    [Header("Animation")]
    [SerializeField] private PlayerAnimationController animationController;

    private void Start()
    {
        if (!isJoystick)
            characterController = GetComponent<CharacterController>();
        else
            rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!isJoystick)
        {
            Move();
            RotateTowardsMouse();
        }
    }

    private void FixedUpdate()
    {
        if (isJoystick)
        {
            if (canMove)
            {
                JoystickMove();
            }
            else
            {
                // Kill all momentum the moment movement is locked
                rb.linearVelocity = Vector3.zero;
            }
        }

    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(h, 0f, v).normalized;

        characterController.Move(moveDirection * speed * Time.deltaTime);

        animationController.SetMoveSpeed(moveDirection.magnitude);
    }

    private void JoystickMove()
    {
        rb.linearVelocity = new Vector3(
            fixedJoystick.Horizontal * speed,
            0,
            fixedJoystick.Vertical * speed);

        Vector3 direction = new Vector3(
            fixedJoystick.Horizontal,
            0,
            fixedJoystick.Vertical);

        float moveSpeed = direction.sqrMagnitude > 0.01f ? 1f : 0f;
       
        animationController.SetMoveSpeed(moveSpeed);

        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                Time.fixedDeltaTime * 15f);
        }
    }

    private void RotateTowardsMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, transform.position);

        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 worldMousePos = ray.GetPoint(distance);

            Vector3 lookDirection =
                worldMousePos - transform.position;

            lookDirection.y = 0f;

            if (lookDirection.sqrMagnitude > 0.01f)
                transform.rotation =
                    Quaternion.LookRotation(lookDirection);
        }
    }
}
