namespace QuestionMarkExclamationPoint.Commons;

using System;

public static class EnumExt {
    public static long ToIndex(this Enum value)
            => Array.IndexOf(Enum.GetValues(value.GetType()), value);

    public static long ToIndex<TEnum>(TEnum value) where TEnum : Enum
            => Array.IndexOf(Enum.GetValues(value.GetType()), value);

    public static TEnum? FromIndex<TEnum>(long index) where TEnum : struct, Enum {
        var values = Enum.GetValues<TEnum>();
        if (index < 0 || index >= values.Length) {
            return null;
        }
        return values[index];
    }
}
