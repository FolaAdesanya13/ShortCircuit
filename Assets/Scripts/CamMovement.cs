using UnityEngine;
using UnityEngine.InputSystem;

public class CamMovement : MonoBehaviour
{
    public Camera cam;
    public InputActionReference look;
    private float xRotation = 0f, yRotation = 0f;
    private float mouseSens = 30f, gamepadSens = 150f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 lookInput = look.action.ReadValue<Vector2>();
        float sensitivity = lookInput.magnitude > 2f ? mouseSens : gamepadSens;
        Vector2 scaledInput = lookInput * sensitivity * Time.deltaTime;

        xRotation -= scaledInput.y;
        yRotation += scaledInput.x;

        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}
