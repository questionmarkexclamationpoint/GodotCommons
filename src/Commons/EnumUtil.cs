namespace Commons;

using System;
using System.Runtime.CompilerServices;

public static class EnumUtil {
    public static long ToLong<TEnum>(TEnum value) where TEnum : Enum => Unsafe.As<TEnum, long>(ref value);

    public static TEnum? FromLong<TEnum>(long value) where TEnum : struct, Enum {
        var values = Enum.GetValues<TEnum>();
        if (value < 0 || value >= values.Length) {
            return null;
        }
        return Unsafe.As<long, TEnum>(ref value);
    }
}
