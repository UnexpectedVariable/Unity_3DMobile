using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class ResourceUIRow : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _resourceTMPRow = null;
    [SerializeField]
    private Image _resourceIcon = null;
    [SerializeField]
    private Resources _type;
    [SerializeField]
    private bool _autoLoadIcon = false;

    private uint _count = 0;

    public string Text
    {
        get => _resourceTMPRow.text;
        set
        {
            _resourceTMPRow.text = value;
            _count = uint.Parse(value);
        }
    }
    public Resources Type
    {
        get => _type;
    }
    public uint Count
    {
        get => _count;
        set
        {
            _count = value;
            _resourceTMPRow.text = $"{value}";
        }
    }

    public void Initialize()
    {
        if(_autoLoadIcon)
        {
            _resourceIcon.sprite = (Sprite)AssetDatabase.LoadAssetAtPath(Constants.ResourceIconMap[_type], typeof(Sprite));
        }
        _count = uint.Parse(_resourceTMPRow.text);
    }
}
