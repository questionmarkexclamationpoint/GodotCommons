namespace CommonsTest;

using Commons;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class NormalizeTest {
    [TestMethod]
    public void TestSimpleTanh() {
        var result = Normalize.Tanh(-1_000_000);
        AssertMore.AreNear(-1, result);
        result = Normalize.Tanh(1_000_000);
        AssertMore.AreNear(1, result);
        result = Normalize.Tanh(0);
        Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void TestClampedTanh() {
        var result = Normalize.Tanh(2, clamp: true);
        Assert.AreEqual(1, result);
        result = Normalize.Tanh(1, clamp: true);
        Assert.AreEqual(1, result);
        result = Normalize.Tanh(-2, clamp: true);
        Assert.AreEqual(-1, result);
        result = Normalize.Tanh(-1, clamp: true);
        Assert.AreEqual(-1, result);
        result = Normalize.Tanh(0, clamp: true);
        Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void TestSimpleSigmoid() {
        var result = Normalize.Sigmoid(-1_000_000);
        AssertMore.AreNear(0, result);
        result = Normalize.Sigmoid(1_000_000);
        AssertMore.AreNear(1, result);
        result = Normalize.Sigmoid(0);
        Assert.AreEqual(0.5, result);
    }

    [TestMethod]
    public void TestClampedSigmoid() {
        var result = Normalize.Sigmoid(2, clamp: true);
        Assert.AreEqual(1, result);
        result = Normalize.Sigmoid(1, clamp: true);
        Assert.AreEqual(1, result);
        result = Normalize.Sigmoid(-2, clamp: true);
        Assert.AreEqual(0, result);
        result = Normalize.Sigmoid(-1, clamp: true);
        Assert.AreEqual(0, result);
        // TODO these are broken
        // result = Normalize.Sigmoid(0, clamp: true);
        // Assert.AreEqual(0.5, result);
    }
}
