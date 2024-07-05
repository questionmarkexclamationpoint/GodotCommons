using Microsoft.VisualStudio.TestTools.UnitTesting;
using Commons;

namespace CommonsTest;

[TestClass]
public class RandomizeTest {
    [TestMethod]
    public void TestRandom() {
        Assert.AreEqual(0, Randomize.Random(0));
    }
}
