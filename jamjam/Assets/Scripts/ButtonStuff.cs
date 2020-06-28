using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonStuff : MonoBehaviour
{
    public GameObject buttons, inside, gameDesc, knight, thief, chungus, arcanist, credits;
    public Animator doorAnim;

    public void OpenTheGate()
    {
        buttons.SetActive(false);
        gameDesc.SetActive(true);
        knight.SetActive(false);
        thief.SetActive(false);
        chungus.SetActive(false);
        arcanist.SetActive(false);
        credits.SetActive(false);
        doorAnim.SetTrigger("Open");
    }

    public void CloseTheGate()
    {
        inside.SetActive(false);
        doorAnim.SetTrigger("Close");
    }

    public void OpenKnight()
    {
        gameDesc.SetActive(false);
        knight.SetActive(true);
    }

    public void OpenThief()
    {
        knight.SetActive(false);
        thief.SetActive(true);
    }

    public void OpenChungus()
    {
        thief.SetActive(false);
        chungus.SetActive(true);
    }

    public void OpenArcanist()
    {
        chungus.SetActive(false);
        arcanist.SetActive(true);
    }

    public void OpenCredits()
    {
        arcanist.SetActive(false);
        credits.SetActive(true);
    }
}
