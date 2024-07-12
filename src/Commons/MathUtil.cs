namespace QuestionMarkExclamationPoint.Commons;

using System;

public static class MathUtil {

    public static float Modulo(float dividend, float divisor) => dividend < 0 ? (dividend % divisor) + divisor : dividend % divisor;

    public static (float, float) WeightToPercentages(float weight) {
        weight = (Math.Clamp(weight, -1, 1) + 1) / 2;
        return (1 - weight, weight);
    }
}
