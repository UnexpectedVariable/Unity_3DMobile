using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody = null;
    [SerializeField] 
    private PlayerInput _playerInput;
    [SerializeField]
    private float _speedMultiplier = 1.0f;
    [SerializeField]
    private GameObject _joystick = null;

    void Update()
    {
        
    }

    private void OnMove(InputValue value)
    {
        Debug.Log("On Move");
        Vector3 inputVector = value.Get<Vector2>();
        inputVector.z = inputVector.y;
        inputVector.y = 0;
        _rigidbody.velocity = inputVector * _speedMultiplier;
    }
}
