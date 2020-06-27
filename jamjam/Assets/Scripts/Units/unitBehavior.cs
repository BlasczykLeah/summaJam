using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitBehavior : MonoBehaviour, IEvolveable
{
    [Header("Unit Stats")]
    public int health;
    protected int maxHealth;

    public int damage;

    [Tooltip("Amount of time(seconds) between moving spaces.")]
    public float speed;

    [Tooltip("Amount of time(seconds) before can be spawned again.")]
    public float cooldown;

    [Tooltip("Cost of unit. (gold?)")]
    public int cost;

    bool evolved = false;
    spaceComponent mySpace;

    public void objectSpawned(spaceComponent startingPoint)
    {
        mySpace = startingPoint;
        StartCoroutine(waitToAct(speed));
    }

    void Update()
    {
        if (evolveCondition() && !evolved) evolve();
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
    }

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
        // does something
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
