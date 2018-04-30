using UnityEngine;
using System;

public static class ColorExtension
{
    public static Color GetRandomColor()
    {
        var type = EnumExtension.GetRandom<BlockColorType>();

        switch(type)
        {
            case BlockColorType.Blue:
                return "81C7FCFF".ToColorByHex();

            case BlockColorType.Sky:
                return "9ED5FDFF".ToColorByHex();

            case BlockColorType.Yellow:
                return "FAE7C6FF".ToColorByHex();

            case BlockColorType.Orange:
                return "FEC19DFF".ToColorByHex();

            case BlockColorType.Red:
                return "FC8885FF".ToColorByHex();

            default:
                return Color.white;
        }
    }

    public static Color ToColorByHex(this string hex)
    {
        hex = hex.Replace("0x", "");
        hex = hex.Replace("#", "");

        byte a = 255;
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

        if (hex.Length == 8)
        {
            a = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
        }

        return new Color(r, g, b, a);
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
