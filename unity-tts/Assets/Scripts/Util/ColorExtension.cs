using UnityEngine;
using System;

public static class ColorExtension
{
    public static Color GetRandomColor()
    {
        var type = EnumExtension.GetRandom<BlockColorType>();
        return type.ToColor();
    }

    public static Color ToColor(this BlockColorType type)
    {
        switch (type)
        {
            case BlockColorType.Blue:
                return "#81C7FCFF".ToColorByHex();

            case BlockColorType.Sky:
                return "#9ED5FDFF".ToColorByHex();

            case BlockColorType.Yellow:
                return "#FAE7C6FF".ToColorByHex();

            case BlockColorType.Orange:
                return "#FEC19DFF".ToColorByHex();

            case BlockColorType.Red:
                return "#FC8885FF".ToColorByHex();

            default:
                return Color.white;
        }
    }

    public static Color ToColorByHex(this string hex)
    {
        Color color;
        ColorUtility.TryParseHtmlString(hex, out color);

        return color;
    }

    public static Color ToTransparent(this Color color)
    {
        color.a = 0.0f;
        return color;
    }

    public static Color ToHalfTransparent(this Color color)
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
