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

    /*void Start()
    {
        loadingScreen.SetActive(false);
        controlsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }*/

    public void PlayPressed()
    {
        //loadingScreen.SetActive(true);
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
