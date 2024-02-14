using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] _resourceTMPRows = new TextMeshProUGUI[3];
    [SerializeField]
    private Image[] _resourceIcons = new Image[3];

    private Dictionary<Resources, uint> _resourceMap = new Dictionary<Resources, uint>();

    public uint Wood
    {
        get => _resourceMap[Resources.Wood];
        set
        {
            _resourceMap[Resources.Wood] = value;
            SetResourceCount(Resources.Wood);
        }
    }
    public uint Stone
    {
        get => _resourceMap[Resources.Stone];
        set
        {
            _resourceMap[Resources.Stone] = value;
            SetResourceCount(Resources.Stone);
        }
    }
    public uint Crystal
    {
        get => _resourceMap[Resources.Crystal];
        set
        {
            _resourceMap[Resources.Crystal] = value;
            SetResourceCount(Resources.Crystal);
        }
    }
    public uint Lumber
    {
        get => _resourceMap[Resources.Lumber];
        set
        {
            _resourceMap[Resources.Lumber] = value;
            SetResourceCount(Resources.Lumber);
        }
    }
    public uint Brick
    {
        get => _resourceMap[Resources.Brick];
        set
        {
            _resourceMap[Resources.Brick] = value;
            SetResourceCount(Resources.Brick);
        }
    }

    private Resources[] _activeResourceTypes = new Resources[3];

    private void Start()
    {
        InitializeResourceMap();
        InitializeStartResources();
        //InitializeActiveResources();
    }

    private void InitializeStartResources()
    {
        SetResourceCount(500, Resources.Brick);
        SetResourceCount(500, Resources.Lumber);
        SetResourceCount(500, Resources.Crystal);
        SetResourceCount(500, Resources.Stone);
        SetResourceCount(500, Resources.Wood);
    }

    private void InitializeActiveResources()
    {
        _activeResourceTypes[0] = Resources.Wood;
        _activeResourceTypes[1] = Resources.Stone;
        _activeResourceTypes[2] = Resources.Crystal;
    }

    private void InitializeResourceMap()
    {
        _resourceMap.Add(Resources.Wood, 0);
        _resourceMap.Add(Resources.Stone, 0);
        _resourceMap.Add(Resources.Crystal, 0);
        _resourceMap.Add(Resources.Lumber, 0);
        _resourceMap.Add(Resources.Brick, 0);
    }

    private bool IsResourceActive(Resources resourceType)
    {
        if(_activeResourceTypes.Contains(resourceType)) return true;
        return false;
    }

    private void MakeResourceActive(Resources resourceType)
    {
        if (IsResourceActive(resourceType)) return;
        ShiftUI(resourceType);
    }

    private void ShiftUI(Resources resourceType)
    {
        _activeResourceTypes[2] = _activeResourceTypes[1];
        _resourceTMPRows[2].text = _resourceTMPRows[1].text;
        _resourceIcons[2].sprite = _resourceIcons[1].sprite;
        _activeResourceTypes[1] = _activeResourceTypes[0];
        _resourceTMPRows[1].text = _resourceTMPRows[0].text;
        _resourceIcons[1].sprite = _resourceIcons[0].sprite;
        _activeResourceTypes[0] = resourceType;
        _resourceIcons[0].sprite = UnityEngine.Resources.Load<Sprite>(Constants.ResourceIconMap[resourceType]);
    }

    private void SetResourceCount(Resources type)
    {
        MakeResourceActive(type);
        int idx = Array.IndexOf(_activeResourceTypes, type);
        _resourceTMPRows[idx].text = $"{_resourceMap[type]}";
    }

    public void SetResourceCount(uint count, Resources type)
    {
        _resourceMap[type] = count;
        SetResourceCount(type);
    }

    public uint GetResourceCount(Resources type)
    {
        return _resourceMap[type];
    }
}
