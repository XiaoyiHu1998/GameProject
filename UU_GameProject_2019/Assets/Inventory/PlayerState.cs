using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : ScriptableObject
    
{
    public int Health;
    public int InvetorySelection;
    public bool BowUnlock, BombUnlock, BoomerangUnlock, SwordUnlock;
    public int Arrows, Bombs;
}
