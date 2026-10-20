using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
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
    [SerializeField] private TextMeshProUGUI ammo;
    private const int maxAmmo = 150;
    private int AmmoCount = 150;
    private bool isReloading = false;
    private float reloadTimer = 1.5f;

    private Vector3 ogCrosshairScale, tgtCrosshairScale;
    private float shootingScaleMult = 1.7f;

    [SerializeField] private AudioSource shootSource;
    [SerializeField] private PlayerInput playerInput;
    private float lastShotTime = -1f;

    void Awake()
    {
        if (crosshair == null)
        {
            Debug.LogError("Crosshair GameObject not assigned in " + gameObject.name);
            ogCrosshairScale = Vector3.one;
        }
        else
        {
            ogCrosshairScale = crosshair.transform.localScale == Vector3.zero ? Vector3.one : crosshair.transform.localScale;
        }
        tgtCrosshairScale = ogCrosshairScale * shootingScaleMult;
    }

    private void Start()
    {
        shootSource = GetComponent<AudioSource>();
        if (shootSource == null)
        {
            Debug.LogError("AudioSource component not found on " + gameObject.name);
        }

        AmmoCount = maxAmmo;
        if (crosshair != null)
        {
            ogCrosshairScale = crosshair.transform.localScale == Vector3.zero ? Vector3.one : crosshair.transform.localScale;
            tgtCrosshairScale = ogCrosshairScale * shootingScaleMult;
        }
        transform.SetLocalPositionAndRotation(transform.localPosition, initialRotation);

        Transform cameraRecoilTransform = transform.Find("CameraRot/CameraRecoil");
        if (cameraRecoilTransform != null)
        {
            recoil = cameraRecoilTransform.GetComponent<ViewRecoil>();
            if (recoil == null)
            {
                Debug.LogError("ViewRecoil component not found on CameraRecoil");
            }
        }
        else
        {
            Debug.LogError("CameraRot/CameraRecoil path not found");
        }
    }
    void Update()
    {
        if (isShooting && Time.time >= nextTimeToFire && AmmoCount > 0)
        {
            AmmoCount--;
            nextTimeToFire = Time.time + 1/fireRate;
            ShootLogic();
            if (ammo != null)
            {
                ammo.SetText("{0}", AmmoCount);
            }
        }
        if (AmmoCount <= 0 && !isReloading)
        {
            StartCoroutine(AutoReload());
        }

        SFX();
    }

    public void Shoot(InputAction.CallbackContext input)
    {
        if (crosshair == null) return;

        if (input.started && AmmoCount > 0)
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

    public void OnReload(InputAction.CallbackContext input)
    {
        if(input.performed)
        {
            AmmoCount = maxAmmo;
            if (ammo != null)
            {
                ammo.SetText("{0}", AmmoCount);
            }
        }
    }

    IEnumerator AutoReload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTimer);
        AmmoCount = maxAmmo;
        if (ammo != null)
        {
            ammo.SetText("{0}", AmmoCount);
        }
        isReloading = false;
    }

    private void ShootLogic()
    {
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        if (recoil != null)
        {
            recoil.AddRecoil();
        }

        SFX();
        RaycastHit hit;
        GameObject impact = null;

        if (cam != null && Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target != null) target.TakeDamage(damage);

            if (impactEffect != null)
            {
                impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, 0.4f);
            }
        }
    }

    private void SFX()
    {
        if (shootSource == null) return;

        if (isShooting)
        {
            if (!shootSource.isPlaying)
            {
                shootSource.Play();
            }
        }
        else
        {
            if (shootSource.isPlaying)
            {
                shootSource.Stop();
            }
        }
    }
}
