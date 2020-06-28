using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorStuff : MonoBehaviour
{
    public GameObject innerds, buttons;

    public void TurnOnInsideStuff()
    {
        innerds.SetActive(true);
    }

    public void TurnOnMain()
    {
        buttons.SetActive(true);
    }

    public void slingshot()
    {
        Camera.main.GetComponent<Animator>().SetTrigger("Go");
    }
}
