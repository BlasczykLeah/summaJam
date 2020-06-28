using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerManager : MonoBehaviour
{
    public bool player;

    public int health;

    public int money;

    public unitIcon[] unitShop;

    public startingPlace[] startingPlacesRaycasts;

    public GameObject myWinScreen;
    public GameObject buttons;

    [Header("Player Texts")]
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI healthText;

    [Header("Enemy Texts")]
    public TextMeshPro moneyTextE;
    public TextMeshPro healthTextE;

    bool end = false;

    private void Start()
    {
        InvokeRepeating("endlessMoney", 5, 0.7F);
    }

    private void Update()
    {
        if (player)
        {
            moneyText.text = "Money: " + money;
            healthText.text = "Health: " + health;
        }
        else
        {
            moneyTextE.text = "Money: " + money;
            healthTextE.text = "Health: " + health;
        }

        if (health <= 0 && !end)
        {
            end = true;
            lose();
        }
    }

    void endlessMoney()
    {
        money++;
        if (player) comparePrices();
        else GetComponent<enemySpawn>().tryToSpawn();
    }

    public spaceComponent buyAUnit(int price)
    {
        for(int i = 0; i < startingPlacesRaycasts.Length; i++)
        {
            if (startingPlacesRaycasts[i].hovered && !startingPlacesRaycasts[i].mySpace.unit)
            {
                money -= price;
                comparePrices();
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

    public void comparePrices()
    {
        foreach (unitIcon a in unitShop) a.checkPrices(money);
    }

    void lose()
    {
        CancelInvoke();

        if (player)
        {
            AudioMan.inst.StopSong();
            AudioMan.inst.PlayLose();
            StartCoroutine(enableButtons(7));

            FindObjectOfType<enemySpawn>().enabled = false;
            moneyText.gameObject.SetActive(false);
            healthText.gameObject.SetActive(false);

            GameObject.Find("EnemyObject").GetComponent<playerManager>().moneyTextE.gameObject.SetActive(false);
            GameObject.Find("EnemyObject").GetComponent<playerManager>().healthTextE.gameObject.SetActive(false);
        }
        else
        {
            AudioMan.inst.StopSong();
            AudioMan.inst.PlayWin();
            StartCoroutine(enableButtons(5));

            GetComponent<enemySpawn>().enabled = false;
            moneyTextE.gameObject.SetActive(false);
            healthTextE.gameObject.SetActive(false);

            GameObject.Find("PlayerObject").GetComponent<playerManager>().moneyText.gameObject.SetActive(false);
            GameObject.Find("PlayerObject").GetComponent<playerManager>().healthText.gameObject.SetActive(false);
        }

        foreach (unitIcon a in unitShop) a.enabled = false;

        foreach (unitBehavior a in FindObjectsOfType<unitBehavior>()) a.gameOver();

        myWinScreen.SetActive(true);
        //StartCoroutine(enableButtons(2));
    }

    IEnumerator enableButtons(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        buttons.SetActive(true);
        foreach (unitBehavior a in FindObjectsOfType<unitBehavior>()) a.gameOver();
    }
}
