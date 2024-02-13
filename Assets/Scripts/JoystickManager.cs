using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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
        _joystick.SetActive(true);
        _joystick.transform.position = Mouse.current.position.ReadValue();
    }

    private void OnLMBReleased(InputValue value)
    {
        //Debug.Log("OnRelease triggered");
        _joystick.SetActive(false);
    }
}
