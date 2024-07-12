namespace QuestionMarkExclamationPoint.Commons;

using System.Numerics;

public static class Range {
    public static TOut Map<TIn, TOut>(TIn value, (TIn, TIn) inBounds, (TOut, TOut) outBounds)
            where TIn : INumber<TIn>
            where TOut : INumber<TOut>, IDivisionOperators<TOut, TIn, TOut>, IMultiplyOperators<TOut, TIn, TOut> {
        var (inMin, inMax) = SortTwo(inBounds);
        var (outMin, outMax) = SortTwo(outBounds);
        if (value <= inMin) {
            return outMin;
        } else if (value >= inMax) {
            return outMax;
        }
        var scale = GetScale(inBounds, outBounds);
        return (scale * (value - inMin)) + outMin;
    }

    public static TOut Map<TIn, TOut>(TIn value, (TIn, TIn, TIn) inBounds, (TOut, TOut, TOut) outBounds)
            where TIn : INumber<TIn>
            where TOut : INumber<TOut>, IDivisionOperators<TOut, TIn, TOut>, IMultiplyOperators<TOut, TIn, TOut> {
        var (inMin, inMid, inMax) = SortThree(inBounds);
        var (outMin, outMid, outMax) = SortThree(outBounds);
        return value <= inMid
                ? Map(value, (inMin, inMid), (outMin, outMid))
                : Map(value, (inMid, inMax), (outMid, outMax));
    }

    public static float MapToPercentage(float value, (float, float) inBounds)
            => Map(value, inBounds, (0f, 1f));

    public static float MapToPercentage(float value, (float, float, float) inBounds)
            => Map(value, inBounds, (0f, 0.5f, 1f));

    public static float MapFromPercentage(float percentage, (float, float) outBounds)
        => Map(percentage, (0f, 1f), outBounds);

    public static float MapFromPercentage(float percentage, (float, float, float) outBounds)
        => Map(percentage, (0f, 0.5f, 1f), outBounds);

    private static TOut GetScale<TIn, TOut>((TIn, TIn) inBounds, (TOut, TOut) outBounds)
            where TIn : INumber<TIn>
            where TOut : INumber<TOut>, IDivisionOperators<TOut, TIn, TOut> {
        var (inMin, inMax) = inBounds;
        var (outMin, outMax) = outBounds;
        var inRange = inMax - inMin;
        var outRange = outMax - outMin;
        return outRange / inRange;
    }

    private static (T, T) SortTwo<T>((T, T) two) where T : INumber<T>
            => SortTwo(two.Item1, two.Item2);

    private static (T, T) SortTwo<T>(T a, T b) where T : INumber<T>
            => a > b ? (b, a) : (a, b);

    private static (T, T, T) SortThree<T>((T, T, T) three) where T : INumber<T>
            => SortThree(three.Item1, three.Item2, three.Item3);

    private static (T, T, T) SortThree<T>(T a, T b, T c) where T : INumber<T> {
        (a, c) = SortTwo(a, c);
        (a, b) = SortTwo(a, b);
        (b, c) = SortTwo(b, c);
        return (a, b, c);
    }
}
