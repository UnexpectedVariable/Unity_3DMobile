using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Rigidbody _rigidbody = null;
    [SerializeField] 
    private PlayerInput _playerInput;
    [SerializeField]
    private GameObject _joystick = null;
    [SerializeField]
    private Transform _cameraTransform = null;
    [SerializeField]
    private Vector3 _cameraOffset = Vector3.zero;
    [SerializeField]
    private float _speedMultiplier = 1.0f;
    [SerializeField]
    private float _targetVelocity = 1f;

    [SerializeField]
    private PIDParameterController _parameterController = null;
    private PIDController _pidController = null;

    private void Start()
    {
        _pidController = _parameterController.Controller;
        _rigidbody.maxAngularVelocity = 0;
    }

    private void FixedUpdate()
    {
        _cameraTransform.position = gameObject.transform.position + _cameraOffset;
        Vector3 inputVector = _playerInput.currentActionMap["Move"].ReadValue<Vector2>();
        _animator.SetFloat("Velocity", inputVector.sqrMagnitude);
        float throttle = 0f;
        if (_playerInput.currentActionMap["Move"].IsPressed())
        {
            _rigidbody.drag = 0;
            throttle = _pidController.Update(Time.fixedDeltaTime, _rigidbody.velocity.sqrMagnitude, _targetVelocity * _targetVelocity);
        }
        else
        {
            _rigidbody.drag = 100;
        }
        inputVector.z = inputVector.y;
        inputVector.y = 0;
        Move(inputVector.normalized * Mathf.Sqrt(throttle));
    }

    private bool IsOnGround(out RaycastHit hit)
    {
        Debug.DrawRay(gameObject.transform.position, Vector3.down, Color.red);
        return Physics.Raycast(gameObject.transform.position, Vector3.down, out hit, 1f);
    }

    private void Move(Vector3 inputVector)
    {
        Debug.Log($"Fixed delta time is {Time.fixedDeltaTime}");
        Debug.Log($"input magnitude {inputVector.sqrMagnitude}");
        //_rigidbody.AddForce(inputVector * _speedMultiplier * Time.fixedDeltaTime, ForceMode.Force);
        _rigidbody.AddForce(inputVector, ForceMode.Force);

        if (inputVector == Vector3.zero) return;
        _rigidbody.MoveRotation(Quaternion.LookRotation(inputVector * Time.fixedDeltaTime, Vector3.up));
    }
}
