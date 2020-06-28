using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thiefBehavior : unitBehavior
{
    int moneyEarned = 0;
    int currentMoney;
    playerManager myManager;

    public override void objectSpawned(spaceComponent startingPoint, bool isPlayer)
    {
        maxHealth = health;

        myHealthBar = Instantiate(healthBar, GameObject.Find("Canvas").transform);
        Vector2 barSpot = Camera.main.WorldToScreenPoint(transform.position);
        myHealthBar.transform.position = new Vector2(barSpot.x, barSpot.y + barOffset);

        playerUnit = isPlayer;
        mySpace = startingPoint;

        if (playerUnit) myManager = GameObject.Find("PlayerObject").GetComponent<playerManager>();
        else myManager = GameObject.Find("EnemyObject").GetComponent<playerManager>();
        currentMoney = myManager.money;

        AudioMan.inst.PlayThief();

        mySpace.addUnit(this);
        StartCoroutine(waitToAct(speed));
    }

    protected override void Update()
    {
        base.Update();

        if(currentMoney != myManager.money)
        {
            if (currentMoney > myManager.money) currentMoney = myManager.money;
            else
            {
                moneyEarned += (myManager.money - currentMoney);
                currentMoney = myManager.money;
            }
        }
    }

    protected override bool evolveCondition()
    {
        if (moneyEarned >= 20) return true;
        else return false;
    }
}
