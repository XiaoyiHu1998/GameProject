using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }
    
    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("begin dialogue: " + dialogue.instructionType);

        /*
        sentences.Clear();

        foreach(string sentences in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
        */

    }
    /*
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        Debug.Log(sentence);

    }

    void EndDialogue()
    {
        Debug.Log("einde van gesprek");
    }
    */

}
