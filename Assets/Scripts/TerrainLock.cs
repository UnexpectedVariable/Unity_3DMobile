using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainLock : MonoBehaviour
{
    [SerializeField]
    private ResourceDemandManager _resourceDemandManager = null;
    [SerializeField]
    private List<GameObject> _terrain = null;

    public event EventHandler<GameObjectEventArgs> TerrainUnlockedEvent = null;

    private void Start()
    {
        _resourceDemandManager.DemandsSatisfiedEvent += HandleDemandsSatisfied;
    }

    private void HandleDemandsSatisfied(object sender, EventArgs args)
    {
        Debug.Log($"Demands satisfied event invoked!");
        GameObjectEventArgs GOargs = new GameObjectEventArgs();
        GOargs.GOs = _terrain;
        TerrainUnlockedEvent?.Invoke(this, GOargs);
        gameObject.SetActive(false);
    }
}
