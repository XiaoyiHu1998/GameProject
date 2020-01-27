using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DialogueStats 
{
    public static string[] Hints;

    //public static bool[] 

    static DialogueStats()
    {
        string[] tempHints = new string[30];
        tempHints[0] = "hoi Stefan";
        tempHints[1] = "hallo2";
        tempHints[2] = "hallo3";
        tempHints[22] = "22";
        tempHints[6] = "dit is de spawn scene";
        tempHints[7] = "top of spawn scene zin";

        Hints = tempHints;
    }
}
