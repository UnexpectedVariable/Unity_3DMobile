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

    private float _defaultDrag = 0f;

    private void Start()
    {
        //Change where this is done later
        Physics.gravity *= 2;

        _defaultDrag = _rigidbody.drag;
        _pidController = _parameterController.Controller;
        _rigidbody.maxAngularVelocity = 0;
    }

    private void FixedUpdate()
    {
        //Debug.Log($"Player velocity is : {_rigidbody. velocity.magnitude.ToString("0")}");
        //Debug.Log($"Player scale is: lossy {gameObject.transform.lossyScale}; local {gameObject.transform.localScale}");
        //Debug.LogWarning($"Current gravity is {Physics.gravity}");
        //Debug.Log($"Player euler angles: {gameObject.transform.rotation.eulerAngles}");
        //Debug.Log($"Is player in front of wall: {IsInfrontWall(out RaycastHit hit)}");
        _cameraTransform.position = gameObject.transform.position + _cameraOffset;
        Vector3 inputVector = _playerInput.currentActionMap["Move"].ReadValue<Vector2>();
        _animator.SetFloat("Velocity", inputVector.sqrMagnitude);
        float throttle = 0f;
        if (_playerInput.currentActionMap["Move"].IsPressed())
        {
            if(IsInfrontWall(out RaycastHit hit))
            {
                //_rigidbody.isKinematic = true;
                Vector3 targetPos = new Vector3(transform.position.x, hit.transform.position.y, transform.position.z);
                Debug.Log($"Wall collider transform: {hit.transform.position}");
                Debug.Log($"Wall gameobject transform: {hit.collider.gameObject.transform.position}");
                //Debug.Log($"Target position: {targetPos}");
                _rigidbody.MovePosition(targetPos);
            }
            _rigidbody.drag = _defaultDrag;
            throttle = _pidController.Update(Time.fixedDeltaTime, _rigidbody.velocity.magnitude, _targetVelocity);
            inputVector.z = inputVector.y;
            inputVector.y = 0;
        }
        else
        {
            
        }
        Move(inputVector.normalized * throttle);
    }

    private bool IsOnGround(out RaycastHit hit)
    {
        Debug.DrawRay(gameObject.transform.position, Vector3.down, Color.red);
        return Physics.Raycast(gameObject.transform.position + new Vector3(0, gameObject.transform.localScale.y * 0.5f, 0), Vector3.down, out hit, gameObject.transform.localScale.y);
    }

    private bool IsInfrontWall(out RaycastHit hit)
    {
        Debug.DrawRay(gameObject.transform.position + new Vector3(0, 0.1f, 0), transform.forward, Color.red, Time.fixedDeltaTime, false);
        return Physics.Raycast(gameObject.transform.position + new Vector3(0, 0.1f, 0), transform.forward, out hit, 0.5f);
    }

    private void Move(Vector3 inputVector)
    {
        //Debug.Log($"Fixed delta time is {Time.fixedDeltaTime}");
        //Debug.Log($"input magnitude {inputVector.sqrMagnitude}");
        _rigidbody.AddForce(inputVector, ForceMode.Force);

        if (inputVector == Vector3.zero) return;
        _rigidbody.MoveRotation(Quaternion.LookRotation(inputVector * Time.fixedDeltaTime, Vector3.up));
    }
}
