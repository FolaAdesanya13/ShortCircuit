using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

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
    [SerializeField] private TextMeshProUGUI ammo;
    private const int maxAmmo = 150;
    private int AmmoCount = 150;
    private bool isReloading = false;
    private float reloadTimer = 1.5f;

    private Vector3 ogCrosshairScale, tgtCrosshairScale; //probably need to change position of crosshair slightly to add more responsiveness
    private float shootingScaleMult = 1.7f;
    void Awake()
    {
        ogCrosshairScale = crosshair.transform.localScale == Vector3.zero ? Vector3.one : crosshair.transform.localScale;
        tgtCrosshairScale = ogCrosshairScale * shootingScaleMult;
    }

    private void Start()
    {
        AmmoCount = maxAmmo;
        ogCrosshairScale = crosshair.transform.localScale == Vector3.zero ? Vector3.one : crosshair.transform.localScale;
        tgtCrosshairScale = ogCrosshairScale * shootingScaleMult;
        transform.SetLocalPositionAndRotation(transform.localPosition, initialRotation);
        recoil = transform.Find("CameraRot/CameraRecoil").GetComponent<ViewRecoil>();
    }
    void Update()
    {
        if (isShooting && Time.time >= nextTimeToFire && AmmoCount > 0)
        {
            AmmoCount--;
            nextTimeToFire = Time.time + 1/fireRate;
            ShootLogic();
            ammo.SetText("{0}", AmmoCount);
        }
        if (AmmoCount <= 0 && !isReloading)
        {
            StartCoroutine(Reload());
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

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTimer);
        AmmoCount = maxAmmo;
        ammo.SetText("{0}", AmmoCount);
        isReloading = false;
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
