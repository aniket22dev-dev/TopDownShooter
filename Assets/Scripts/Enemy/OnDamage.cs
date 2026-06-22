using System.Collections;
using UnityEngine;

public class OnDamage : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float health = 100;
    AutoAimShooter AutoAimShooter;
    private Animator anim;
    // Update is called once per frame

    private void Start()
    {
        AutoAimShooter = FindFirstObjectByType<AutoAimShooter>();
        anim = GetComponent<Animator>();

    }
    public void TakeDamage(float damage)
    {
        if (health > 0)
        {
            health -= damage;
            StartCoroutine(HitImpact());
            Debug.Log($"Enemy took the damage : {damage}  current health {health}");
        }
        if (health == 0 || health < 0)
        {
            StartCoroutine(OnEnemyDead());
        }


    }

    IEnumerator OnEnemyDead()
    {
        BoxCollider collider = GetComponent<BoxCollider>();
        collider.enabled = false; // ? To Avoide triggring the lock movement 
        anim.enabled=false;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject,0.5f);
        AutoAimShooter.UnlockMovement();
        Debug.Log($"Health : {health} Enemy Died ");
    }
    IEnumerator HitImpact()
    {
        anim.SetBool("Hit", true);
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("Hit", false);

    }
}
