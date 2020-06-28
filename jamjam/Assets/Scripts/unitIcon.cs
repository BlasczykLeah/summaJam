using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class unitIcon : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public playerManager myPlayer;

    public GameObject myUnitPrefab;
    public GameObject myIcon;

    public Image fillImage;
    public Image costImage;
    bool costImageEnabled = true;

    [SerializeField]
    bool buyCooldown = false;    // buying cooldown

    [SerializeField]
    bool purchaseAllowed = false;    // all buying conditions correct

    public int cost;
    float cooldown, cdReset;

    [Header("Help Box")]
    public Sprite sprite;
    public string unitString;
    public string cooldownString;
    public string healthString;
    public string damageString;
    public string speedString;
    public string evolveString;

    public GameObject box;  // box children:    1 = image, 2 = name, 3 = cost, 4 = health, 5 = damage, 6 = speed, 7 = evolve

    void Start()
    {
        cost = myUnitPrefab.transform.GetChild(0).GetComponent<unitBehavior>().cost;
        cooldown = cdReset = myUnitPrefab.transform.GetChild(0).GetComponent<unitBehavior>().cooldown;
        fillImage.gameObject.SetActive(false);
        costImage.gameObject.SetActive(true);
    }

    void Update()
    {
        if (buyCooldown)
        {
            // do cooldown things
            cooldown -= Time.deltaTime;
            if (cooldown < 0) cooldown = 0;
            fillImage.fillAmount = cooldown / cdReset;
            if(cooldown <= 0)
            {
                // reset things
                buyCooldown = false;
                fillImage.gameObject.SetActive(false);
                cooldown = cdReset;
            }
        }
    }

    public void checkPrices(int money)
    {
        if(costImageEnabled && money >= cost)
        {
            costImageEnabled = false;
            costImage.gameObject.SetActive(false);
        } 
        if(!costImageEnabled && money < cost)
        {
            costImageEnabled = true;
            costImage.gameObject.SetActive(true);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // check if can buy (has money && !buyCoolDown)
        if(!buyCooldown && myPlayer.money >= cost)
        {
            purchaseAllowed = true;
            myIcon.SetActive(true);
            myPlayer.showStartingPos(true);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // if passed buy (enable some bool) drag an icon
        if (purchaseAllowed)
        {
            myIcon.transform.position = Input.mousePosition;
            box.SetActive(false);
        }
        else
        {
            Debug.Log("Unable to purchase.");
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // if dropped on starting point (and can buy), buy and spawn a dude

        if (purchaseAllowed)
        {
            // get starting point
            spaceComponent start = myPlayer.buyAUnit(cost);
            if (start)
            {
                // place dude
                GameObject newUnit = Instantiate(myUnitPrefab, start.transform.position, Quaternion.identity);
                //start.addUnit(newUnit.GetComponent<unitBehavior>());
                newUnit.transform.GetChild(0).GetComponent<unitBehavior>().objectSpawned(start, true);

                buyCooldown = true;
                fillImage.gameObject.SetActive(true);
            }

            myPlayer.showStartingPos(false);
            myIcon.SetActive(false);
            purchaseAllowed = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        box.SetActive(true);

        // box children:    1 = image, 2 = name, 3 = cost, 4 = health, 5 = damage, 6 = speed, 7 = evolve
        if (box.transform.GetChild(1).GetComponent<Image>().sprite != sprite)
        {
            box.transform.GetChild(1).GetComponent<Image>().sprite = sprite;
            box.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = unitString;
            box.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = cooldownString;
            box.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = healthString;
            box.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = damageString;
            box.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = speedString;
            box.transform.GetChild(7).GetComponent<TextMeshProUGUI>().text = evolveString;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        box.SetActive(false);
    }
}
