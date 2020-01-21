using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOpenDialogue : MonoBehaviour
{
    public GameObject dialogueManager;

    private void OnEnable()
    {
        DialogueScript dialogueScript = dialogueManager.gameObject.GetComponent<DialogueScript>();
        dialogueScript.OpenDialogue();
    }
}
