using UnityEngine;
using UnityEngine.EventSystems;

public class CheckerRaycast : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private TapToInstantiateInAR _tapToInstantiateInAR;

    public void OnPointerDown(PointerEventData eventData)
    {
        _tapToInstantiateInAR.SpawnWarriorOnPlacementIndicatorPosition();
    }
}