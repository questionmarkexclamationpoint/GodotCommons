namespace Commons;

using System;

public static class RandomExt {
    public static float NextFloat(this Random random) => random.NextSingle();


    public static double NextDoubleInRange(this Random random, double min, double max) {
        (min, max) = min > max ? (max, min) : (min, max);
        return (random.NextDouble() * (max - min)) + min;
    }

    public static float NextFloatInRange(this Random random, float min, float max) {
        (min, max) = min > max ? (max, min) : (min, max);
        return (random.NextFloat() * (max - min)) + min;
    }

    public static int NextIntInRange(this Random random, int min, int max) {
        (min, max) = min > max ? (max, min) : (min, max);
        return (int)random.NextInt64(max - min) + min;
    }
}
