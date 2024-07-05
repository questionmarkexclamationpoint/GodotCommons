using System;

namespace Commons;

// TODO this is broken
public static class RangeScale
{
    public static float MapPercentage(float value, float outMin, float outMax)
    {
        (outMin, outMax) = outMin > outMax ? (outMax, outMin) : (outMin, outMax);
        return MapPercentage(value, outMin, (outMax - outMin) / 2, outMax);
    }

    public static float MapPercentage(float value, float outMin, float outMid, float outMax)
    {
        return Map(value, 0, 0.5f, 1, outMin, outMid, outMax);
    }

    public static float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        (inMin, inMax) = inMin > inMax ? (inMax, inMin) : (inMin, inMax);
        (outMin, outMax) = outMin > outMax ? (outMax, outMin) : (outMin, outMax);
        return Map(value, inMin, (inMax - inMin) / 2, inMax, outMin, (outMax - outMin) / 2, outMax);
    }

    public static float Map(float value, float inMin, float inMid, float inMax, float outMin, float outMid, float outMax)
    {
        (outMin, outMid, outMax) = SortThree(outMin, outMid, outMax);
        (inMin, inMid, inMax) = SortThree(inMin, inMid, inMax);
        value = Math.Clamp(value, inMin, inMax) / (inMax - inMin);
        if (value < inMid)
        {
            return (value - inMin) / (inMid - inMin) * (outMid - outMin) + outMin;
        }
        else
        {
            return (value - inMid) / (inMax - inMid) * (outMax - outMid) + outMid;
        }
    }

    private static (float, float, float) SortThree(float a, float b, float c)
    {
        (c, a) = a > c ? (a, c) : (c, a);
        (b, a) = a > b ? (a, b) : (b, a);
        (c, b) = b > c ? (b, c) : (c, b);
        return (a, b, c);
    }
}
