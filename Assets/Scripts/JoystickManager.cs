using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoystickManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _joystick = null;

    private void OnLMBPressed(InputValue value)
    {
        //Debug.Log("OnClick triggered!");
        _joystick.SetActive(true);
        _joystick.transform.position = Mouse.current.position.ReadValue();
    }

    private void OnLMBReleased(InputValue value)
    {
        //Debug.Log("OnRelease triggered");
        _joystick.SetActive(false);
    }
}
