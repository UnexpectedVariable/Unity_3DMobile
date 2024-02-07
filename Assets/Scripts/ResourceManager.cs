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

    /*[SerializeField]
    private int _wood = 0;
    [SerializeField]
    private int _stone = 0;
    [SerializeField]
    private int _crystal = 0;
    [SerializeField]
    private int _lumber = 0;
    [SerializeField]
    private int _brick = 0;*/

    private Dictionary<Resources, int> _resourceMap = new Dictionary<Resources, int>();

    public int Wood
    {
        get => _resourceMap[Resources.Wood];
        set
        {
            _resourceMap[Resources.Wood] = value;
            SetResourceCount(Resources.Wood);
        }
    }
    public int Stone
    {
        get => _resourceMap[Resources.Stone];
        set
        {
            _resourceMap[Resources.Stone] = value;
            SetResourceCount(Resources.Stone);
        }
    }
    public int Crystal
    {
        get => _resourceMap[Resources.Crystal];
        set
        {
            _resourceMap[Resources.Crystal] = value;
            SetResourceCount(Resources.Crystal);
        }
    }
    public int Lumber
    {
        get => _resourceMap[Resources.Lumber];
        set
        {
            _resourceMap[Resources.Lumber] = value;
            SetResourceCount(Resources.Lumber);
        }
    }
    public int Brick
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
        InitializeActiveResources();
        InitializeResourceMap();
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
        _resourceIcons[0].sprite = (Sprite)AssetDatabase.LoadAssetAtPath(Constants.ResourceIconMap[resourceType], typeof(Sprite));
    }

    private void SetResourceCount(Resources type)
    {
        MakeResourceActive(type);
        int idx = Array.IndexOf(_activeResourceTypes, type);
        _resourceTMPRows[idx].text = $"{_resourceMap[type]}";
    }
}
