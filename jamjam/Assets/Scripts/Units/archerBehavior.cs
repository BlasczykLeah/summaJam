using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class archerBehavior : unitBehavior
{
    protected override void act()
    {
        //move or attack
        spaceComponent nextSpace = mySpace.choseSpaceWithAheads(playerUnit);
        if (!nextSpace)
        {
            return;
        }
        else if (nextSpace == mySpace)
        {
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

            AudioMan.inst.PlayHit();

            mySpace.removeUnit();
            Destroy(myHealthBar);
            Destroy(gameObject);
            return;
        }

        if (nextSpace.unit)
        {
            if (nextSpace.unit.takeDamage(damage))
            {
                nextSpace.removeUnit();
            }
        }
        else
        {
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
        return false;
    }
}
