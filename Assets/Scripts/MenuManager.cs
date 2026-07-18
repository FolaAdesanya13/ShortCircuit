using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    public GameObject loadingScreen, controlsMenu, mainMenu;

    void Start()
    {
        loadingScreen.SetActive(false);
        controlsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void PlayPressed()
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadGameScene());

    }
    private IEnumerator LoadGameScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameScene");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void ControlsPressed()
    {
        controlsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void ControlsBackButton()
    {
        controlsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void SettingsPressed()
    {

    }

    public void QuitPressed()
    {
        Application.Quit();
    }

}
