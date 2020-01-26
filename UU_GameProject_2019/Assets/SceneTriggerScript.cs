using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTriggerScript : MonoBehaviour
{ 
    public DialogueScript dialogueScript;

    void Start()
    {
        DialogueScript dialogueScript = FindObjectOfType<DialogueScript>();
        dialogueScript.OpenDialogue();
    }
}
