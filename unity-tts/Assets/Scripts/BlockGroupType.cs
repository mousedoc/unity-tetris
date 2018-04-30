using System;

public enum BlockGroupType
{
    I, J, L, O, S, T, Z
}

public static class EnumExtension
{
    public static T GetRandom<T>()
    {
        var value = Enum.GetValues(typeof(T));
        return (T)value.GetValue(new Random().Next(value.Length));
    }
}