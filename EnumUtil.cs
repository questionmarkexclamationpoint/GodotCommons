using System;
using System.Runtime.CompilerServices;

namespace Commons;

public static class EnumUtil {
    public static long ToLong<E>(E value) where E : Enum {
        return Unsafe.As<E, long>(ref value);
    }

    public static E? FromLong<E>(long value) where E : struct, Enum {
        E[] values = Enum.GetValues<E>();
        if (value < 0 || value >= values.Length) {
            return null;
        }
        return Unsafe.As<long, E>(ref value);
    }
}
