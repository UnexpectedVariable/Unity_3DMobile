using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour, IObservedGO
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Rigidbody _rigidbody = null;
    [SerializeField] 
    private PlayerInput _playerInput;
    [SerializeField]
    private float _targetVelocity = 1f;

    [SerializeField]
    private PIDParameterController _parameterController = null;
    private PIDController _pidController = null;

    private List<IGOObserver> _observers = new List<IGOObserver>();

    private void Start()
    {
        //Change where this is done later
        Physics.gravity *= 2;

        _pidController = _parameterController.Controller;
        _rigidbody.maxAngularVelocity = 0;
    }

    private void FixedUpdate()
    {
        if(_rigidbody.velocity.sqrMagnitude > 0)
        {
            Notify();
        }
        
        Vector3 inputVector = _playerInput.currentActionMap["Move"].ReadValue<Vector2>();
        inputVector.z = inputVector.y;
        inputVector.y = 0;
        _animator.SetFloat("Velocity", inputVector.sqrMagnitude);
        float throttle = 0f;
        if (_playerInput.currentActionMap["Move"].IsPressed())
        {
            if(IsInfrontCollider(out RaycastHit hit))
            {
                Debug.Log($"{hit.collider.name} hit, tag: {hit.collider.tag}");
                if (hit.collider.tag == "Terrain")
                {
                    Vector3 targetPos = new Vector3(transform.position.x, hit.transform.position.y, transform.position.z);
                    Debug.DrawRay(gameObject.transform.position, hit.normal, Color.yellow, Mathf.Infinity);
                    _rigidbody.MovePosition(targetPos);
                }
            }
            throttle = _pidController.Update(Time.fixedDeltaTime, _rigidbody.velocity.magnitude, _targetVelocity);
        }
        Move(inputVector.normalized * throttle);

        if (inputVector == Vector3.zero) return;
        Rotate(Quaternion.LookRotation(inputVector * Time.fixedDeltaTime, Vector3.up));
    }

    private bool IsOnGround(out RaycastHit hit)
    {
        Debug.DrawRay(gameObject.transform.position, Vector3.down, Color.red);
        return Physics.Raycast(gameObject.transform.position + new Vector3(0, gameObject.transform.localScale.y * 0.5f, 0), Vector3.down, out hit, gameObject.transform.localScale.y);
    }

    private bool IsInfrontCollider(out RaycastHit hit)
    {
        Debug.DrawRay(gameObject.transform.position, transform.forward, Color.red, Time.fixedDeltaTime, false);
        return Physics.Raycast(gameObject.transform.position, transform.forward, out hit, 0.5f);
    }

    private void Move(Vector3 inputVector)
    {
        _rigidbody.AddForce(inputVector, ForceMode.Force);
    }

    private void Rotate(Quaternion inputQuaternion)
    {
        _rigidbody.MoveRotation(inputQuaternion);
    }

    public void Attach(IGOObserver observer)
    {
        _observers.Add(observer);
    }

    public void Detach(IGOObserver observer)
    {
        _observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (var observer in _observers)
        {
            observer.Update(gameObject);
        }
    }
}
