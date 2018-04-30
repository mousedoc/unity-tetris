using UnityEngine;

public static class ColorExtension
{
    public static Color ToTranslucent(this Color color)
    {
        color.a = 0.5f;
        return color;
    }

    public static Color ToOpacity(this Color color)
    {
        color.a = 1.0f;
        return color;
    }
}
