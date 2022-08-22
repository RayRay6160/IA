using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverButtonGuestButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{   
    public RectTransform Button;

    void Start()
    {
        Button.GetComponent<Animator>().Play("HoverOff(GuestButton)");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Button.GetComponent<Animator>().Play("HoverOn(GuestButton)");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Button.GetComponent<Animator>().Play("HoverOff(GuestButton)");
    }
}
