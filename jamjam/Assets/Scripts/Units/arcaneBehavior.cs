using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arcaneBehavior : unitBehavior
{
    public override void objectSpawned(spaceComponent startingPoint, bool isPlayer)
    {
        maxHealth = health;

        myHealthBar = Instantiate(healthBar, GameObject.Find("Canvas").transform);
        Vector2 barSpot = Camera.main.WorldToScreenPoint(transform.position);
        myHealthBar.transform.position = new Vector2(barSpot.x, barSpot.y + barOffset);

        playerUnit = isPlayer;
        mySpace = startingPoint;

        AudioMan.inst.PlayArcane();

        mySpace.addUnit(this);
        StartCoroutine(waitToAct(0.001F));
    }

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
