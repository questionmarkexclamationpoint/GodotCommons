namespace QuestionMarkExclamationPoint.Commons.Test;

using System;
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

    [TestMethod]
    public void TestScale() => throw new NotImplementedException();

    [TestMethod]
    public void TestOperators() => throw new NotImplementedException();
}
