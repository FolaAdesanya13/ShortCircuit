using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


/*TO-DO: UN-COMMENT SCRIPTS WHEN THE DESIGN FOR SAID ELEMENTS ARE DONE, 
 * COMMENTED THEM OUT BECAUSE OF NULL POINTER EXCEPTIONS 
 * BUT LOGICALLY SPEAKING THEY SHOULD BE GOOD SO NO CHANGE REQUIRED*/


public class MenuManager : MonoBehaviour
{
    //public GameObject loadingScreen, controlsMenu, mainMenu;
    public GameObject playButton;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        EventSystem.current.SetSelectedGameObject(playButton);
        /*loadingScreen.SetActive(false);
        controlsMenu.SetActive(false);
        mainMenu.SetActive(true);*/
    }

    public void PlayPressed()
    {
        //loadingScreen.SetActive(true);
        Time.timeScale = 1;
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

    /*public void ControlsPressed()
    {
        controlsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void ControlsBackButton()
    {
        controlsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }*/

    public void SettingsPressed()
    {

    }

    public void QuitPressed()
    {
        Application.Quit();
    }

}
