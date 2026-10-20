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
        if (playButton != null)
        {
            EventSystem.current.SetSelectedGameObject(playButton);
        }

        if (loadingScreen != null)
        {
            loadingScreen.SetActive(false);
        }

        if (loadingBar != null)
        {
            loadingBar.fillAmount = 0;
        }
        /*controlsMenu.SetActive(false);*/
    }

    public void PlayPressed()
    {
        if (loadingScreen != null)
        {
            loadingScreen.SetActive(true);
        }
        Time.timeScale = 1;
        StartCoroutine(LoadGameScene());

    }
    private IEnumerator LoadGameScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameScene");
        while (!asyncLoad.isDone)
        {
            if (loadingBar != null)
            {
                loadingBar.fillAmount += asyncLoad.progress / 0.9f;
            }
            yield return null;
        }
        if(asyncLoad.isDone && loadingScreen != null) 
        {
            loadingScreen.SetActive(false);
        }
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
