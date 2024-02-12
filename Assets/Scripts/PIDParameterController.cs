using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PIDParameterController : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _pGainInputField = null;
    [SerializeField]
    private TMP_InputField _dGainInputField = null;
    [SerializeField]
    private TMP_InputField _iGainInputField = null;

    private PIDController _controller;

    public PIDController Controller
    {
        get => _controller;
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _controller = new PIDController(float.Parse(_pGainInputField.text), float.Parse(_iGainInputField.text), float.Parse(_dGainInputField.text), 20, DerivativeMeasurement.Velocity);
        _controller.DerivativeMeasurement = DerivativeMeasurement.Velocity;

        _pGainInputField.onValueChanged.AddListener(PGainUpdated);
        _dGainInputField.onValueChanged.AddListener(DGainUpdated);
        _iGainInputField.onValueChanged.AddListener(IGainUpdated);
    }

    private void PGainUpdated(string gain)
    {
        _controller.ProportionalGain = float.Parse(gain);
    }

    private void DGainUpdated(string gain)
    {
        _controller.DerivativeGain = float.Parse(gain);
    }

    private void IGainUpdated(string gain) 
    {
        _controller.IntegralGain = float.Parse(gain);
    }

    public void Update()
    {
        Report();
    }

    private void Report()
    {
        Vector3 parameters = _controller.GetParameters();
        Debug.LogWarning($"PID parameters are ({parameters.x}; {parameters.y}; {parameters.z})");
    }
}
