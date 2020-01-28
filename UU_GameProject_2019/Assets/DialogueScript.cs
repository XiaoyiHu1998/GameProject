using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class DialogueScript : MonoBehaviour
{
    public Text instructies;
    public string[] sentences;
    private int sentenceIndex;
    private int hintIndex;
    public float typeSpeed;

    public Button /*playNextSentenceButton,*/ openDialogueButton, closeDialogueButton;

    public float opencloseSpeed, animationOffset;

    public RectTransform dialoguePanel, openDialogueButton2;

    public void Awake()
    {
        //playNextSentenceButton.onClick.AddListener(PlayNextSentence);
        openDialogueButton.onClick.AddListener(OpenDialogue);
        closeDialogueButton.onClick.AddListener(CloseDialogue);
    }

    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string SceneName = currentScene.name;
        
        if (SceneName == "SpawnScene")
        {
            instructies.text = DialogueStats.Hints[0];
        }
        if (SceneName == "TopSpawnScene")
        {
            instructies.text = DialogueStats.Hints[1];
        }
        if (SceneName == "ForestScene")
        {
            instructies.text = DialogueStats.Hints[1];
        }
        if (SceneName == "OrangeScene")
        {
            instructies.text = DialogueStats.Hints[1];
        }
        if (SceneName == "SceneX4Y2")
        {
            instructies.text = DialogueStats.Hints[1];
        }
        if (SceneName == "BridgeScene")
        {
            instructies.text = DialogueStats.Hints[3];
        }
        if (SceneName == "SceneX2Y0")
        {
            instructies.text = DialogueStats.Hints[2];
        }
        if (SceneName == "DungeonEntranceScene")
        {
            instructies.text = DialogueStats.Hints[4];
        }
        if (SceneName == "LoopScene")
        {
            instructies.text = DialogueStats.Hints[5];
        }

    }

    public void OpenDialogue()
    {
        //instructies.text = "";
        dialoguePanel.DOAnchorPos(new Vector2(0, -420), opencloseSpeed);
        openDialogueButton2.DOAnchorPos(new Vector2(0, 700), opencloseSpeed).SetDelay(animationOffset);
        
        openDialogueButton.interactable = false;
        
        StartCoroutine(TypeSentence());
        
    }

    public void CloseDialogue()
    {
        dialoguePanel.DOAnchorPos(new Vector2(0, -750), opencloseSpeed);
        openDialogueButton2.DOAnchorPos(new Vector2(0, 480), opencloseSpeed).SetDelay(animationOffset);
        openDialogueButton.interactable = true;
    }

    public void Update()
    {/*
        if (instructies.text == sentences[sentenceIndex])
        {
            playNextSentenceButton.interactable = true;
        }*/
    }

    IEnumerator TypeSentence()
    {
        foreach (char letter in sentences[sentenceIndex].ToCharArray())
        {
             instructies.text += letter;
             yield return new WaitForSeconds(typeSpeed);
         }
        //creates a fluid animation where a sentence appears letter for letter
    }

    public void PlayNextSentence()
    {
        //playNextSentenceButton.interactable = false;

        if (sentenceIndex < sentences.Length - 1)
        {
            sentenceIndex++;
            instructies.text = "";
            StartCoroutine(TypeSentence());
        }
        else
        {
            instructies.text = "";
            
        }
    }
}
