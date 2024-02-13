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
        args.GO.SetActive(true);
        Vector3 targetPosition = args.GO.transform.position;
        targetPosition.y = args.GO.transform.lossyScale.y * 2;
        _transposer.Transpose(args.GO, targetPosition);
    }
}
