namespace QuestionMarkExclamationPoint.Commons.Test.Graph.BinarySearchTree;

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuestionMarkExclamationPoint.Commons.Graph;

[TestClass]
public class BinarySearchTreeTest {
    private BinarySearchTree<int> tree;

    [TestInitialize]
    public void Setup() => this.tree = [];

    [TestMethod("BST initializes correctly")]
    public void TestInitialize() {
        Assert.AreEqual(0, this.tree.Count);
        Assert.AreEqual(0, this.tree.Height);
        Assert.IsNull(this.tree.Root);
        Assert.IsFalse(this.tree.Contains(42));
    }

    [TestMethod("BST initializes correctly with inline array")]
    public void TestInitializeWithInlineArray() {
        this.tree = [1, 2, 3];
        Assert.AreEqual(3, this.tree.Count);
        Assert.AreEqual(3, this.tree.Height);
        Assert.IsNotNull(this.tree.Root);
        Assert.IsTrue(this.tree.Contains(1));
        Assert.IsTrue(this.tree.Contains(2));
        Assert.IsTrue(this.tree.Contains(3));
    }

    [TestMethod("BST add adds single value")]
    public void TestAddSingle() {
        Assert.AreEqual(0, this.tree.Count);
        var toAdd = 42;
        this.tree.Add(toAdd);
        Assert.AreEqual(1, this.tree.Count);
        Assert.AreEqual(1, this.tree.Height);
        Assert.IsNotNull(this.tree.Root);
        Assert.AreEqual(this.tree.Root.Value, toAdd);
        Assert.IsTrue(this.tree.Contains(toAdd));
    }

    [TestMethod("BST add adds many values")]
    public void TestAddMultiple() {
        this.tree.Add(42);
        Assert.AreEqual(1, this.tree.Count);
        List<int> toAdd = [69, 7, 420];
        foreach (var i in toAdd) {
            this.tree.Add(i);
        }
        Assert.AreEqual(4, this.tree.Count);
        Assert.AreEqual(3, this.tree.Height);
        Assert.IsNotNull(this.tree.Root);
        Assert.IsNotNull(this.tree.Root.Left);
        Assert.IsNotNull(this.tree.Root.Right);
        foreach (var i in toAdd) {
            Assert.IsTrue(this.tree.Contains(i));
        }
    }

    [TestMethod("BST add adds many, many values")]
    public void TestAddMany() {
        this.tree.Add(42);
        Assert.AreEqual(1, this.tree.Count);
        List<int> toAdd = [69, 7, 420, 3, 12, 99, 500, 300, 20];
        foreach (var i in toAdd) {
            this.tree.Add(i);
        }
        Assert.AreEqual(toAdd.Count + 1, this.tree.Count);
        Assert.IsNotNull(this.tree.Root);
        Assert.IsNotNull(this.tree.Root.Left);
        Assert.IsNotNull(this.tree.Root.Right);
        foreach (var i in toAdd) {
            Assert.IsTrue(this.tree.Contains(i));
        }
    }

    [TestMethod("BST contains returns false when item not found")]
    public void TestContainsNotFound() {
        this.tree.Add(42);
        Assert.IsFalse(this.tree.Contains(69));
        Assert.AreEqual(1, this.tree.Count);
        Assert.IsNotNull(this.tree.Root);
    }

    [TestMethod("BST contains returns true when item found")]
    public void TestContainsFound() {
        this.tree.Add(42);
        Assert.IsTrue(this.tree.Contains(42));
        Assert.AreEqual(1, this.tree.Count);
        Assert.IsNotNull(this.tree.Root);
    }

    [TestMethod("BST remove works on root")]
    public void TestRemoveRoot() {
        this.tree.Add(42);
        Assert.IsTrue(this.tree.Remove(42));
        Assert.AreEqual(0, this.tree.Count);
        Assert.IsNull(this.tree.Root);
    }

    [TestMethod("BST remove works on root with one child")]
    public void TestRemoveRootOneChild() {
        this.tree.Add(42);
        this.tree.Add(69);
        Assert.IsTrue(this.tree.Remove(42));
        Assert.AreEqual(1, this.tree.Count);
        Assert.IsNotNull(this.tree.Root);
        Assert.IsNull(this.tree.Root.Left);
        Assert.IsNotNull(this.tree.Root.Right);
    }

    [TestMethod("BST remove works on root with two children")]
    public void TestRemoveRootTwoChildren() {
        this.tree.Add(42);
        this.tree.Add(69);
        this.tree.Add(7);
        Assert.IsTrue(this.tree.Remove(42));
        Assert.AreEqual(2, this.tree.Count);
        Assert.IsNotNull(this.tree.Root);
        Assert.IsNull(this.tree.Root.Left);
        Assert.IsNotNull(this.tree.Root.Right);
    }

    [TestMethod("BST remove works on leaf")]
    public void TestRemoveLeaf() {
        this.tree.Add(42);
        this.tree.Add(69);
        Assert.IsTrue(this.tree.Remove(69));
        Assert.AreEqual(1, this.tree.Count);
        Assert.IsNotNull(this.tree.Root);
        Assert.IsNull(this.tree.Root.Left);
        Assert.IsNull(this.tree.Root.Right);
    }

    [TestMethod("BST remove works on node with one child")]
    public void TestRemoveOneChild() {
        this.tree.Add(42);
        this.tree.Add(69);
        this.tree.Add(7);
        Assert.IsTrue(this.tree.Remove(69));
        Assert.AreEqual(2, this.tree.Count);
        Assert.IsNotNull(this.tree.Root);
        Assert.IsNotNull(this.tree.Root.Left);
        Assert.IsNull(this.tree.Root.Right);
    }

    [TestMethod("BST remove works on node with two children")]
    public void TestRemoveTwoChildren() {
        this.tree.Add(42);
        this.tree.Add(69);
        this.tree.Add(7);
        this.tree.Add(420);
        Assert.IsTrue(this.tree.Remove(69));
        Assert.AreEqual(3, this.tree.Count);
        Assert.IsNotNull(this.tree.Root);
        Assert.IsNotNull(this.tree.Root.Left);
        Assert.IsNotNull(this.tree.Root.Right);
    }

    [TestMethod("BST remove returns false when item not found")]
    public void TestRemoveNotFound() {
        this.tree.Add(42);
        Assert.IsFalse(this.tree.Remove(69));
        Assert.AreEqual(1, this.tree.Count);
        Assert.IsNotNull(this.tree.Root);
    }

    [TestMethod("BST clear removes all items")]
    public void TestClear() {
        this.tree.Add(42);
        this.tree.Add(69);
        this.tree.Add(7);
        this.tree.Clear();
        Assert.AreEqual(0, this.tree.Count);
        Assert.IsNull(this.tree.Root);
    }

    [TestMethod("BST copy copies all items")]
    public void TestCopy() {
        var inSet = new HashSet<int>() {
            0,
            -16, 16,
            -32, -8, 8, 32,
            -48, -24, -12, -6, 6, 12, 24, 48,
            -64, -40, -28, -22, -13, -11, -7, -5, 5, 7, 11, 13, 22, 28, 40, 64
        };
        foreach (var i in inSet) {
            this.tree.Add(i);
        }
        Assert.AreEqual(inSet.Count, this.tree.Count);

        var arr = new int[inSet.Count];
        this.tree.CopyTo(arr, 0);
        var outSet = new HashSet<int>(arr);

        foreach (var i in inSet) {
            Assert.IsTrue(outSet.Contains(i), $"{i} missing");
        }
    }

    [TestMethod("BST for each iterates over every item in order")]
    public void TestForEach() {
        this.tree = [
            0,
            -16, 16,
            -32, -8, 8, 32,
            -48, -24, -12, -6, 6, 12, 24, 48,
            -64, -40, -28, -22, -13, -11, -7, -5, 5, 7, 11, 13, 22, 28, 40, 64
        ];
        var last = int.MinValue;
        foreach (var i in this.tree) {
            Assert.IsTrue(last < i, $"{i} out of order");
            last = i;
        }
    }
}
