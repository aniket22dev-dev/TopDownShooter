using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0f, 6f, -4f);
    [SerializeField] private float smoothTime = 0.15f;

    public bool onEnemyRadius = false;

    private Vector3 _velocity;
    private Vector3 lastEnemyPos;

    private void LateUpdate()
    {
        if (onEnemyRadius == false || lastEnemyPos == Vector3.zero)
        {
            SetCam();
        }
        else
        {
            LookAtEnemy(lastEnemyPos);
        }
    }

    public void SetCam()
    {
        Vector3 targetPosition = target.position + offset;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref _velocity,
            smoothTime
        );

        // Smoothly return to default rotation
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.Euler(50, 0, 0),
            Time.deltaTime / smoothTime
        );
    }

    public void LookAtEnemy(Vector3 enemyPos)
    {
        lastEnemyPos = enemyPos;

        // Midpoint between player and enemy
        Vector3 midPoint = (target.position + enemyPos) / 2f;

        // Direction from player to enemy on XZ plane
        Vector3 dir = (enemyPos - target.position);
        dir.y = 0f;

        // Y angle to face enemy direction
        float yaw = dir == Vector3.zero ? 0f : Quaternion.LookRotation(dir).eulerAngles.y;

        // Rotate offset by that yaw so camera stays behind player relative to enemy
        Quaternion yawRotation = Quaternion.Euler(0, yaw, 0);
        Vector3 rotatedOffset = yawRotation * offset;

        Vector3 targetPosition = midPoint + rotatedOffset;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref _velocity,
            smoothTime
        );

        // Keep overhead pitch (50) but rotate Y to face enemy
        Quaternion targetRotation = Quaternion.Euler(50, yaw, 0);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            Time.deltaTime / smoothTime
        );
    }
}