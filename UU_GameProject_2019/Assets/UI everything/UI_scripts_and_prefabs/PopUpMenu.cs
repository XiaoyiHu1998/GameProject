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

    public Button ResumeButton, ExitGameButton;

    public float closeDelay;

   // Scene currentScene = SceneManager.GetActiveScene();
   // string sceneName = currentScene.name;

    public void Awake()
    {
        //On click events for the pop up menu buttons

        ResumeButton.onClick.AddListener(ResumeGame);
        //GoToSettingsButton.onClick.AddListener(OpenSettings);
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
            PopUpPanel.DOScale(0.01f, 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            PopUpPanel.DOScale(1f, 1);
        }
    }

    public void ResumeGame()
    {
        //Time.timeScale = 1f;
        IsPaused = false;
        PopUpPanel.DOScale(0.0001f, 0.5f).SetDelay(closeDelay);
    }

    public void PauseGame()
    {
        PopUp.SetActive(true);
        //Time.timeScale = 0f;
        IsPaused = true;
        PopUpPanel.DOScale(1f, 0.5f);

    }


    public void ExitGame()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
