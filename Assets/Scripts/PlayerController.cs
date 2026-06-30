using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController cc;
    private float speed = 5f;
    private Vector2 moveInput;
    private bool isGrounded;
    private float jumpPower = 4f;
    private float gravity = -9.81f;
    private float gravityConstant = -2f;
    private Vector3 playerVelocity;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = cc.isGrounded;

        Vector3 movementDirection = Vector3.zero;
        movementDirection.x = moveInput.x;
        movementDirection.z = moveInput.y;
        Vector3 ovrVelocity = transform.TransformDirection(movementDirection.normalized) * speed;

        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y <= 0)
        {
            playerVelocity.y = gravityConstant;
        }

        ovrVelocity.y = playerVelocity.y;
        cc.Move(ovrVelocity * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext input)
    {
        moveInput = input.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext input)
    {
        if (isGrounded && input.performed)
        {
            playerVelocity.y = Mathf.Sqrt(jumpPower * -2 * gravity);
        }
    }
}
