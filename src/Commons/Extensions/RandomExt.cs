namespace QuestionMarkExclamationPoint.Commons.Extensions;

using System;

public static class RandomExt {
    public static float NextFloat(this Random random) => random.NextSingle();

    public static double NextDoubleInRange(this Random random, double min, double max) {
        (min, max) = min > max ? (max, min) : (min, max);
        return (random.NextDouble() * (max - min)) + min;
    }

    public static double NextDoubleInRange(this Random random, Range<double> range)
        => random.NextDoubleInRange(range.Start, range.End);

    public static float NextFloatInRange(this Random random, float min, float max) {
        (min, max) = min > max ? (max, min) : (min, max);
        return (random.NextFloat() * (max - min)) + min;
    }

    public static float NextFloatInRange(this Random random, Range<float> range)
        => random.NextFloatInRange(range.Start, range.End);

    public static int NextIntInRange(this Random random, int min, int max) {
        (min, max) = min > max ? (max, min) : (min, max);
        return (int)random.NextInt64(max - min) + min;
    }

    public static int NextIntInRange(this Random random, Range<int> range)
        => random.NextIntInRange(range.Start, range.End);
}
