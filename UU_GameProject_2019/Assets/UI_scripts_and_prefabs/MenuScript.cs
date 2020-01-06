using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    //References to the buttons of the main menu/options/levelselect/etc.

    public Button playButton, optionsButton, quitButton, back_OptionsToMenu, levelsButton, back_LevelsToMenu, Level1Button, Level2Button, ContinueButton;

    //References to the panels of the main menu/options/levelselect/etc.

    public GameObject OptionsPanel, MenuPanel, LevelsPanel, MainOpeningPanel;

    public string startKey;

    public void Awake()
    {
        //On click events for all the buttons in the main menu/options/levelselect/etc.

        playButton.onClick.AddListener(StartGame);
        
       // optionsButton.onClick.AddListener(OpenOptions);
        
        back_OptionsToMenu.onClick.AddListener(GoBack_OptionsToMenu);

        levelsButton.onClick.AddListener(LevelSelectScreen);

        back_LevelsToMenu.onClick.AddListener(GoBack_LevelsToMenu);

        Level1Button.onClick.AddListener(LoadLevel1);

        Level2Button.onClick.AddListener(LoadLevel2);

        ContinueButton.onClick.AddListener(Continue);

       /* Scene currentScene = SceneManager.GetActiveScene();

        string sceneName = currentScene.name;

        if (sceneName == "MenuScene")
        {
            Debug.Log("je bent in menu scene");
            OptionsPanel.SetActive(true);
        }
        */
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SpawnScene");
        Time.timeScale = 1f;
    }
    /*
    public void OpenOptions()
    { 
        OptionsPanel.SetActive(true);
        MenuPanel.SetActive(false);        
    }
    */
    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoBack_OptionsToMenu()
    {
        OptionsPanel.SetActive(false);
        MenuPanel.SetActive(true);
    }

    public void GoBack_LevelsToMenu()
    {
        LevelsPanel.SetActive(false);
        MenuPanel.SetActive(true);
    }

    public void LevelSelectScreen()
    {
        LevelsPanel.SetActive(true);
        MenuPanel.SetActive(false);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("SpawnScene");
        Time.timeScale = 1f;
        Debug.Log("Level 1 loaded!");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("DungeonEntranceScene");
        Time.timeScale = 1f;
    }

    public void Continue()
    {
        Debug.Log("continued");
        MenuPanel.SetActive(true);
        MainOpeningPanel.SetActive(false);
    }
}
