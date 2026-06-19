using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0f, 15f, -8f); // adjust for your angle
    public bool onEnemyRadius = false;
    private void LateUpdate()
    {
        if (onEnemyRadius==false)
        {
            SetCam();
        }
        
    }
    public void SetCam()
    {
        transform.position = target.position + offset;
        transform.rotation = Quaternion.Euler(50, 0, 0);
    }
    public void LookAtEnemy(Vector3 enemyPos)
    {
        Vector3 targetPos = enemyPos * 1.5f;
        transform.LookAt(targetPos);
       
    }
}