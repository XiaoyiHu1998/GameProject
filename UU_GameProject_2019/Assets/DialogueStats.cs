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
        tempHints[0] = "Check out the shop in the cave to the north-west, you can get a sword there!";
        tempHints[1] = "The tales say the treasure resides in the mountains to the north.";
        tempHints[2] = "You can defeat this enemy with a ranged weapon.";
        tempHints[3] = "You can encounter large boulders blocking your path. You can blow these up to clear your path.";
        tempHints[4] = "This is your last chance to stock up on supplies in the shop, choose wisely.";
        tempHints[5] = "To solve this puzzle, get away from the tree. North, East, West.";
        tempHints[6] = "It is advised to keep your distance from this monster.";

        Hints = tempHints;
    }
}
