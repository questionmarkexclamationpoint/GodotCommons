namespace QuestionMarkExclamationPoint.Commons.Test;

using System;
using Commons.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class EnumExtensionsTest {
    private enum TestEnum {
        A = -2,
        B = -3,
        C = 5,
        D = 10
    }

    [TestMethod("Convert to index")]
    public void ConvertToIndex() {
        var values = Enum.GetValues<TestEnum>();
        for (var i = 0; i < values.Length; i++) {
            Assert.AreEqual(i, values[i].ToIndex(), $"{values[i]} failed");
            Assert.AreEqual(
                    values[i],
                    EnumExtensions.FromIndex<TestEnum>(values[i].ToIndex()),
                    $"{values[i]} failed"
            );
        }
    }
}
