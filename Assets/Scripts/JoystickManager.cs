using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class JoystickManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _joystick = null;

    private void Start()
    {
        Vector2 screen2D = Camera.main.ScreenToWorldPoint(new Vector3(Screen.height, Screen.width, Camera.main.nearClipPlane));
        RectTransform joystickRect = (RectTransform)_joystick.transform;
        _joystick.transform.position = screen2D - joystickRect.rect.size;
        //joystick.gameObject.SetActive(false);
    }

    private void OnLMBPressed(InputValue value)
    {
        //Debug.Log("OnClick triggered!");
        EnableAtPosition(Mouse.current.position.ReadValue());
    }

    private void OnLMBReleased(InputValue value)
    {
        //Debug.Log("OnRelease triggered");
        _joystick.SetActive(false);
    }

    private void OnTouchInitiated(InputValue value)
    {
        //Debug.Log($"Touch initiated input action invoked!");
        EnableAtPosition(value.Get<Vector2>());
    }

    private void OnTouchReleased(InputValue value)
    {
        //Debug.Log($"Touch released input action invoked!");
        _joystick?.SetActive(false);
    }

    private void EnableAtPosition(Vector2 position)
    {
        _joystick.SetActive(true);
        _joystick.transform.position = position;
    }
}
