namespace QuestionMarkExclamationPoint.Commons;

using System.Numerics;

public readonly struct Range<T>(T a, T b, T? c = null)
        where T : struct, INumber<T> {
    public T Start { get; } = a;

    // TODO find a better way to hand invalid non-null mid
    public T Mid { get; } = c == null
            ? (b + a) / Two
            : InitializeMid(a, b, (T)c);

    public T End { get; } = c == null ? b : (T)c;

    public T Delta => this.End - this.Start;

    public T UpperDelta => this.End - this.Mid;

    public T LowerDelta => this.Mid! - this.Start;

    public bool IsAscending => this.End > this.Start;

    public bool IsDescending => this.Start > this.End;

    public Range<T> Lower => new(this.Start, this.Mid);

    public Range<T> Upper => new(this.Mid, this.End);

    public static Range<T> operator *(Range<T> left, T right)
        => new((left.LowerDelta * right) + left.Start, left.Mid, (left.UpperDelta * right) + left.Mid);

    public static Range<T> operator /(Range<T> left, T right)
        => new((left.LowerDelta / right) + left.Start, left.Mid, (left.UpperDelta / right) + left.Mid);

    public static Range<T> operator +(Range<T> left, T right)
        => new(left.Start + right, left.Mid + right, left.End + right);

    public static Range<T> operator -(Range<T> left, T right)
        => new(left.Start - right, left.Mid - right, left.End - right);
    public TOther GetScale<TOther>(Range<TOther> other, T value)
            where TOther : struct,
                INumber<TOther>,
                IDivisionOperators<TOther, T, TOther> {
        var inMin = this.GetMin(value);
        var inMax = this.GetMax(value);
        var outMin = inMin == this.Start ? other.Start : other.Mid;
        var outMax = inMax == this.End ? other.End : other.Mid;
        var inDelta = inMax - inMin;
        var outDelta = outMax - outMin;
        return outDelta / inDelta;
    }

    public (TOther, TOther) GetScale<TOther>(Range<TOther> other)
            where TOther : struct,
                INumber<TOther>,
                IDivisionOperators<TOther, T, TOther>
        => (this.GetScale(other, this.Start), this.GetScale(other, this.End));

    public TOther Map<TOther>(Range<TOther> other, T value)
            where TOther : struct,
                INumber<TOther>,
                IDivisionOperators<TOther, T, TOther>,
                IMultiplyOperators<TOther, T, TOther> {
        if (this.IsAscending ? value <= this.Start : value >= this.Start) {
            return other.Start;
        } else if (this.IsAscending ? value >= this.End : value <= this.End) {
            return other.End;
        }
        var inMin = this.GetMin(value);
        var outMin = inMin == this.Start ? other.Start : other.Mid;
        return (this.GetScale(other, value) * (value - inMin)) + outMin;
    }

    public override string ToString() => $"[{this.Start} .. {this.Mid} .. {this.End}]";

    private static T Two => T.One + T.One;

    private static T InitializeMid(T start, T mid, T end) {
        if (end > start) {
            if (mid < start) {
                mid = start;
            } else if (mid > end) {
                mid = end;
            }
        } else {
            if (mid > start) {
                mid = start;
            } else if (mid < end) {
                mid = end;
            }
        }
        return mid;
    }

    private T GetMin(T value)
        => (this.IsAscending ? value <= this.Mid : value >= this.Mid)
            ? this.Start
            : this.Mid;

    private T GetMax(T value)
        => (this.IsAscending ? value <= this.Mid : value >= this.Mid)
            ? this.Mid
            : this.End;
}

public static class Range {
    public static readonly Range<double> Percentage = new(0, 1);

    public static readonly Range<double> Tanh = new(-1, 1);
}
