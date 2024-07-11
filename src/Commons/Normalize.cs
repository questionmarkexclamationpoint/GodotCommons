namespace Commons;

using System;

public static class Normalize {

    // 0.99 = tanh(2.64665)
    private static readonly float TANH_CLAMP_X_FACTOR = 2.64665f;
    // 1.0101 = 1 / 0.99
    private static readonly float TANH_CLAMP_Y_FACTOR = 1.0101f;
    public static float Tanh(
            float value,
            bool clamp = false
    ) {
        var squeeze = clamp ? TANH_CLAMP_X_FACTOR : 1;
        var outStretch = clamp ? TANH_CLAMP_Y_FACTOR : 1;
        return outStretch * (float)Math.Tanh(squeeze * value);
    }

    // TODO implement other parameters for sigmoid
    public static float Sigmoid(float value) => 1 / (1 + (float)Math.Pow(Math.E, -value));
}
