using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OnDamage : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float maxHealth = 100f;
    private float health;

    [Header("UI")]
    [SerializeField] private Slider healthSlider;

    private AutoAimShooter autoAimShooter;
    private Animator anim;
    private Rigidbody[] ragdollBodies;
    private bool isDead = false;

    private Coroutine hideHealthBarRoutine;

    private void Start()
    {
        health = maxHealth;

        autoAimShooter = FindFirstObjectByType<AutoAimShooter>();
        anim = GetComponent<Animator>();
        ragdollBodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in ragdollBodies)
        {
            if (rb.gameObject == gameObject)
                continue;

            rb.isKinematic = true;
        }

        // Health bar setup
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth;
            healthSlider.gameObject.SetActive(false);
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead)
            return;

        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);

        // Show & update health bar
        if (healthSlider != null)
        {
            healthSlider.gameObject.SetActive(true);
            healthSlider.value = health;

            if (hideHealthBarRoutine != null)
                StopCoroutine(hideHealthBarRoutine);

            hideHealthBarRoutine = StartCoroutine(HideHealthBar());
        }

        if (health > 0)
        {
            StartCoroutine(HitImpact());
            Debug.Log($"Enemy took {damage} damage. Remaining Health: {health}");
        }
        else
        {
            isDead = true;
            OnEnemyDead();
        }
    }

    private void OnEnemyDead()
    {
        if (healthSlider != null)
            healthSlider.gameObject.SetActive(false);

        CapsuleCollider collider = GetComponent<CapsuleCollider>();
        if (collider != null)
            collider.enabled = false;

        anim.enabled = false;

        foreach (Rigidbody rb in ragdollBodies)
        {
            if (rb.gameObject == gameObject)
                continue;

            rb.isKinematic = false;
        }

        autoAimShooter.UnlockMovement();

        Destroy(gameObject, 3f);

        Debug.Log("Enemy Died");
    }

    IEnumerator HitImpact()
    {
        anim.SetBool("Hit", true);
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("Hit", false);
    }

    IEnumerator HideHealthBar()
    {
        yield return new WaitForSeconds(2f);

        if (!isDead && healthSlider != null)
            healthSlider.gameObject.SetActive(false);
    }
}