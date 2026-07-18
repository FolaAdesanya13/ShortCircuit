using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

/*TO-DO: MOVE OVER HOVER AND BUTTON EFFECTS OVER TO ITS OWN SEPARATE SCRIPT, 
 * KEEP MENU MANAGER FOR BORDERLINE FUNCTIONALITY*/

public class MenuManager : MonoBehaviour
{
    /*public Button play, controls, settings;
    private Vector3 playButtonScale, controlsButtonScale, settingsButtonScale;
    private float hoverScaleMult = 1.1f;*/

    public GameObject loadingScreen, controlsMenu, mainMenu;

    void Start()
    {
        /*playButtonScale = play.transform.localScale;
        controlsButtonScale = controls.transform.localScale;*/
        loadingScreen.SetActive(false);
        controlsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    /*public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = buttonScale * hoverScaleMult;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = buttonScale;
    }*/

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
