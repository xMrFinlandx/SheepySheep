using System;
using _Scripts.Utilities.Tools;
using UnityEngine;

namespace _Scripts.Utilities.Tools
{
    public enum OffsetType
    {
        Top,
        Bottom,
        Left,
        Right,
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight,
        Center
    }
}

public static class Ex
{
    public static Vector3Int GetOffset(this OffsetType offsetType)
    {
        return offsetType switch
        {
            OffsetType.Top => new Vector3Int(0, 1, 0),
            OffsetType.Bottom => new Vector3Int(0, -1, 0),
            OffsetType.Left => new Vector3Int(-1, 0, 0),
            OffsetType.Right => new Vector3Int(1, 0, 0),
            OffsetType.TopLeft => new Vector3Int(-1, 1, 0),
            OffsetType.TopRight => new Vector3Int(1, 1, 0),
            OffsetType.BottomLeft => new Vector3Int(-1, -1, 0),
            OffsetType.BottomRight => new Vector3Int(1, -1, 0),
            OffsetType.Center => new Vector3Int(0, 0, 0),
            _ => new Vector3Int(0, 0, 0)
        };
    }
}