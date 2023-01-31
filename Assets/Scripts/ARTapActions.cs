using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
public class ARTapActions : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject _placementIndicator;
    [SerializeField] private GameObject _spawnPosition;
    [SerializeField] private ARRaycastManager _arRaycastManager;
    [SerializeField] private UIController _uIController;
    [SerializeField] private TouchController _touchController;

    private Vector2 _touchPosition;
    private Pose _placementPosition;
    private bool _placementPositionIsValid;
    private bool _touchIsPressed;
    private bool _touchIsReleased;

    private void Awake()
    {
        _touchController.TouchPositionChangedPressedOrReleased += OnTouchPositionChangedPressedOrReleased;
    }

    private void OnTouchPositionChangedPressedOrReleased(Vector2 currentTouchPosition, bool isPressed, bool isReleased)
    {
        _touchPosition = currentTouchPosition;
        _touchIsPressed = isPressed;
        _touchIsReleased = isReleased;
    }

    private void Update()
    {
        if (_gameManager.CurrentGameState != GameManager.GameState.None)
        {
            _spawnPosition.SetActive(true);

            if (_touchIsPressed)
            {
                UpdatePlacementPosition();
                UpdatePlacementIndicator();
            }

            if (_touchIsReleased)
            {
                UpdatePlacementPosition();
                UpdatePlacementIndicator();
                InstantiateOrHealUpWarrior();
                _touchIsPressed = false;
                _touchIsReleased = false;
            }
        }
        else
        {
            _spawnPosition.SetActive(false);
        }
    }

    private void UpdatePlacementPosition()
    {
        var hits = new List<ARRaycastHit>();
        _arRaycastManager.Raycast(_touchPosition, hits, TrackableType.PlaneWithinPolygon);

        _placementPositionIsValid = hits.Count > 0;

        if (_placementPositionIsValid)
        {
            _placementPosition = hits[0].pose;
            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            _placementPosition.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }

    private void UpdatePlacementIndicator()
    {
        if (_placementPositionIsValid)
        {
            _placementIndicator.SetActive(true);
            _placementIndicator.transform.SetPositionAndRotation(_placementPosition.position, _placementPosition.rotation);
        }
        else
        {
            _placementIndicator.SetActive(false);
        }
    }

    private void InstantiateOrHealUpWarrior()
    {
        Ray ray = Camera.main.ScreenPointToRay(_touchPosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            var warrior = hitInfo.collider.GetComponent<Warrior>();
            if (warrior != null)
            {
                warrior.UnitStats.HealUp();
                warrior.JumpOnHealUp();
                _uIController.ShowMessage("heal up!");
            }
            else
            {
                SpawnWarriorOnPlacementIndicatorPosition();
            }
        }
    }

    public void SpawnWarriorOnPlacementIndicatorPosition()
    {
        if (_placementPositionIsValid && _gameManager.CurrentGameState != GameManager.GameState.None)
        {
            _gameManager.InstantiateWarrior(_placementPosition.position, _placementPosition.rotation);
        }
        else if (!_placementPositionIsValid)
        {
            _uIController.ShowMessage("not valid");
        }
    }

    private void OnDestroy()
    {
        _touchController.TouchPositionChangedPressedOrReleased -= OnTouchPositionChangedPressedOrReleased;
    }
}