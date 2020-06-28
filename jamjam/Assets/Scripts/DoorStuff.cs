using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorStuff : MonoBehaviour
{
    public GameObject innerds, buttons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOnInsideStuff()
    {
        innerds.SetActive(true);
    }

    public void TurnOnMain()
    {
        buttons.SetActive(true);
    }
}
