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
    private float _speedMultiplier = 1.0f;
    [SerializeField]
    private GameObject _joystick = null;

    private void FixedUpdate()
    {
        Vector3 inputVector = _playerInput.currentActionMap["Move"].ReadValue<Vector2>();
        //if (inputVector == Vector3.zero) Debug.Log("inputVec is zero");

        inputVector.z = inputVector.y;
        inputVector.y = 0;
        _rigidbody.velocity = inputVector * _speedMultiplier;
        gameObject.transform.rotation = Quaternion.LookRotation(inputVector, Vector3.up);

        //_animator.SetBool("IsMoving", true);
        _animator.SetFloat("Velocity", _rigidbody.velocity.sqrMagnitude);
    }

    /*private void OnMove(InputValue value)
    {
        //Debug.Log("On Move");
        Vector3 inputVector = value.Get<Vector2>();
        inputVector.z = inputVector.y;
        inputVector.y = 0;
        _rigidbody.velocity = inputVector * _speedMultiplier;
    }*/
}
