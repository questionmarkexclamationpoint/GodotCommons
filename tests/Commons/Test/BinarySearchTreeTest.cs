namespace QuestionMarkExclamationPoint.Commons.Test;

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuestionMarkExclamationPoint.Commons.Graph;

[TestClass]
public class BinarySearchTreeTest {
    [TestMethod]
    public void TestConstructor() {
        var bst = new BinarySearchTree<int>();
        Assert.AreEqual(0, bst.Count);
        Assert.AreEqual(0, bst.Height);
        Assert.IsNull(bst.Root);
        Assert.IsFalse(bst.Contains(42));
    }

    [TestMethod]
    public void TestAddOne() {
        var bst = new BinarySearchTree<int>();
        Assert.AreEqual(0, bst.Count);
        var toAdd = 42;
        bst.Add(toAdd);
        Assert.AreEqual(1, bst.Count);
        Assert.AreEqual(1, bst.Height);
        Assert.IsNotNull(bst.Root);
        Assert.AreEqual(bst.Root.Value, toAdd);
        Assert.IsTrue(bst.Contains(toAdd));
    }

    [TestMethod]
    public void TestAddMore() {
        var bst = new BinarySearchTree<int> { 42 };
        Assert.AreEqual(1, bst.Count);
        List<int> toAdd = [69, 7, 420];
        foreach (var i in toAdd) {
            bst.Add(i);
        }
        var trav = bst.GetTraverser();
        // PrintTree(bst);
        Assert.AreEqual(4, bst.Count);
        Assert.AreEqual(3, bst.Height);
        Assert.IsNotNull(bst.Root);
        Assert.IsNotNull(bst.Root.Left);
        Assert.IsNotNull(bst.Root.Right);
        foreach (var i in toAdd) {
            Assert.IsTrue(bst.Contains(i));
        }
    }

    [TestMethod]
    public void TestAddEvenMore() {
        var bst = new BinarySearchTree<int> { 42 };
        Assert.AreEqual(1, bst.Count);
        List<int> toAdd = [69, 7, 420, 3, 12, 99, 500, 300, 20];
        foreach (var i in toAdd) {
            bst.Add(i);
        }
        Assert.AreEqual(toAdd.Count + 1, bst.Count);
        Assert.IsNotNull(bst.Root);
        Assert.IsNotNull(bst.Root.Left);
        Assert.IsNotNull(bst.Root.Right);
        foreach (var i in toAdd) {
            Assert.IsTrue(bst.Contains(i));
        }
    }

    [TestMethod]
    public void TestRemove() {
        var bst = new BinarySearchTree<double> {
            0,
            8,
            -8
        };
        //    0
        //   / \
        // -8   8
        Assert.IsTrue(bst.Remove(0));
        Assert.IsTrue(bst.Contains(8));
        Assert.IsTrue(bst.Remove(8));
        Assert.IsFalse(bst.Contains(8));
        Assert.IsFalse(bst.Remove(8));
        Assert.IsTrue(bst.Remove(-8));
        Assert.IsTrue(bst.Count == 0);
        for (var i = 0; i < 2; i++) {
            var sign = i % 2 == 0 ? -1 : 1;
            double[] children = [
                sign * 8,
                sign * -8,
                sign * 16,
                sign * 32,
                sign * 4
            ];
            bst = [0];
            foreach (var child in children) {
                bst.Add(child);
            }
            //    0
            //   / \
            // -8   8
            //     / \
            //    4   16
            //          \
            //           32
            Assert.IsTrue(bst.Remove(0));
            Assert.IsFalse(bst.Contains(0));
            foreach (var child in children) {
                Assert.IsTrue(bst.Contains(child));
            }
        }
    }

    [TestMethod]
    public void TestCopy() {
        var inSet = new HashSet<int>() {
            0,
            -16, 16,
            -32, -8, 8, 32,
            -48, -24, -12, -6, 6, 12, 24, 48,
            -64, -40, -28, -22, -13, -11, -7, -5, 5, 7, 11, 13, 22, 28, 40, 64
        };
        var tree = new BinarySearchTree<int>();
        foreach (var i in inSet) {
            tree.Add(i);
        }
        Assert.AreEqual(inSet.Count, tree.Count);
        var arr = new int[inSet.Count];
        tree.CopyTo(arr, 0);
        var outSet = new HashSet<int>(arr);
        foreach (var i in inSet) {
            Assert.IsTrue(outSet.Contains(i), $"{i} missing");
        }
    }

    [TestMethod]
    public void TestForEach() {
        var tree = new BinarySearchTree<int>() {
            0,
            -16, 16,
            -32, -8, 8, 32,
            -48, -24, -12, -6, 6, 12, 24, 48,
            -64, -40, -28, -22, -13, -11, -7, -5, 5, 7, 11, 13, 22, 28, 40, 64
        };
        var last = int.MinValue;
        foreach (var i in tree) {
            Assert.IsTrue(last < i, $"{i} out of order");
            last = i;
        }
    }
}
