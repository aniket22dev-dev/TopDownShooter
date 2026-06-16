using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0f, 15f, -8f); // adjust for your angle
    [SerializeField] private Vector3 offsetRotation = new Vector3(0f, 15f, -8f); // adjust for your angle

    private void LateUpdate()
    {
        // Stays at fixed angle, just follows player position
        transform.position = target.position + offset;
        float YRoatation = target.eulerAngles.y;
       // transform.rotation = Quaternion.Euler(offsetRotation.x,offsetRotation.y*YRoatation,offsetRotation.z);

    }
}