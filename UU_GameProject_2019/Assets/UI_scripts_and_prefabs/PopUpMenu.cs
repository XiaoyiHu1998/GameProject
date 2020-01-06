using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PopUpMenu : MonoBehaviour
{
    public static bool IsPaused = false;

    //References to the Pop up menu panel and the OptionsPanel from the main menu

    public RectTransform PopUpPanel;

    public GameObject PopUp, OptionsPanel;

    public Button ResumeButton, GoToSettingsButton, ExitGameButton;

   // Scene currentScene = SceneManager.GetActiveScene();
   // string sceneName = currentScene.name;

    public void Awake()
    {
        //On click events for the pop up menu buttons

        ResumeButton.onClick.AddListener(ResumeGame);
        GoToSettingsButton.onClick.AddListener(OpenSettings);
        ExitGameButton.onClick.AddListener(ExitGame);
        
    }

    void Update()
    {
        //check if the escape button is pressed and check if the game is already paused, then resume or pause game accordingly.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
             if (IsPaused)
             {
                 ResumeGame();
                 Debug.Log("You resumed");
             }
             else
             {
                 PauseGame();
                 Debug.Log("You paused");
             }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            //PopUpPanel.DOAnchorPos(new Vector2(1000, 0), 5);
            //PopUpPanel.DOSizeDelta(new Vector2(0,0), 5, false);
            PopUpPanel.DOScale(0.01f, 0.5f);
            Debug.Log("P");
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            PopUpPanel.DOScale(1f, 1);
            Debug.Log("I");
        }
    }

    public void ResumeGame()
    {
        //PopUp.SetActive(false);
        //Time.timeScale = 1f;
        IsPaused = false;
        PopUpPanel.DOScale(0.01f, 0.5f);
    }

    public void PauseGame()
    {
        PopUp.SetActive(true);
        //Time.timeScale = 0f;
        IsPaused = true;
        PopUpPanel.DOScale(1f, 0.5f);

    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("MenuScene");
        //OptionsPanel.SetActive(true);
       /* if (sceneName == "MenuScene")
        {
            Debug.Log("yoyo");
        }*/
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
