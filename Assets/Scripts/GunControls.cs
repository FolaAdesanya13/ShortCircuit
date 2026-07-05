using UnityEngine;
using UnityEngine.InputSystem;

public class GunControls : MonoBehaviour
{
    private float damage = 10f, range = 100f;
    public Camera cam;
    public ParticleSystem muzzleFlash;
    public float debug = 0f;

    public void Shoot(InputAction.CallbackContext input)
    {
        //while (!input.canceled) muzzleFlash.Play(); Debug.Log(debug++);
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target != null) target.TakeDamage(damage);
        }
    }
}
