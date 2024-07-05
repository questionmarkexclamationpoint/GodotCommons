using System;

namespace Commons;

public static class Normalize {

    // 0.99 = tanh(2.64665)
    private static readonly float TANH_CLAMP_X_FACTOR = 2.64665f;
    // 1.0101 = 1/0.99
    private static readonly float TANH_CLAMP_Y_FACTOR = 1.0101f;
    public static float Tanh(
            float value,
            float min = -1,
            float max = 1,
            float shift = 0,
            float squeeze = 1,
            bool clamp = false
    ) {
        (min, max) = MinMax(min, max);
        if (clamp && (value <= -1 || value >= 1)) {
            return value <= -1 ? min : max;
        }
        var outStretch = (max - min) / 2;
        var outIncrement = (max + min) / 2;
        if (clamp) {
            squeeze *= TANH_CLAMP_X_FACTOR;
            outStretch *= TANH_CLAMP_Y_FACTOR;
        }
        return outStretch * (float)Math.Tanh(squeeze * (value - shift)) + outIncrement;
    }

    private static readonly float SIGMOID_CLAMP_X_FACTOR = 5.2933f;
    private static readonly float SIGMOID_CLAMP_Y_FACTOR = 1.0101f;
    public static float Sigmoid(
            float value,
            float min = -1,
            float max = 1,
            float shift = 0,
            float squeeze = 1,
            bool clamp = false
    ) {
        (min, max) = MinMax(min, max);
        if (clamp && (value <= -1 || value >= 1)) {
            return value <= -1 ? min : max;
        }
        var stretch = max - min;
        if (clamp) {
            squeeze *= SIGMOID_CLAMP_X_FACTOR;
            stretch *= SIGMOID_CLAMP_Y_FACTOR;
        }
        return stretch * (1 / (1 + (float)Math.Pow(Math.E, -(squeeze * (value - shift))))) + min;
    }

    public static float PercentageSigmoid(float x) {
        return Sigmoid(x, 0, 1, clamp: true);
    }
    private static (float, float) MinMax(float a, float b) {
        return a > b ? (b, a) : (a, b);
    }
}
