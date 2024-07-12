namespace CommonsTest;

using System;
using Commons;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class EnumExtTest {
    private enum TestEnum {
        A = -2,
        B = -3,
        C = 5,
        D = 10
    }

    [TestMethod]
    public void TestToAndFromLong() {
        var values = Enum.GetValues<TestEnum>();
        for (var i = 0; i < values.Length; i++) {
            Assert.AreEqual(i, values[i].ToIndex(), $"{values[i]} failed");
            Assert.AreEqual(
                    values[i],
                    EnumExt.FromIndex<TestEnum>(values[i].ToIndex()),
                    $"{values[i]} failed"
            );
        }
    }
}
