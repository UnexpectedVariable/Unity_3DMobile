using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    public static Dictionary<Resources, string> ResourceIconMap = new Dictionary<Resources, string>()
    {
        {Resources.Wood, "Sprites/Wood"},
        {Resources.Stone, "Sprites/Stone"},
        {Resources.Crystal, "Sprites/Crystal"},
        {Resources.Lumber, "Sprites/Lumber"},
        {Resources.Brick, "Sprites/Brick"}
    };
}