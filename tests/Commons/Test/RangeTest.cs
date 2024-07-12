namespace QuestionMarkExclamationPoint.Commons.Test;

using Commons;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class RangeTest
{
    [TestMethod]
    public void TestSimpleMap()
    {
        var numTests = 10;
        double inMin = -1;
        double inMax = 1;
        var inDelta = (inMax - inMin) / numTests;
        double outMin = -100;
        double outMax = 200;
        var outDelta = (outMax - outMin) / numTests;
        for (double i = inMin, j = outMin; i < inMax; i += inDelta, j += outDelta)
        {
            var result = Range.Map(i, (inMin, inMax), (outMin, outMax));
            AssertMore.AreNear(
                j,
                result,
                $"RangeScale({i}, ({inMin}, {inMax}), ({outMin}, {outMax})) failed"
            );
        }
    }

    [TestMethod]
    public void TestMidPointMap()
    {
        float inMin = -1;
        float inMid = 0;
        float inMax = 1;
        var outMin = -100;
        float outMid = 0;
        var outMax = 200;
        var result = Range.Map(
            -0.5f,
            (inMin, inMid, inMax),
            (outMin, outMid, outMax)
        );
        var expected = -50;
        Assert.AreEqual(
                expected,
                result,
                $"RangeScale(-0.5, ({inMin}, {inMid}, {inMax}), ({outMin}, {outMid}, {outMax})) failed"
        );
        result = Range.Map(
                0.5f,
                (inMin, inMid, inMax),
                (outMin, outMid, outMax)
        );
        expected = 100;
        Assert.AreEqual(
                expected,
                result,
                $"RangeScale(0.5, ({inMin}, {inMid}, {inMax}), ({outMin}, {outMid}, {outMax})) failed"
        );
    }
}
