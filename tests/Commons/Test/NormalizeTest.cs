namespace QuestionMarkExclamationPoint.Commons.Test;

using Commons;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class NormalizeTest {
    [TestMethod("Simple tanh normalization")]
    public void TestSimpleTanh() {
        var result = Normalize.Tanh(-1_000_000);
        Assert.AreEqual(-1, result, 0.01);
        result = Normalize.Tanh(1_000_000);
        Assert.AreEqual(1, result, 0.01);
        result = Normalize.Tanh(0);
        Assert.AreEqual(0, result);
    }

    [TestMethod("Clamped tanh normalization")]
    public void TestClampedTanh() {
        var result = Normalize.Tanh(2, clamp: true);
        Assert.AreEqual(1, result, 0.015);
        result = Normalize.Tanh(1, clamp: true);
        Assert.AreEqual(1, result, 0.01);
        result = Normalize.Tanh(-2, clamp: true);
        Assert.AreEqual(-1, result, 0.015);
        result = Normalize.Tanh(-1, clamp: true);
        Assert.AreEqual(-1, result, 0.01);
        result = Normalize.Tanh(0, clamp: true);
        Assert.AreEqual(0, result, 0.01);
    }

    [TestMethod("Simple sigmoid normalization")]
    public void TestSimpleSigmoid() {
        var result = Normalize.Sigmoid(-1_000_000);
        Assert.AreEqual(0, result, 0.01);
        result = Normalize.Sigmoid(1_000_000);
        Assert.AreEqual(1, result, 0.01);
        result = Normalize.Sigmoid(0);
        Assert.AreEqual(0.5, result);
    }
}
