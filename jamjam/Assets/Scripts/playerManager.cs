using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{
    public int health;

    public int money;

    public unitIcon[] unitShop;

    public startingPlace[] startingPlacesRaycasts;

    private void Start()
    {
        InvokeRepeating("endlessMoney", 0, 1);
    }

    void endlessMoney()
    {
        money++;
    }

    public spaceComponent buyAUnit(int price)
    {
        for(int i = 0; i < startingPlacesRaycasts.Length; i++)
        {
            if (startingPlacesRaycasts[i].hovered && !startingPlacesRaycasts[i].mySpace.unit)
            {
                money -= price;
                return startingPlacesRaycasts[i].mySpace;
            }
        }

        Debug.Log("No starting points found.");
        return null;
    }

    public void showStartingPos(bool show)
    {
        foreach(startingPlace a in startingPlacesRaycasts)
        {
            if (show) a.GetComponent<Animator>().SetTrigger("On");
            else a.GetComponent<Animator>().SetTrigger("Off");
        }
    }
}
