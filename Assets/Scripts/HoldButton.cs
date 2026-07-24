using UnityEngine;
using UnityEngine.EventSystems;

public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float direction;
    private bool isHeld;

    public void OnPointerDown(PointerEventData eventData)
    {
        isHeld = true;
    }

    public void OnPointerUp(PointerEventData eventData) 
    {
        isHeld = false;
    }

    public bool IsHeld => isHeld;

}
