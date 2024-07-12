namespace QuestionMarkExclamationPoint.Commons.Test;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Commons;
using System;

[TestClass]
public class RandomExtTest
{
    [TestMethod]
    public void TestRandom()
    {
        var random = new Random();
        Assert.AreEqual(0, random.NextFloatInRange(0, 0));
        Assert.AreEqual(0, random.NextIntInRange(0, 0));
        Assert.AreEqual(0, random.NextDoubleInRange(0, 0));
    }
}
