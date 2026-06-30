using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController cc;
    private float speed = 5f;
    private Vector2 moveInput;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        movePlayer();
    }

    public void OnMove(InputAction.CallbackContext input)
    {
        moveInput = input.ReadValue<Vector2>();
    }
    private void movePlayer()
    {
        Vector3 movementDirection = Vector3.zero;
        movementDirection.x = moveInput.x;
        movementDirection.z = moveInput.y;
        cc.Move(transform.TransformDirection(movementDirection.normalized) * speed * Time.deltaTime);
    }
}
