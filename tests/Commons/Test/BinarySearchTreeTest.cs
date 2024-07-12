namespace QuestionMarkExclamationPoint.Commons.Test;

using System;
using System.Collections.Generic;
using System.Linq;
using Commons.BinarySearch;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class BinarySearchTreeTest
{
    [TestMethod]
    public void TestContains()
    {
        var bst = new Tree<float> {
            0,
            1,
            -1
        };
        Assert.IsTrue(bst.Contains(0));
        Assert.IsTrue(bst.Contains(-1));
        Assert.IsTrue(bst.Contains(1));
        bst.Remove(-1);
        Assert.IsFalse(bst.Contains(-1));
        bst.Add(-1);
        bst.Add(-2);
        bst.Add(-1.5f);
        bst.Add(2);
        bst.Add(0.5f);
        Assert.IsTrue(bst.Root.Right.Contains(1));
        Assert.IsTrue(bst.Root.Right.Contains(0.5f));
        Assert.IsTrue(bst.Root.Right.Contains(2));
        Assert.IsFalse(bst.Root.Right.Contains(-0.5f));
    }

    [TestMethod]
    public void TestRemove()
    {
        var bst = new Tree<double> {
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
        for (var i = 0; i < 2; i++)
        {
            var sign = i % 2 == 0 ? -1 : 1;
            double[] children = [
                sign * 8,
                sign * -8,
                sign * 16,
                sign * 32,
                sign * 4
            ];
            bst = [0];
            foreach (var child in children)
            {
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
            foreach (var child in children)
            {
                Assert.IsTrue(bst.Contains(child));
            }
        }
    }

    [TestMethod]
    public void TestAdd()
    {
        var bst = new Tree<double> { 0, 8, -8 };
        //    0
        //   / \
        // -8   8
        Assert.AreEqual(0, bst.Root.Value);
        Assert.AreEqual(-8, bst.Root.Left.Value);
        Assert.AreEqual(8, bst.Root.Right.Value);
        bst.Add(16);
        bst.Add(32);
        bst.Add(4);
        //    0
        //   / \
        // -8   8
        //     / \
        //    4   16
        //          \
        //           32
        Assert.AreEqual(4, bst.Root.Right.Left.Value);
        Assert.AreEqual(16, bst.Root.Right.Right.Value);
        Assert.AreEqual(32, bst.Root.Right.Right.Right.Value);
    }

    [TestMethod]
    public void TestCopy()
    {
        var inSet = new HashSet<int>() {
            0,
            -16, 16,
            -32, -8, 8, 32,
            -48, -24, -12, -6, 6, 12, 24, 48,
            -64, -40, -28, -22, -13, -11, -7, -5, 5, 7, 11, 13, 22, 28, 40, 64
        };
        var tree = new Tree<int>();
        foreach (var i in inSet)
        {
            tree.Add(i);
        }
        Assert.AreEqual(inSet.Count, tree.Count);
        var arr = new int[inSet.Count];
        tree.CopyTo(arr, 0);
        var outSet = new HashSet<int>(arr);
        foreach (var i in inSet)
        {
            Assert.IsTrue(outSet.Contains(i), $"{i} missing");
        }
    }

    [TestMethod]
    public void TestForEach()
    {
        var tree = new Tree<int>() {
            0,
            -16, 16,
            -32, -8, 8, 32,
            -48, -24, -12, -6, 6, 12, 24, 48,
            -64, -40, -28, -22, -13, -11, -7, -5, 5, 7, 11, 13, 22, 28, 40, 64
        };
        var last = int.MinValue;
        foreach (var i in tree)
        {
            Assert.IsTrue(last < i, $"{i} out of order");
            last = i;
        }
    }
}
