using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainLock : MonoBehaviour
{
    [SerializeField]
    private ResourceDemandManager _resourceDemandManager = null;
    [SerializeField]
    private GameObject _terrain = null;

    private void Start()
    {
        _resourceDemandManager.DemandsSatisfiedEvent += HandleDemandsSatisfied;
    }

    private void HandleDemandsSatisfied(object sender, EventArgs args)
    {
        Debug.Log($"Demands satisfied event invoked!");
        _terrain.SetActive(true);
        gameObject.SetActive(false);
    }
}