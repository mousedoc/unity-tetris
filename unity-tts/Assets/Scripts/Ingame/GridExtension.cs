using System.Collections.Generic;
using UnityEngine;

public static class GridExtension
{
    public static Vector2 LocalToWorld(this Vector2 vector)
    {
        int x = (GridInfo.Width - 1) / 2 + (int)vector.x;
        int y = (GridInfo.Height - 1) + (int)vector.y;

        return new Vector2(x, y);
    }

    public static List<Vector2> LocalToWorld(this List<Vector2> list)
    {
        var newList = new List<Vector2>();

        foreach (var vector in list)
            newList.Add(vector.LocalToWorld());

        return newList;
    }

    public static Vector2 RotateByPivot(this Vector2 point, Vector2 pivot, float degree)
    {
        Vector3 offset = Quaternion.Euler(0, 0, degree) * (point - pivot);
        var newPos = (Vector2)offset + pivot;

        return new Vector2(Mathf.Round(newPos.x), Mathf.Round(newPos.y));
    }

    public static bool IsAvailiable(this Vector2 position)
    {
        if (position.x < 0 ||
            position.y < 0 ||
            position.x >= GridInfo.Width ||
            position.y >= GridInfo.Height ||
            IngameController.Instance.Grid[(int)position.y, (int)position.x].IsActive)
            return false;

        return true;
    }
}