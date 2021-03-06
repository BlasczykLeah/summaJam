﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class unitBehavior : MonoBehaviour
{
    [Header("Unit Stats")]
    public bool playerUnit;

    public GameObject healthBar;
    protected GameObject myHealthBar;
    public float barOffset;

    public GameObject particles;

    public int health;
    protected int maxHealth;

    public int damage;

    [Tooltip("Amount of time(seconds) between moving spaces.")]
    public float speed;

    [Tooltip("Amount of time(seconds) before can be spawned again.")]
    public float cooldown;

    [Tooltip("Cost of unit. (gold?)")]
    public int cost;

    protected bool moving = false;
    protected Vector3 location;

    protected bool evolved = false;
    protected spaceComponent mySpace;

    public virtual void objectSpawned(spaceComponent startingPoint, bool isPlayer)
    {
        maxHealth = health;

        myHealthBar = Instantiate(healthBar, GameObject.Find("Canvas").transform);
        Vector2 barSpot = Camera.main.WorldToScreenPoint(transform.position);
        myHealthBar.transform.position = new Vector2(barSpot.x, barSpot.y + barOffset);

        playerUnit = isPlayer;
        mySpace = startingPoint;

        mySpace.addUnit(this);
        StartCoroutine(waitToAct(0.001F));
    }

    protected virtual void Update()
    {
        if (!evolved) if(evolveCondition()) evolve();

        if (moving)
        {
            transform.parent.position = Vector3.MoveTowards(transform.parent.position, location, Time.deltaTime * 2);

            Vector3 barSpot = Camera.main.WorldToScreenPoint(transform.position);
            myHealthBar.transform.position = new Vector3(barSpot.x, barSpot.y + barOffset, barSpot.z);

            if (Vector2.Distance(transform.parent.position, location) < 0.001F)
            {
                moving = false;
            }
        }
    }

    protected IEnumerator waitToAct(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        act();
        StartCoroutine(waitToAct(speed));
    }

    protected virtual void act()
    {
        //move or attack
        spaceComponent nextSpace = mySpace.chooseSpace(playerUnit);
        if (!nextSpace)
        {
            //Debug.Log("Unable to move, wating for next turn.");
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

            AudioMan.inst.PlayHit();

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
        if(!nextSpace.unit || killed)
        {
            //Debug.Log("Moving to a new space");

            mySpace.removeUnit();
            mySpace = nextSpace;
            nextSpace.addUnit(this);

            // move the dude
            GetComponent<Animator>().SetTrigger("Jump");
            location = nextSpace.transform.position;
            Invoke("setMove", 0.2F);
        }
    }

    protected void setMove() { moving = true; }

    public bool takeDamage(int dmg)
    {
        AudioMan.inst.PlaySword();

        health -= dmg;
        myHealthBar.transform.GetChild(0).GetComponent<Image>().fillAmount = (float)health / (float)maxHealth;

        if (health <= 0)
        {
            //Debug.Log("DEAD");
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
            GameObject.Find("EnemyObject").GetComponent<playerManager>().money += (cost/ 5);
            if (evolved) GameObject.Find("EnemyObject").GetComponent<playerManager>().money += (cost / 5);
        }
        else
        {
            GameObject.Find("PlayerObject").GetComponent<playerManager>().money += (cost / 5);
            if (evolved) GameObject.Find("PlayerObject").GetComponent<playerManager>().money += (cost / 5);
        }

        Destroy(myHealthBar);
        Destroy(gameObject);
    }

    public void evolve()
    {
        evolved = true;

        AudioMan.inst.PlayEvolve();

        transform.GetChild(0).gameObject.SetActive(true);
        Instantiate(particles, transform.GetChild(1).position, Quaternion.identity);

        health = maxHealth;
        myHealthBar.transform.GetChild(0).GetComponent<Image>().fillAmount = 1;

        cost *= 2;
        speed /= 1.5F;
        damage *= 2;
    }

    protected virtual bool evolveCondition()
    {
        return false;
    }

    public void gameOver()
    {
        Destroy(myHealthBar);
        Destroy(gameObject);
    }
}
