using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spaceComponent : MonoBehaviour
{
    public spaceComponent left;
    public spaceComponent right;

    public bool hasUnit = false;

    public spaceComponent chooseSpace()
    {
        if (left) if (left.hasUnit || !right) return left;

        if (right) if (right.hasUnit || !left) return right;

        int rand = Random.Range(0, 100);
        if (rand % 2 == 0) return left;
        else return right;
    }
}
