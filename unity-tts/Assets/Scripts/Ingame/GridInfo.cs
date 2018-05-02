using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridInfo
{
    public readonly static int Width = 13;
    public readonly static int Height = 26;

    public static int CenterXIndex
    {
        get { return Width / 2; }
    }

    public static int CenterYIndex
    {
        get { return Height / 2; }
    }
}
