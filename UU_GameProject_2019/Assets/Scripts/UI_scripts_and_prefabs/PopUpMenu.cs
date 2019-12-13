using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PopUpMenu : MonoBehaviour
{
    public static bool IsPaused = false;

    //References to the Pop up menu panel and the OptionsPanel from the main menu

    public GameObject PopUp, OptionsPanel;

    public Button ResumeButton, GoToSettingsButton, ExitGameButton;

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
    }

    public void ResumeGame()
    {
        PopUp.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void PauseGame()
    {
        PopUp.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("MenuScene");
        OptionsPanel.SetActive(true);
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
