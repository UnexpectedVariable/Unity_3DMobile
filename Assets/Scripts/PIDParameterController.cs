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
        _controller = new PIDController();
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
}
