using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public event Action<Vector2, bool, bool> TouchPositionChangedPressedOrReleased;

    public void OnPointerDown(PointerEventData eventData)
    {
        TouchPositionChangedPressedOrReleased?.Invoke(eventData.position, true, false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        TouchPositionChangedPressedOrReleased?.Invoke(eventData.position, true, false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        TouchPositionChangedPressedOrReleased?.Invoke(eventData.position, false, true);
    }
}