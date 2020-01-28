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
        tempHints[0] = "Check out the cave to the north!";
        tempHints[1] = "The tales say the treasure resides in the mountains to the north.";
        tempHints[2] = "You can defeat this enemy with a ranged weapon.";
        tempHints[3] = "Some alone standing rocks can be blown up with a bomb.";
        tempHints[4] = "This is your last chance to stock up on supplies, choose wisely.";
        tempHints[5] = "To solve this puzzle, get away from the tree. North, East, West.";
        tempHints[6] = "";
        tempHints[7] = "";

        Hints = tempHints;
    }
}
