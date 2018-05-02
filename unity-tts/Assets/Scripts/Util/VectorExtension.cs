using UnityEngine;

public static class VectorExtension
{
    #region Vector2 

    public static int GetIntX(this Vector2 vector)
    {
        return (int)vector.x;
    }

    public static int GetIntY(this Vector2 vector)
    {
        return (int)vector.y;
    }

    #endregion

    #region Vector3 

    public static int GetIntX(this Vector3 vector)
    {
        return (int)vector.x;
    }

    public static int GetIntY(this Vector3 vector)
    {
        return (int)vector.y;
    }

    public static int GetIntZ(this Vector3 vector)
    {
        return (int)vector.z;
    }

    #endregion
}