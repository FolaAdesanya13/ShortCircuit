using UnityEngine;
using UnityEngine.InputSystem;

public class GunControls : MonoBehaviour
{
    private float damage = 10f, range = 100f;
    public Camera cam;

    public void Shoot(InputAction.CallbackContext input)
    {
        if (input.canceled)
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
            {
                EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
                if (target != null) target.TakeDamage(damage);
            }
        }
    }
}
