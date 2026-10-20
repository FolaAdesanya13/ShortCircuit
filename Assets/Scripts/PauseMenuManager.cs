using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject resume;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private PlayerInput playerInput;

    void Start()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
        if (playerInput == null)
        {
            playerInput = GetComponent<PlayerInput>();
            if (playerInput == null)
            {
                Debug.LogError("PlayerInput component not found on " + gameObject.name);
            }
        }
    }
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void pauseGame(InputAction.CallbackContext input)
    {
        if (!input.performed) return;

        if (pauseMenu == null || resume == null) return;

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
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
        Time.timeScale = 1f;
        if (playerInput != null)
        {
            playerInput.SwitchCurrentActionMap("Movement");
        }
    }

    public void Quit()
    {
        SceneManager.LoadSceneAsync("TitleScreen");
    }
}