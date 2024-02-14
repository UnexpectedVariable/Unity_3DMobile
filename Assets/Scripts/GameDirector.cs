using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    [SerializeField]
    private PlayerController _playerController = null;
    [SerializeField]
    private CameraMovementController _cameraController = null;

    [SerializeField]
    private List<TerrainLock> _locks = null;
    [SerializeField]
    private ObjectTransposer _transposer = null;
    [SerializeField]
    private GameObject _water = null;

    void Start()
    {
        _locks = new List<TerrainLock>(Object.FindObjectsByType<TerrainLock>(FindObjectsInactive.Include, FindObjectsSortMode.None));

        Initialize();
    }

    private void Initialize()
    {
        foreach(var terrainLock in _locks)
        {
            terrainLock.TerrainUnlockedEvent += HandleTerrainUnlockedEvent;
        }

        _playerController.Attach(_cameraController);

        _cameraController.SetPositionWithOffset(_playerController.transform.position);
    }

    private void HandleTerrainUnlockedEvent(object sender, GameObjectEventArgs args)
    {
        foreach (var GO in args.GOs)
        {
            GO.SetActive(true);
            Vector3 targetPosition = GO.transform.position;
            GO.transform.position = new Vector3(GO.transform.position.x, _water.transform.position.y, GO.transform.position.z);
            _transposer.Transpose(GO, targetPosition);
        }
    }
}
