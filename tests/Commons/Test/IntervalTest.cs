namespace QuestionMarkExclamationPoint.Commons.Test;

using System;
using System.Numerics;
using Commons;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class IntervalTest {
    [TestMethod("Mapping from one range to another without midpoint")]
    public void MapWithoutMidpoint() {
        var numTests = 10;
        Interval<double> input = new(-1, 1);
        Interval<double> output = new(-100, 200);
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

    [TestMethod("Mapping from one range to another with midpoint")]
    public void MapWithMidpoint() {
        Interval<float> input = new(-1, 0, 1);
        Interval<float> output = new(-100, 0, 200);
        Assert.AreEqual(-50, input.Map(output, -0.5f), 0.01);
        Assert.AreEqual(100, input.Map(output, 0.5f), 0.01);
    }

    private static void TestSingleInitialization<T>(T a, T b, T c) where T : struct, INumber<T> {
        Interval<T> range = new(a, b, c);
        Assert.AreEqual(a, range.Start, $"{a}, {b}, {c} -> {range}");
        Assert.AreEqual(c, range.End, $"{a}, {b}, {c} -> {range}");
        Assert.IsTrue(a < c ? range.Start <= range.Mid : range.Start >= range.Mid, $"{a}, {b}, {c} -> {range}");
        Assert.IsTrue(a < c ? range.End >= range.Mid : range.End <= range.Mid, $"{a}, {b}, {c} -> {range}");
        Assert.AreEqual(a, range.Start, $"{a}, {b}, {c} -> {range}");
        Assert.AreEqual(c, range.End, $"{a}, {b}, {c} -> {range}");
        Assert.AreEqual(c - a, range.Delta, $"{a}, {b}, {c} -> {range}");
    }

    private static void TestManyInitializations<T>(T min, T max, T increment) where T : struct, INumber<T> {
        var mult = T.Zero;
        for (var i = 0; i < 10; i++, mult += T.One) {
            var a = min + (increment * mult);
            var b = ((max + min) / (T.One + T.One)) - (increment * (i % 2 == 0 ? mult : -mult));
            var c = max - (increment * mult);
            TestSingleInitialization(a, b, c);
            TestSingleInitialization(a, c, b);
            TestSingleInitialization(b, a, c);
            TestSingleInitialization(b, c, a);
            TestSingleInitialization(c, a, b);
            TestSingleInitialization(c, b, a);
        }
    }

    [TestMethod("Initialization constrains values correctly")]
    public void TestInitialization() {
        TestManyInitializations(int.MinValue, 0, 36);
        TestManyInitializations(0f, 33f, 4.39);
        TestManyInitializations(0.0, -33.6, 0.001);
        TestManyInitializations<byte>(33, 55, 2);
    }

    [TestMethod]
    public void TestScale() => throw new NotImplementedException();

    [TestMethod]
    public void TestOperators() => throw new NotImplementedException();
}
