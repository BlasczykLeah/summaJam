﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spaceComponent : MonoBehaviour
{
    [Header("Player")]
    public spaceComponent left;
    public spaceComponent right;

    public Material playerMat;

    [Header("Enemy")]
    public spaceComponent backLeft;
    public spaceComponent backRight;

    public Material enemyMat;

    [Header("Both")]
    public Material defaultMat;

    public unitBehavior unit;
    public GameObject myHex;

    public spaceComponent chooseSpace(bool player)
    {
        if (player)
        {
            if (left) if (left.unit || !right) return left;

            if (right) if (right.unit || !left) return right;

            int rand = Random.Range(0, 100);
            if (rand % 2 == 0) return left;
            else return right;
        }
        else
        {
            if (backLeft) if (backLeft.unit || !backRight) return backLeft;

            if (backRight) if (backRight.unit || !backLeft) return backRight;

            int rand = Random.Range(0, 100);
            if (rand % 2 == 0) return backLeft;
            else return backRight;
        }
    }

    public void removeUnit()
    {
        unit = null;
        myHex.GetComponent<MeshRenderer>().material = defaultMat;
    }

    public void addUnit(unitBehavior u)
    {
        if (unit) return;

        unit = u;
        if(u.playerUnit) myHex.GetComponent<MeshRenderer>().material = playerMat;
        else myHex.GetComponent<MeshRenderer>().material = enemyMat;
    }
}