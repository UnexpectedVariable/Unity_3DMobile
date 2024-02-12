using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementController : MonoBehaviour, IGOObserver
{
    [SerializeField]
    private float _speed = 1.0f;
    [SerializeField]
    private Vector3 _cameraOffset = Vector3.zero;

    private Vector3 _targetPosition = Vector3.zero;
    public Vector3 TargetPosition
    {
        set => _targetPosition = value;
    }

    private void Update()
    {
        float delta = _speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, delta);
    }

    void IGOObserver.Update(GameObject observed)
    {
        _targetPosition = observed.transform.position + _cameraOffset;
        Debug.Log($"Camera target position changed to {_targetPosition}");
    }
}
