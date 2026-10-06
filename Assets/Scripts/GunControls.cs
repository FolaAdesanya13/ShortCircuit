using UnityEngine;
using UnityEngine.InputSystem;

public class GunControls : MonoBehaviour
{
    private float damage = 10f, range = 30f;
    [SerializeField] private Camera cam;
    [SerializeField] private ParticleSystem muzzleFlash;
    private bool isShooting;
    private Quaternion initialRotation = Quaternion.Euler(0f, 0f, 0f);
    [SerializeField] private GameObject impactEffect;
    private float nextTimeToFire = 0f, fireRate = 20f;
    private ViewRecoil recoil;

    private void Start()
    {
        transform.SetLocalPositionAndRotation(transform.localPosition, initialRotation);
        recoil = transform.Find("CameraRot/CameraRecoil").GetComponent<ViewRecoil>();
    }
    void Update()
    {
        if (isShooting && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1/fireRate;
            ShootLogic();
        }
    }

    public void Shoot(InputAction.CallbackContext input)
    {
        if (input.started) isShooting = true;
        if (input.canceled) isShooting = false;
    }

    private void ShootLogic()
    {
        muzzleFlash.Play();
        recoil.AddRecoil();
        RaycastHit hit;
        GameObject impact = null;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target != null) target.TakeDamage(damage);

            impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 0.4f);
        }
    }
}
