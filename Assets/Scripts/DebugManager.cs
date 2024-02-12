using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    public ResourceManager ResourceManager;
    public uint countChange = 0;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            ResourceManager.Wood +=  countChange;
        }
        if(Input.GetKeyDown(KeyCode.Keypad2))
        {
            ResourceManager.Stone += countChange;
        }
        if(Input.GetKeyDown(KeyCode.Keypad3))
        {
            ResourceManager.Crystal += countChange;
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            ResourceManager.Lumber += countChange;
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            ResourceManager.Brick += countChange;
        }
    }
}
