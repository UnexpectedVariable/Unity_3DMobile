using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using System.Linq;

public class ResourceDemandManager : MonoBehaviour
{
    [SerializeField]
    private ResourceUIRow[] _resourceUIRows = new ResourceUIRow[5];

    public uint Wood
    {
        get => GetResourceCount(Resources.Wood);
        set
        {
            SetResourceCount(value, Resources.Wood);
        }
    }
    public uint Stone
    {
        get => GetResourceCount(Resources.Stone);
        set
        {
            SetResourceCount(value, Resources.Stone);
        }
    }
    public uint Crystal
    {
        get => GetResourceCount(Resources.Crystal);
        set
        {
            SetResourceCount(value, Resources.Crystal);
        }
    }
    public uint Lumber
    {
        get => GetResourceCount(Resources.Lumber);
        set
        {
            SetResourceCount(value, Resources.Lumber);
        }
    }
    public uint Brick
    {
        get => GetResourceCount(Resources.Brick);
        set
        {
            SetResourceCount(value, Resources.Brick);
        }
    }

    private void Start()
    {
        foreach(var row in _resourceUIRows)
        {
            if(row.Count == 0) row.gameObject.SetActive(false);
        }
    }

    public void SetResourceCount(uint count, Resources type)
    {
        //_resourceMap[type] = count;
        foreach (var row in _resourceUIRows)
        {
            if (row.Type == type) row.Count = count;
        }
    }

    public uint GetResourceCount(Resources type)
    {
        foreach(var row in _resourceUIRows)
        {
            if(row.Type == type) return row.Count;
        }
        return 0;
    }

    public Dictionary<Resources, uint> GetDemands()
    {
        var resourceTypes = (Resources[])Enum.GetValues(typeof(Resources));
        var demands = new Dictionary<Resources, uint>(resourceTypes.Length);
        foreach(Resources resource in resourceTypes)
        {
            demands.Add(resource, GetResourceCount(resource));
        }
        demands.TrimExcess();
        return demands;
    }
}
