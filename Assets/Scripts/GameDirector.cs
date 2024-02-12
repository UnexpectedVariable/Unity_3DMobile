using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    [SerializeField]
    private PlayerController _playerController = null;
    [SerializeField]
    private CameraMovementController _cameraController = null;

    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _playerController.Attach(_cameraController);
    }
}
