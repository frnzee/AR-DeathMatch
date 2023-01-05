using UnityEngine;
using UnityEngine.EventSystems;

public class CheckerRaycast : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private AR_TapToInstantiate _arTapToInstantiate;

    public void OnPointerDown(PointerEventData eventData)
    {
        _arTapToInstantiate.SpawnWarriorOnPlacementIndicatorPosition();
    }
}