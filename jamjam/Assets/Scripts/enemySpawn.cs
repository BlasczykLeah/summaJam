using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawn : MonoBehaviour
{
    int myChoice;
    playerManager manager;

    public GameObject[] units;
    bool[] spawnCooldowns;
    float[] cooldowns;

    public spaceComponent[] mySpawns;

    // Start is called before the first frame update
    void Start()
    {
        spawnCooldowns = new bool[units.Length];
        for (int i = 0; i < spawnCooldowns.Length; i++) spawnCooldowns[i] = false;
        cooldowns = new float[units.Length];

        myChoice = 0;
        manager = GetComponent<playerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < units.Length; i++)
        {
            if (spawnCooldowns[i])
            {
                cooldowns[i] -= Time.deltaTime;
                if(cooldowns[i] <= 0)
                {
                    spawnCooldowns[i] = false;
                }
            }
        }
    }

    public void tryToSpawn()
    {
        if(manager.money >= units[myChoice].transform.GetChild(0).GetComponent<unitBehavior>().cost)
        {
            // spawwwwwwwwwwwn it
            spaceComponent spawn = mySpawns[Random.Range(0, mySpawns.Length)];
            while(spawn.unit) spawn = mySpawns[Random.Range(0, mySpawns.Length)];

            GameObject unit = Instantiate(units[myChoice], spawn.transform.position, Quaternion.identity);
            unit.transform.GetChild(0).GetComponent<unitBehavior>().objectSpawned(spawn, false);

            cooldowns[myChoice] = unit.transform.GetChild(0).GetComponent<unitBehavior>().cooldown;
            spawnCooldowns[myChoice] = true;

            manager.money -= units[myChoice].transform.GetChild(0).GetComponent<unitBehavior>().cost;

            chooseNewChoice();
        }
    }

    void chooseNewChoice()
    {
        myChoice = Random.Range(0, units.Length);
    }
}
