using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateNextSentence : MonoBehaviour
{
    public GameObject dialogueManager;

    private void OnEnable()
    {
        DialogueScript dialogueScript = dialogueManager.gameObject.GetComponent<DialogueScript>();
        dialogueScript.PlayNextSentence();
    }
}
