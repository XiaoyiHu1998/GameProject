using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIManagerScript : MonoBehaviour
{
    public RectTransform MenuPanel, OptionsPanel, LevelsPanel, MainOpeningPanel, playbutton, optionsbutton, levelsbutton, quitbutton, buttonlayout;

    public Button button_Play, button_Options, button_Quit, button_OptionsToMenu, button_Levels, button_LevelsToMenu, button_Continue;

    public float animationSpeed, animationSpeed_button, animationDelay;

    public void Awake()
    {
        button_Options.onClick.AddListener(OpenOptions);
        button_OptionsToMenu.onClick.AddListener(CloseOptions);
        button_Levels.onClick.AddListener(OpenLevels);
        button_LevelsToMenu.onClick.AddListener(CloseLevels);
        button_Continue.onClick.AddListener(Continue);
        button_Quit.onClick.AddListener(QuitGame);
        button_Play.onClick.AddListener(PlayGame);
    }

    void Start()
    {
        MainOpeningPanel.DOAnchorPos(Vector2.zero, animationSpeed);
    }

    public void Continue()
    {
        MainOpeningPanel.DOAnchorPos(new Vector2(0, -450), animationSpeed);
        MenuPanel.DOAnchorPos(Vector2.zero, animationSpeed).SetDelay(animationDelay).SetDelay(animationDelay);
        //buttonlayout.DOAnchorPos(Vector2.zero, animationSpeed).SetDelay(1.5f);
        //playbutton.DOAnchorPos(Vector2.zero, animationSpeed_button).SetDelay(2);
        //levelsbutton.DOAnchorPos(Vector2.zero, animationSpeed_button).SetDelay(2.1f);
        //optionsbutton.DOAnchorPos(Vector2.zero, animationSpeed_button).SetDelay(2.2f);
        //quitbutton.DOAnchorPos(Vector2.zero, animationSpeed_button).SetDelay(2.3f);
        
    }

    public void OpenOptions()
    {
        optionsbutton.DOPunchScale(new Vector3(0.5f, 0.5f, 0), 0.2f, 2, 1);
        MenuPanel.DOAnchorPos(new Vector2(-850, 0), animationSpeed).SetDelay(0.5f); ;
        OptionsPanel.DOAnchorPos(Vector2.zero, animationSpeed).SetDelay(0.5f);
    }

    public void CloseOptions()
    {
        MenuPanel.DOAnchorPos(Vector2.zero, animationSpeed);
        OptionsPanel.DOAnchorPos(new Vector2(850, 0), animationSpeed);
    }

    public void OpenLevels()
    {
        levelsbutton.DOPunchScale(new Vector3(0.5f, 0.5f, 0), 0.2f, 2, 1);
        MenuPanel.DOAnchorPos(new Vector2(-850, 0), animationSpeed).SetDelay(0.5f); ;
        LevelsPanel.DOAnchorPos(Vector2.zero, animationSpeed).SetDelay(animationDelay);
    }

    public void CloseLevels()
    {
        MenuPanel.DOAnchorPos(Vector2.zero, animationSpeed).SetDelay(animationDelay);
        LevelsPanel.DOAnchorPos(new Vector2(0, 450), animationSpeed);
    }

    public void QuitGame()
    {
        quitbutton.DOPunchScale(new Vector3(0.5f, 0.5f, 0), 0.2f, 2, 1);
        MenuPanel.DOAnchorPos(new Vector2(-850, 0), animationSpeed).SetDelay(0.5f);
        MainOpeningPanel.DOAnchorPos(Vector2.zero, animationSpeed).SetDelay(animationDelay);

    }

    public void PlayGame()
    {
        //playbutton.DOShakePosition(1, new Vector3(5, 0, 0), 15, 0, false, false);
        playbutton.DOPunchScale(new Vector3(0.5f, 0.5f, 0), 0.2f, 2, 1);
        SceneManager.LoadScene("SpawnScene");
        Time.timeScale = 1f;
    }

}
