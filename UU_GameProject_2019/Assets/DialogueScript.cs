using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DialogueScript : MonoBehaviour
{
    public Text instructies;
    public string[] sentences;
    private int sentenceIndex;
    public float typeSpeed;

    public Button playNextSentenceButton, openDialogueButton, closeDialogueButton;

    public float opencloseSpeed, animationOffset;

    public RectTransform dialoguePanel, openDialogueButton2;

    public void Awake()
    {
        playNextSentenceButton.onClick.AddListener(PlayNextSentence);
        openDialogueButton.onClick.AddListener(OpenDialogue);
        closeDialogueButton.onClick.AddListener(CloseDialogue);
    }

    public void OpenDialogue()
    {
        instructies.text = "";
        //instructies.text = DialogueStats.Hints[0];
        dialoguePanel.DOAnchorPos(new Vector2(0, -420), opencloseSpeed);
        openDialogueButton2.DOAnchorPos(new Vector2(0, 700), opencloseSpeed).SetDelay(animationOffset);
        
        openDialogueButton.interactable = false;
        
        StartCoroutine(TypeSentence());
        
    }

    public void CloseDialogue()
    {
        dialoguePanel.DOAnchorPos(new Vector2(0, -750), opencloseSpeed);
        openDialogueButton2.DOAnchorPos(new Vector2(0, 500), opencloseSpeed).SetDelay(animationOffset);
        openDialogueButton.interactable = true;
    }

    public void Update()
    {
        if (instructies.text == sentences[sentenceIndex])
        {
            playNextSentenceButton.interactable = true;
        }
    }

    IEnumerator TypeSentence()
    {
        //instructies.text = DialogueStats.Hints[0];
         foreach (char letter in sentences[sentenceIndex].ToCharArray())
         {
             instructies.text += letter;
             yield return new WaitForSeconds(typeSpeed);
         }
    }

    public void PlayNextSentence()
    {
        playNextSentenceButton.interactable = false;

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
