using System.Collections;
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
            if (left != null) if (left.unit != null && !left.unit.playerUnit) return left;

            if (right != null) if (right.unit != null && !right.unit.playerUnit) return right;

            if (left == null && right == null)
            {
                Debug.Log("Player score!");
                return this;
            }

            bool canLeft = false, canRight = false;
            if (left != null && left.unit == null) canLeft = true;
            if (right != null && right.unit == null) canRight = true;

            if (canLeft && canRight)
            {
                int rand = Random.Range(0, 100);
                if (rand % 2 == 0) return left;
                else return right;
            }

            if (canLeft) return left;
            if (canRight) return right;

            return null;
        }
        else
        {
            if (backLeft != null)
            {
                if (backLeft.unit != null && backLeft.unit.playerUnit)
                {
                    Debug.Log("i move left cause enemy", gameObject);
                    return backLeft;
                }
            }

            if (backRight != null)
            {
                if (backRight.unit != null && backRight.unit.playerUnit)
                {
                    Debug.Log("i move right cause enemy", gameObject);
                    return backRight;
                }
            }

            if (backLeft == null && backRight == null)
            {
                Debug.Log("Player score!");
                return this;
            }

            bool canLeft = false, canRight = false;
            if (backLeft != null && backLeft.unit == null) canLeft = true;
            if (backRight != null && backRight.unit == null) canRight = true;

            if (canLeft && canRight)
            {
                Debug.Log("i pick random", gameObject);

                int rand = Random.Range(0, 100);
                if (rand % 2 == 0) return backLeft;
                else return backRight;
            }

            if (canLeft)
            {
                Debug.Log("i can move left", gameObject);
                return backLeft;
            }
            if (canRight)
            {
                Debug.Log("i can move right", gameObject);
                return backRight;
            }

            Debug.Log("i cant move", gameObject);
            return null;
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
