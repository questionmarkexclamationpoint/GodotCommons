namespace QuestionMarkExclamationPoint.Commons.Test;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Commons.Extensions;
using System;

[TestClass]
public class RandomExtensionsTest {
    private static void TestNextInRange(Random random, double a, double b) {
        // make variance half the difference, and expected the mean of a and b,
        // so that any correctly generated random value will be in the expected range
        var variance = Math.Abs(b - a) / 2;
        var expected = (a + b) / 2;
        var f = random.NextSingleInRange((float)a, (float)b);
        Assert.AreEqual(expected, f, variance);
        Assert.AreEqual(expected, random.NextFloatInRange(new((float)a, f, (int)b)), variance);
        var i = random.NextIntInRange((int)a, (int)b);
        Assert.AreEqual(expected, i, variance);
        Assert.AreEqual(expected, random.NextIntInRange(new((int)a, i, (int)b)), variance);
        var d = random.NextDoubleInRange(a, b);
        Assert.AreEqual(expected, d, variance);
        Assert.AreEqual(expected, random.NextDoubleInRange(new((int)a, d, (int)b)), variance);
    }

    [TestMethod("Next in range")]
    public void TestNextInRange() {
        var random = new Random();
        TestNextInRange(random, 0, 0);
        TestNextInRange(random, -100, 22.2);
        TestNextInRange(random, 100, -2222);
        TestNextInRange(random, 100, 30);
    }
}
