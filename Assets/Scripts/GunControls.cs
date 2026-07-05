using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class GunControls : MonoBehaviour
{
    private float damage = 10f, range = 100f;
    public Camera cam;
    public ParticleSystem muzzleFlash;
    private bool isShooting;

    void Update()
    {
        if (isShooting) ShootLogic();
    }

    public void Shoot(InputAction.CallbackContext input)
    {
        if (input.started) isShooting = true;
        if (input.canceled) isShooting = false;
    }

    private void ShootLogic()
    {
        muzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target != null) target.TakeDamage(damage);
        }
    }
}
