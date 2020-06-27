using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class startingPlace : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public spaceComponent mySpace;

    public bool hovered = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovered = false;
    }
}
