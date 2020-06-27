using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawn : MonoBehaviour
{
    int myChoice;

    public GameObject[] units;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void chooseNewChoice()
    {
        myChoice = Random.Range(0, units.Length);
    }
}
