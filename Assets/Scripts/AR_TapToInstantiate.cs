using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;

public class AR_TapToInstantiate : MonoBehaviour

{
    private const float PositionValue = 0.5f;

    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject _placementIndicator;
    [SerializeField] private GameObject _spawnPosition;
    [SerializeField] private ARRaycastManager _arRaycastManager;

    private Pose _placementPosition;
    private bool _placementPositionIsValid = false;

    private void Update()
    {
        if (_gameManager.CurrentGameState == GameManager.GameState.Setup)
        {
            _spawnPosition.SetActive(true);

            UpdatePlacementPose();
            UpdatePlacementIndicator();
        }
        else
        {
            _spawnPosition.SetActive(false);
        }
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(PositionValue, PositionValue));
        var hits = new List<ARRaycastHit>();
        _arRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

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

    public void SpawnWarriorOnPlacementIndicatorPosition()
    {
        if (_placementPositionIsValid && _gameManager.CurrentGameState == GameManager.GameState.Setup)
        {
            _gameManager.InstantiateWarrior(_placementPosition.position, _placementPosition.rotation);
        }
    }
}