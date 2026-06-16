using UnityEngine;

public class OnDamage : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   [SerializeField] private float health = 100;
     AutoAimShooter AutoAimShooter;
    // Update is called once per frame

    private void Start()
    {
        AutoAimShooter = FindFirstObjectByType<AutoAimShooter>();


    }
    public void TakeDamage(float damage)
    {
        if (health > 0)
        {
            health -= damage;
            Debug.Log($"Enemy took the damage : {damage}  current health {health}");
        }
        if(health==0 || health<0)
        {
          BoxCollider collider = GetComponent<BoxCollider>();
            collider.enabled = false;
            AutoAimShooter.UnlockMovement();
            Destroy(gameObject, 0.1f);
            Debug.Log($"Health : {health} Enemy Died ");
        }

       //push
    }
}
