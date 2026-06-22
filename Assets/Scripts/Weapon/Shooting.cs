using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Weapon Config")]
    public Guns gunsData;

    [Header("References")]
    public GameObject playerCam;
    public Transform muzzlePoint;

    // ── WeaponSwitcher reference — assign in Inspector or auto-found ──────────
    [Header("Animation")]
    [Tooltip("Drag the GameObject that holds WeaponSwitcher here.")]
    [SerializeField] private WeaponSwitcher weaponSwitcher;

    private float _nextFireTime;

    private void Start()
    {
        if (weaponSwitcher == null)
            weaponSwitcher = GetComponentInParent<WeaponSwitcher>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= _nextFireTime)
            TryShoot();
    }

    public void TryShoot()
    {
        _nextFireTime = Time.time + (1f / gunsData.fireRate);

        SpawnMuzzleFlash();

        for (int i = 0; i < gunsData.pelletsPerShot; i++)
            FireRay();




    }

    void FireRay()
    {
        Vector3 spread = Random.insideUnitSphere * Mathf.Tan(gunsData.spreadAngle * Mathf.Deg2Rad);
        Vector3 dir = (playerCam.transform.forward + spread).normalized;

        if (Physics.Raycast(playerCam.transform.position, dir, out RaycastHit hit, gunsData.range))
        {
            SpawnTrail(muzzlePoint.position, hit.point);
            SpawnImpact(hit);
        }
        else
        {
            Vector3 endPoint = playerCam.transform.position + dir * gunsData.range;
            SpawnTrail(muzzlePoint.position, endPoint);
        }
    }

    void SpawnMuzzleFlash()
    {
        if (gunsData.muzzleFlashPrefab == null) return;
        GameObject fx = Instantiate(gunsData.muzzleFlashPrefab, muzzlePoint.position, muzzlePoint.rotation, muzzlePoint);
        Destroy(fx, gunsData.muzzleFlashDuration);
    }

    void SpawnTrail(Vector3 from, Vector3 to)
    {
        if (gunsData.bulletTrailPrefab == null) return;

        GameObject trail = Instantiate(gunsData.bulletTrailPrefab, from, Quaternion.identity);
        TrailRenderer tr = trail.GetComponent<TrailRenderer>();

        if (tr != null)
            StartCoroutine(MoveTrail(tr, from, to));
    }

    IEnumerator MoveTrail(TrailRenderer tr, Vector3 from, Vector3 to)
    {
        float t = 0f;
        float travelTime = 0.05f;
        tr.transform.position = from;

        while (t < 1f)
        {
            t += Time.deltaTime / travelTime;
            tr.transform.position = Vector3.Lerp(from, to, t);
            yield return null;
        }
        Destroy(tr.gameObject, tr.time);
    }

    void SpawnImpact(RaycastHit hit)
    {
        if (hit.collider.tag == "Enemy")
        {
            if (gunsData.impactEffectEnemy == null) return;
            Quaternion rot = Quaternion.LookRotation(hit.normal);
            GameObject fx = Instantiate(gunsData.impactEffectEnemy, hit.point, rot);
            hit.collider.GetComponent<OnDamage>().TakeDamage(gunsData.damage);
            Destroy(fx, 0.4f);
        }

        else
        {
            if (gunsData.impactEffectPrefab == null) return;
            Quaternion rot = Quaternion.LookRotation(hit.normal);
            GameObject fx = Instantiate(gunsData.impactEffectPrefab, hit.point, rot);
            Destroy(fx, 0.4f);
        }
       
    }

}
