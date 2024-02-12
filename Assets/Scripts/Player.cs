using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private ResourceManager _resourceManager;

    public ResourceManager resourceManager
    {
        get => _resourceManager;
    }
}
