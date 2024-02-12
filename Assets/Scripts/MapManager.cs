using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    private ResourceDemandManager _resourceDemandManager = null;

    [SerializeField]
    private List<ResourceDemandManager> _resourceDemandManagers = null;

    private void Start()
    {
        _resourceDemandManager.DemandsSatisfiedEvent += HandleDemandsSatisfied;

    }

    private void HandleDemandsSatisfied(object sender, EventArgs args)
    {
        //_unlockMap[sender as ResourceDemandManager].SetActive(true);
    }
}
