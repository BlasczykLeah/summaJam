using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chungBehavior : unitBehavior
{
    int attacksSurvived = 0;
    bool inBattle = false;
    int healthCompare;

    public override void objectSpawned(spaceComponent startingPoint, bool isPlayer)
    {
        maxHealth = health;
        healthCompare = health;

        myHealthBar = Instantiate(healthBar, GameObject.Find("Canvas").transform);
        Vector2 barSpot = Camera.main.WorldToScreenPoint(transform.position);
        myHealthBar.transform.position = new Vector2(barSpot.x, barSpot.y + barOffset);

        playerUnit = isPlayer;
        mySpace = startingPoint;

        AudioMan.inst.PlayChungus();

        mySpace.addUnit(this);
        StartCoroutine(waitToAct(0.001F));
    }

    protected override void Update()
    {
        base.Update();

        if(healthCompare > health)
        {
            inBattle = true;
            healthCompare = health;
        }
    }

    protected override void act()
    {
        //move or attack
        spaceComponent nextSpace = mySpace.chooseSpace(playerUnit);
        if (!nextSpace)
        {
            //Debug.Log("Unable to move, wating for next turn.");
            return;
        }
        else if (nextSpace == mySpace)
        {
            AudioMan.inst.PlayHit();

            // player takes damage
            if (playerUnit)
            {
                GameObject.Find("EnemyObject").GetComponent<playerManager>().health--;
                if (evolved) GameObject.Find("EnemyObject").GetComponent<playerManager>().health--;
            }
            else
            {
                GameObject.Find("PlayerObject").GetComponent<playerManager>().health--;
                if (evolved) GameObject.Find("PlayerObject").GetComponent<playerManager>().health--;
            }

            mySpace.removeUnit();
            Destroy(myHealthBar);
            Destroy(gameObject);
            return;
        }

        bool killed = false;
        if (nextSpace.unit)
        {
            //Debug.Log("Attacking!");

            if (nextSpace.unit.takeDamage(damage))
            {
                killed = true;
                nextSpace.removeUnit();
            }
        }
        if (!nextSpace.unit || killed)
        {
            //Debug.Log("Moving to a new space");
            if (inBattle)
            {
                inBattle = false;
                attacksSurvived++;
            }

            mySpace.removeUnit();
            mySpace = nextSpace;
            nextSpace.addUnit(this);

            // move the dude
            GetComponent<Animator>().SetTrigger("Jump");
            location = nextSpace.transform.position;
            Invoke("setMove", 0.2F);
        }
    }

    protected override bool evolveCondition()
    {
        if (attacksSurvived >= 2) return true;
        else return false;
    }
}
