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

    private void Start()
    {
        //if (!player) Invoke("startAI", 5);
        InvokeRepeating("endlessMoney", 5, 1);
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

        if (health <= 0) lose();
    }

    void startAI() { GetComponent<enemySpawn>().enabled = true; }

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
            FindObjectOfType<enemySpawn>().enabled = false;
            moneyText.gameObject.SetActive(false);
            healthText.gameObject.SetActive(false);

            GameObject.Find("EnemyObject").GetComponent<playerManager>().moneyTextE.gameObject.SetActive(false);
            GameObject.Find("EnemyObject").GetComponent<playerManager>().healthTextE.gameObject.SetActive(false);
        }
        else
        {
            GetComponent<enemySpawn>().enabled = false;
            moneyTextE.gameObject.SetActive(false);
            healthTextE.gameObject.SetActive(false);

            GameObject.Find("PlayerObject").GetComponent<playerManager>().moneyText.gameObject.SetActive(false);
            GameObject.Find("PlayerObject").GetComponent<playerManager>().healthText.gameObject.SetActive(false);
        }

        foreach (unitIcon a in unitShop) a.enabled = false;

        foreach (unitBehavior a in FindObjectsOfType<unitBehavior>()) a.gameOver();

        myWinScreen.SetActive(true);
        StartCoroutine(enableButtons(2));
    }

    IEnumerator enableButtons(float time)
    {
        yield return new WaitForSecondsRealtime(2);
        buttons.SetActive(true);
    }
}
