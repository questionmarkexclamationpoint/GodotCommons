namespace CommonsTest;

using Commons;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class RangeScaleTest {
    [TestMethod]
    public void TestSimpleMap() {
        float numTests = 10;
        float inMin = -1;
        float inMax = 1;
        var inDelta = (inMax - inMin) / numTests;
        var outMin = -100;
        var outMax = 200;
        var outDelta = (outMax - outMin) / numTests;
        for (float i = inMin, j = outMin; i < inMax; i += inDelta, j += outDelta) {
            var result = RangeScale.Map(
                    i,
                    inMin,
                    inMax,
                    outMin,
                    outMax
            );
            AssertMore.AreNear(
                j,
                result,
                $"RangeScale({i}, {inMin}, {inMax}, {outMin}, {outMax}) failed"
            );
        }
    }

    [TestMethod]
    public void TestShiftedMap() {
        float inMin = -1;
        float inMax = 1;
        var outMin = -100;
        float outMid = 0;
        var outMax = 200;
        var result = RangeScale.Map(
                -0.5f,
                inMin,
                0,
                inMax,
                outMin,
                outMid,
                outMax
        );
        var expected = -50;
        AssertMore.AreNear(expected, result,
                $"RangeScale(-0.5, {inMin}, {inMax}, {outMin}, {outMax}) failed");
        result = RangeScale.Map(
                0.5f,
                inMin,
                0,
                inMax,
                outMin,
                outMid,
                outMax
        );
        expected = 100;
        AssertMore.AreNear(expected, result,
                $"RangeScale(0.5, {inMin}, {inMax}, {outMin}, {outMax}) failed");
    }
}
