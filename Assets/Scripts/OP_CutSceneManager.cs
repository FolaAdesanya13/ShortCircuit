using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class OP_CutSceneManager : MonoBehaviour
{
    private float skipScene = 5f;
    private const float skipConst = 5f;
    private bool skipping, skipCanceled;
    private bool isLoading = false;

    void Update()
    {
        if (skipping && skipScene > 0)
        {
            skipScene -= Time.deltaTime;
        }
        if (skipCanceled)
        {
            skipScene = skipConst;
            skipCanceled = false;
        }
        if (skipScene <= 0 && !isLoading) LoadGame();
    }

    public void Skip(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            skipping = true;
            skipCanceled = false;
        }
        if (context.canceled)
        {
            skipping = false;
            skipCanceled = true;
        }
    }

    private void LoadGame()
    {
        isLoading = true;
        SceneManager.LoadSceneAsync("GameScene");
    }
}
