using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIManagerScript : MonoBehaviour
{
    public RectTransform MenuPanel, OptionsPanel, LevelsPanel, MainOpeningPanel, playbutton, optionsbutton, quitbutton, buttonlayout;

    public Button button_Play, button_Options, button_Quit, button_OptionsToMenu, button_LevelsToMenu, button_Continue;

    public float animationSpeed, animationSpeed_button, animationDelay, fadeTime, buttonDelay;

    public Graphic fadeObject1, fadeObject2, fadeObject3, fadeObject4, fadeObject5, fadeObject6, fadeObject7, fadeObject8;

    public void Awake()
    {
        button_Options.onClick.AddListener(OpenOptions);
        button_OptionsToMenu.onClick.AddListener(CloseOptions);
       // button_Levels.onClick.AddListener(OpenLevels);
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
        MainOpeningPanel.DOAnchorPos(new Vector2(0, -450), animationSpeed).SetDelay(buttonDelay);
        MenuPanel.DOAnchorPos(Vector2.zero, animationSpeed).SetDelay(animationDelay).SetDelay(animationDelay + buttonDelay);  
    }

    public void OpenOptions()
    {
        MenuPanel.DOAnchorPos(new Vector2(850, 0), animationSpeed).SetDelay(0.5f); ;
        OptionsPanel.DOAnchorPos(Vector2.zero, animationSpeed).SetDelay(0.5f);
    }

    public void CloseOptions()
    {
        MenuPanel.DOAnchorPos(Vector2.zero, animationSpeed);
        OptionsPanel.DOAnchorPos(new Vector2(-850, 0), animationSpeed);
    }

    /*
    public void OpenLevels()
    {
        levelsbutton.DOPunchScale(new Vector3(0.5f, 0.5f, 0), 0.2f, 2, 1);
        MenuPanel.DOAnchorPos(new Vector2(-850, 0), animationSpeed).SetDelay(0.5f); ;
        LevelsPanel.DOAnchorPos(Vector2.zero, animationSpeed).SetDelay(animationDelay);
    }*/

    public void CloseLevels()
    {
        MenuPanel.DOAnchorPos(Vector2.zero, animationSpeed).SetDelay(animationDelay);
        LevelsPanel.DOAnchorPos(new Vector2(0, 450), animationSpeed);
    }

    public void QuitGame()
    {
        MenuPanel.DOAnchorPos(new Vector2(850, 0), animationSpeed).SetDelay(0.5f);
        MainOpeningPanel.DOAnchorPos(Vector2.zero, animationSpeed).SetDelay(animationDelay);

    }

    public void PlayGame()
    {
        //MenuPanel.DOScale(0.01f, 0.5f);
        fadeObject1.DOFade(0, fadeTime);
        fadeObject2.DOFade(0, fadeTime);
        fadeObject3.DOFade(0, fadeTime);
        fadeObject4.DOFade(0, fadeTime);
        fadeObject5.DOFade(0, fadeTime);
        fadeObject6.DOFade(0, fadeTime);
        fadeObject7.DOFade(0, fadeTime);
        fadeObject8.DOFade(0, fadeTime);
        //SceneManager.LoadScene("SpawnScene");
        Time.timeScale = 1f;
    }

}
