namespace QuestionMarkExclamationPoint.Commons.Extensions;

using System;

public static class RandomExtensions {
    public static double NextDoubleInRange(this Random random, double min, double max) {
        (min, max) = min > max ? (max, min) : (min, max);
        return (random.NextDouble() * (max - min)) + min;
    }

    public static double NextDoubleInRange(this Random random, Interval<double> range)
        => random.NextDoubleInRange(range.Start, range.End);

    public static float NextSingleInRange(this Random random, float min, float max) {
        (min, max) = min > max ? (max, min) : (min, max);
        return (random.NextSingle() * (max - min)) + min;
    }

    public static float NextSingleInRange(this Random random, Interval<float> range)
        => random.NextSingleInRange(range.Start, range.End);

    public static double NextGaussianDouble(this Random random, double mean = 0.0, double stdDev = 1.0) {
        // Box-Muller transform
        var u1 = random.NextDouble();
        var u2 = random.NextDouble();
        var z0 = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);
        return (z0 * stdDev) + mean;
    }

    public static float NextGaussianSingle(this Random random, float mean = 0.0f, float stdDev = 1.0f) {
        // Box-Muller transform
        var u1 = random.NextSingle();
        var u2 = random.NextSingle();
        var z0 = MathF.Sqrt(-2.0f * MathF.Log(u1)) * MathF.Cos(2.0f * MathF.PI * u2);
        return (z0 * stdDev) + mean;
    }

    public static int NextIntInRange(this Random random, int min, int max) {
        (min, max) = min > max ? (max, min) : (min, max);
        return (int)random.NextInt64(max - min) + min;
    }

    public static int NextIntInRange(this Random random, Interval<int> range)
        => random.NextIntInRange(range.Start, range.End);
}
