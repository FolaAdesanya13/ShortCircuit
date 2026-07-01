using UnityEngine;
using UnityEngine.InputSystem;

public class CamMovement : MonoBehaviour
{
    public Camera cam;
    public InputActionReference look;
    private float xRotation = 0f, yRotation = 0f;
    private float xSens = 30f, ySens = 30f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 lookInput = look.action.ReadValue<Vector2>() * Time.deltaTime;

        float mouseX = lookInput.x * xSens;
        float mouseY = lookInput.y * ySens;

        xRotation -= mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}
