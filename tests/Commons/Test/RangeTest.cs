namespace QuestionMarkExclamationPoint.Commons.Test;

using System;
using System.Numerics;
using Commons;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class RangeTest {
    [TestMethod]
    public void TestSimpleMap() {
        var numTests = 10;
        Range<double> input = new(-1, 1);
        Range<double> output = new(-100, 200);
        var inputInc = input.Delta / numTests;
        var outputInc = output.Delta / numTests;
        for (var i = 0; i < 10; i++) {
            var inputValue = input.Start + (i / 10.0 * input.Delta);
            var outputValue = output.Start + (i / 10.0 * output.Delta);
            var result = input.Map(output, inputValue);
            Assert.AreEqual(
                outputValue,
                result,
                0.01
            );
        }
    }

    [TestMethod]
    public void TestMidPointMap() {
        Range<float> input = new(-1, 0, 1);
        Range<float> output = new(-100, 0, 200);
        Assert.AreEqual(-50, input.Map(output, -0.5f), 0.01);
        Assert.AreEqual(100, input.Map(output, 0.5f), 0.01);
    }

    private static void TestInitializationRange<T>(T a, T b, T c) where T : struct, INumber<T> {
        Range<T> range = new(a, b, c);
        Assert.AreEqual(a, range.Start, $"{a}, {b}, {c} -> {range}");
        Assert.AreEqual(c, range.End, $"{a}, {b}, {c} -> {range}");
        Assert.IsTrue(a < c ? range.Start <= range.Mid : range.Start >= range.Mid, $"{a}, {b}, {c} -> {range}");
        Assert.IsTrue(a < c ? range.End >= range.Mid : range.End <= range.Mid, $"{a}, {b}, {c} -> {range}");
        Assert.AreEqual(a, range.Start, $"{a}, {b}, {c} -> {range}");
        Assert.AreEqual(c, range.End, $"{a}, {b}, {c} -> {range}");
        Assert.AreEqual(c - a, range.Delta, $"{a}, {b}, {c} -> {range}");
    }

    private static void TestInitializationRanges<T>(T min, T max, T increment) where T : struct, INumber<T> {
        var mult = T.Zero;
        for (var i = 0; i < 10; i++, mult += T.One) {
            var a = min + (increment * mult);
            var b = ((max + min) / (T.One + T.One)) - (increment * (i % 2 == 0 ? mult : -mult));
            var c = max - (increment * mult);
            TestInitializationRange(a, b, c);
            TestInitializationRange(a, c, b);
            TestInitializationRange(b, a, c);
            TestInitializationRange(b, c, a);
            TestInitializationRange(c, a, b);
            TestInitializationRange(c, b, a);
        }
    }

    [TestMethod]
    public void TestInitialization() {
        TestInitializationRanges(int.MinValue, 0, 36);
        TestInitializationRanges(0f, 33f, 4.39);
        TestInitializationRanges(0.0, -33.6, 0.001);
        TestInitializationRanges<byte>(33, 55, 2);
    }

    [TestMethod]
    public void TestScale() => throw new NotImplementedException();

    [TestMethod]
    public void TestOperators() => throw new NotImplementedException();
}
