namespace QuestionMarkExclamationPoint.Commons.Test;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuestionMarkExclamationPoint.Commons.Graph;

[TestClass]
public class WeightTreeTest {
    [TestMethod("Sample works correctly")]
    public void TestSample() {
        var tree = new WeightTree<int> {
            { 1, 1 },
            { 2, 3 },
            { 3, 4 },
            { -3, 5 },
            { -4, 6 },
            { -1, 5 }
        };
        //                     1 (1, [23, 24])
        //                    / \
        //    (5, [11, 16)) -3   2 (3, [20, 23))
        //                  / \   \
        //    (6, [0, 6)) -4   \   3 (4, [16, 20))
        //                      \
        //                      -1 (5, [6, 11))
        Assert.AreEqual(1, tree.Sample(23 / 24f));
        Assert.AreEqual(1, tree.Sample(1));

        Assert.AreEqual(2, tree.Sample(22.9f / 24f));
        Assert.AreEqual(2, tree.Sample(20 / 24f));

        Assert.AreEqual(3, tree.Sample(19.9f / 24f));
        Assert.AreEqual(3, tree.Sample(16 / 24f));

        Assert.AreEqual(-3, tree.Sample(15.9f / 24f));
        Assert.AreEqual(-3, tree.Sample(11 / 24f));

        Assert.AreEqual(-1, tree.Sample(10.9f / 24f));
        Assert.AreEqual(-1, tree.Sample(6 / 24f));

        Assert.AreEqual(-4, tree.Sample(5.9f / 24f));
        Assert.AreEqual(-4, tree.Sample(0));
    }
}
