using UnityEngine;
using UnityEngine.InputSystem;

public class GunControls : MonoBehaviour
{
    private float damage = 10f, range = 85f;
    [SerializeField] private Camera cam;
    [SerializeField] private ParticleSystem muzzleFlash;
    private bool isShooting;
    private Quaternion initialRotation = Quaternion.Euler(0f, 0f, 0f);
    [SerializeField] private GameObject impactEffect;
    private float nextTimeToFire = 0f, fireRate = 20f;
    private ViewRecoil recoil;
    [SerializeField] private GameObject crosshair;

    private Vector3 ogCrosshairScale, tgtCrosshairScale; //probably need to change position of crosshair slightly to add more responsiveness
    private float shootingScaleMult = 1.7f;
    void Awake()
    {
        ogCrosshairScale = crosshair.transform.localScale == Vector3.zero ? Vector3.one : crosshair.transform.localScale;
        tgtCrosshairScale = ogCrosshairScale * shootingScaleMult;
    }

    private void Start()
    {
        ogCrosshairScale = crosshair.transform.localScale == Vector3.zero ? Vector3.one : crosshair.transform.localScale;
        tgtCrosshairScale = ogCrosshairScale * shootingScaleMult;
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
        if (input.started)
        {
            crosshair.transform.localScale = Vector3.Slerp(ogCrosshairScale, tgtCrosshairScale, 1);
            isShooting = true;
        }
        if (input.canceled)
        {
            crosshair.transform.localScale = Vector3.Slerp(tgtCrosshairScale, ogCrosshairScale, 1);
            isShooting = false;
        }
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
