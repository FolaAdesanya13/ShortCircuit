using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject resume;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private PlayerInput playerInput;

    void Start()
    {
        pauseMenu.SetActive(false);
        if (playerInput == null) playerInput = GetComponent<PlayerInput>();
    }
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void pauseGame(InputAction.CallbackContext input)
    {
        if (!input.performed) return;

        if (!pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(resume);
        }
        else
        {
            Resume();
        }
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        playerInput.SwitchCurrentActionMap("Movement");
    }
}