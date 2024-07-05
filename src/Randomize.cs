using System;
using System.Security.Cryptography;

namespace Commons;

public static class Randomize
{
    public static readonly int SEED = DateTime.Now.Millisecond.GetHashCode(); // TODO
    public static readonly Random RANDOM = new(SEED);

    public static float Random(float max = 1) {
        return RandomRange(0, max);
    }

    public static float RandomRange(float min, float max) {
        (min, max) = min > max ? (max, min) : (min, max);
        float value = (float)RANDOM.NextDouble();
        float diff = max - min;
        return value * diff + min;
    }

    public static int RandomRangeInt(int min, int max) {
        return (int)Math.Floor(RandomRange(Math.Min(min, max), Math.Max(min, max) + 1));
    }
}
