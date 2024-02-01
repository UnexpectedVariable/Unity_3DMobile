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

    void Update()
    {
        //Vector2 moveVec = _playerInput.actions["Move"].ReadValue<Vector2>();
    }

    private void OnMove(InputValue value)
    {
        _rigidbody.velocity = value.Get<Vector2>();
    }
}
