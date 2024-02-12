using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PIDController
{

    private float _proportionalGain;
    private float _integralGain;
    private float _derivativeGain;

    private float _integralSaturation = 8;
    private DerivativeMeasurement _derivativeMeasurement;

    private float _valueLast;
    private float _errorLast;
    private float _integrationStored;
    private bool _derivativeInitialized;


    public float ProportionalGain
    {
        set => _proportionalGain = value;
    }
    public float IntegralGain
    {
        set => _integralGain = value;
    }
    public float DerivativeGain
    {
        set => _derivativeGain = value;
    }
    public DerivativeMeasurement DerivativeMeasurement
    {
        set => _derivativeMeasurement = value;
    }


    public PIDController(float proportionalGain, float integralGain, float derivativeGain, float integralSaturation, DerivativeMeasurement derivativeMeasurement)
    {
        _proportionalGain = proportionalGain;
        _integralGain = integralGain;
        _derivativeGain = derivativeGain;
        _integralSaturation = integralSaturation;
        _derivativeMeasurement = derivativeMeasurement;
    }

    public void Reset()
    {
        _derivativeInitialized = false;
    }

    public float Update(float delta, float currentValue, float targetValue)
    {
        float error = targetValue - currentValue;
        Debug.LogWarning($"PID error: {error}");

        //calculate P term
        float P = _proportionalGain * error;

        //calculate I term
        _integrationStored = Mathf.Clamp(_integrationStored + (error * delta), -_integralSaturation, _integralSaturation);
        float I = _integralGain * _integrationStored;

        //calculate both D terms
        float errorRateOfChange = (error - _errorLast) / delta;
        _errorLast = error;

        float valueRateOfChange = (currentValue - _valueLast) / delta;
        _valueLast = currentValue;

        //choose D term to use
        float deriveMeasure = 0;

        if (_derivativeInitialized)
        {
            if (_derivativeMeasurement == DerivativeMeasurement.Velocity)
            {
                deriveMeasure = -valueRateOfChange;
            }
            else
            {
                deriveMeasure = errorRateOfChange;
            }
        }
        else
        {
            _derivativeInitialized = true;
        }

        float D = _derivativeGain * deriveMeasure;

        float result = P + I + D;

        Report(P, I, D);

        return Mathf.Clamp(result, 0, Mathf.Infinity);
    }

    public Vector3 GetParameters()
    {
        return new Vector3(_proportionalGain, _integralGain, _derivativeGain);
    }

    private void Report(float p, float i, float d)
    {
        Debug.LogWarning($"({p.ToString("n1")}; {i.ToString("n1")}; {d.ToString("n1")})");
    }
}
