using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen; //controlsMenu;
    [SerializeField] private GameObject playButton;
    [SerializeField] private Image loadingBar;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(playButton);
        loadingScreen.SetActive(false);
        loadingBar.fillAmount = 0;
        /*controlsMenu.SetActive(false);*/
    }

    public void PlayPressed()
    {
        loadingScreen.SetActive(true);
        Time.timeScale = 1;
        StartCoroutine(LoadGameScene());

    }
    private IEnumerator LoadGameScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameScene");
        while (!asyncLoad.isDone)
        {
            loadingBar.fillAmount += asyncLoad.progress / 0.9f;
            yield return null;
        }
        if(asyncLoad.isDone) loadingScreen.SetActive(false);
    }

    /*public void ControlsPressed()
    {
        controlsMenu.SetActive(true);
    }

    public void ControlsBackButton()
    {
        controlsMenu.SetActive(false);
    }*/

    public void SettingsPressed()
    {

    }

    public void QuitPressed()
    {
        Application.Quit();
    }

}
