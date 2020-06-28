using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arcaneBehavior : unitBehavior
{
    protected override bool evolveCondition()
    {
        int types = 0;

        if (FindObjectsOfType<knightBehavior>().Length > 0) types++;
        if (FindObjectsOfType<thiefBehavior>().Length > 0) types++;
        if (FindObjectsOfType<chungBehavior>().Length > 0) types++;
        if (FindObjectsOfType<arcaneBehavior>().Length > 0) types++;

        if (types >= 4) return true;
        else return false;
    }
}
