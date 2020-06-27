using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitBehavior : MonoBehaviour, IEvolveable
{
    [Header("Unit Stats")]
    public bool playerUnit;

    public int health;
    protected int maxHealth;

    public int damage;

    [Tooltip("Amount of time(seconds) between moving spaces.")]
    public float speed;

    [Tooltip("Amount of time(seconds) before can be spawned again.")]
    public float cooldown;

    [Tooltip("Cost of unit. (gold?)")]
    public int cost;

    bool moving = false;
    Vector3 location;

    bool evolved = false;
    spaceComponent mySpace;

    public void objectSpawned(spaceComponent startingPoint, bool isPlayer)
    {
        playerUnit = isPlayer;
        mySpace = startingPoint;

        mySpace.addUnit(this);
        StartCoroutine(waitToAct(speed));
    }

    void Update()
    {
        if (evolveCondition() && !evolved) evolve();

        if (moving)
        {
            transform.parent.position = Vector3.MoveTowards(transform.parent.position, location, Time.deltaTime * 2);
            if(Vector2.Distance(transform.parent.position, location) < 0.001F)
            {
                moving = false;
            }
        }
    }

    IEnumerator waitToAct(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        act();
        StartCoroutine(waitToAct(seconds));
    }

    void act()
    {
        //move or attack
        spaceComponent nextSpace = mySpace.chooseSpace(playerUnit);
        if (!nextSpace)
        {
            Debug.Log("Unable to move, wating for next turn.");
            return;
        }
        else if(nextSpace == mySpace)
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

            mySpace.removeUnit();
            Destroy(gameObject);
            return;
        }

        bool killed = false;
        if (nextSpace.unit)
        {
            Debug.Log("Attacking!");

            if (nextSpace.unit.takeDamage(damage))
            {
                killed = true;
                nextSpace.removeUnit();
            }
        }
        if(!nextSpace.unit || killed)
        {
            Debug.Log("Moving to a new space");

            mySpace.removeUnit();
            mySpace = nextSpace;
            nextSpace.addUnit(this);

            // move the dude
            GetComponent<Animator>().SetTrigger("Jump");
            location = nextSpace.transform.position;
            Invoke("setMove", 0.2F);
        }
    }

    void setMove() { moving = true; }

    public bool takeDamage(int dmg)
    {
        health -= dmg;
        if(health <= 0)
        {
            Debug.Log("DEAD");
            dead();
            return true;
        }
        else
        {
            return false;
        }
    }

    void dead()
    {
        if (playerUnit)
        {
            GameObject.Find("EnemyObject").GetComponent<playerManager>().money += cost;
            if (evolved) GameObject.Find("EnemyObject").GetComponent<playerManager>().money += cost;
        }
        else
        {
            GameObject.Find("PlayerObject").GetComponent<playerManager>().money += cost;
            if (evolved) GameObject.Find("PlayerObject").GetComponent<playerManager>().money += cost;
        }

        Destroy(gameObject);
    }

    public virtual bool evolveCondition()
    {
        return false;
    }

    public virtual void evolve()
    {
        evolved = true;
        Debug.Log("I have evolved!");
    }
}
