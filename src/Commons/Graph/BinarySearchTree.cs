namespace QuestionMarkExclamationPoint.Commons.Graph;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using QuestionMarkExclamationPoint.Commons.Extensions;
using QuestionMarkExclamationPoint.Commons.Graph.Generic;
using static QuestionMarkExclamationPoint.Commons.Graph.BinarySearchTree;

/// <summary>
/// A binary search tree is a binary tree where the left child is less than the parent and the
/// right child is greater than the parent.
/// </summary>
/// <typeparam name="TNode">The type of node in the tree</typeparam>
/// <typeparam name="TValue">The type of value in the tree</typeparam>
public abstract class BinarySearchTree<TNode, TValue>
        : ITree<TNode>, ICollection<TValue>
        where TValue : IComparable<TValue>
        where TNode : Node<TNode, TValue> {
    /// <summary>
    /// The root node of the tree
    /// </summary>
    /// <returns>The root node of the tree</returns>
    public TNode? Root { get; private set; }

    /// <summary>
    /// The number of nodes in the tree
    /// </summary>
    /// <returns>The number of nodes in the tree</returns>
    public int Count => this.RootDeferred(n => n.SubCount);

    /// <summary>
    /// The height of the tree
    /// </summary>
    /// <returns>The height of the tree</returns>
    public int Height => this.RootDeferred(n => n.SubHeight);

    /// <summary>
    /// Whether the tree is read-only
    /// </summary>
    /// <returns>Whether the tree is read-only</returns>
    public bool IsReadOnly => false;

    protected abstract TNode CreateNode(TValue value);

    /// <summary>
    /// Whether the tree contains the given value
    /// </summary>
    /// <param name="item">The value to check for</param>
    /// <returns>Whether the tree contains the value</returns>
    public bool Contains(TValue item)
        => this.Root != null
                && this.GetComparisonTraverser(item)
                    .Enumerate()
                    .Last()
                    .Value
                    .CompareTo(item) == 0;

    /// <summary>
    /// Adds a value to the tree
    /// </summary>
    /// <param name="item">The value to add</param>
    public void Add(TValue item) {
        if (this.Root == null) {
            this.Root = this.CreateNode(item);
            return;
        }
        // Find the parent node to add the new node to, or the node itself if it already exists
        var parent = this.GetComparisonTraverser(item).Enumerate().Last();
        var comparison = item.CompareTo(parent.Value);
        if (comparison == 0) {
            // Already exists, do nothing
            return;
        }
        var node = this.CreateNode(item);
        if (comparison > 0) {
            parent.Right = node;
        } else {
            parent.Left = node;
        }
        // After adding, rebalance the tree
        // TODO
    }

    /// <summary>
    /// Clears the tree, removing all nodes
    /// </summary>
    public void Clear() => this.Root = null;

    /// <summary>
    /// Removes a value from the tree, if present
    /// </summary>
    /// <param name="item">The value to remove</param>
    /// <returns>Whether the value was removed</returns>
    public bool Remove(TValue item) {
        if (this.Root == null) {
            return false;
        }
        var node = this.GetComparisonTraverser(item).Enumerate().Last();
        if (item.CompareTo(node.Value) != 0) {
            return false;
        }

        if (node.Parent == null) {
            var replacement = node.Right;
            if (replacement == null) {
                replacement = node.Left;
            } else {
                var leftParent = replacement;
                while (leftParent.Left != null) {
                    leftParent = leftParent.Left;
                }
                leftParent.Left = node.Left;
            }
            this.Root = replacement;
            node.Left = null;
            node.Right = null;
            return true;
        }

        var parentDirection = node.Parent.Left == node ? Direction.Left : Direction.Right;
        var leftChild = node.Left;
        var rightChild = node.Right;
        var childDirection = leftChild != null ? Direction.Left : Direction.Right;
        node.Parent[parentDirection] = childDirection == Direction.Left ? leftChild : rightChild;
        if (leftChild != null && rightChild != null) {
            var leftParent = leftChild;
            while (leftParent.Right != null) {
                leftParent = leftParent.Right;
            }
            leftParent.Right = rightChild;
        }
        node.Left = null;
        node.Right = null;
        return true;
    }

    /// <summary>
    /// Returns a traverser that will iterate over the tree in order of the values until the
    /// given value is reached
    /// </summary>
    /// <param name="item">The value to find</param>
    /// <returns>A traverser that will iterate over the tree in order of the values until the
    /// given value is reached</returns>
    internal Traverser<TNode> GetComparisonTraverser(TValue item) => new(
        this.Root,
        node => {
            if (node == null) {
                return null;
            }
            var comparison = item.CompareTo(node.Value);
            if (comparison == 0) {
                return null;
            }
            return comparison > 0 ? node.Right : node.Left;
        }
    );

    /// <summary>
    /// Returns a traverser that will iterate over the tree in order of the values
    /// </summary>
    /// <returns>A traverser that will iterate over the tree in order of the values</returns>
    public Traverser<TNode> GetTraverser()
        => Traversal.InOrder.Traverser(this);

    /// <summary>
    /// Returns a traverser that will iterate over the tree in the given order
    /// </summary>
    /// <param name="successor">The function to determine the next node to visit</param>
    /// <returns>A traverser that will iterate over the tree in the given order</returns>
    public Traverser<TNode> GetTraverser(Traverser.Successor<TNode> successor)
        => new(this.Root, successor);

    /// <summary>
    /// Returns an enumerator that will iterate over the values in the tree
    /// </summary>
    /// <returns>An enumerator that will iterate over the values in the tree</returns>
    public IEnumerator<TValue> GetEnumerator()
        => new MappedEnumerator<TNode, TValue>(
            this.GetTraverser(),
            n => n.Value
        );

    /// <summary>
    /// Returns an enumerator that will iterate over the values in the tree
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    /// <summary>
    /// Copies the values in the tree to an array
    /// </summary>
    /// <param name="array">The array to copy the values to</param>
    /// <param name="arrayIndex">The index in the array to start copying to</param>
    public void CopyTo(TValue[] array, int arrayIndex) {
        foreach (var value in this) {
            array[arrayIndex++] = value;
        }
    }

    private T? RootDeferred<T>(Func<TNode, T> getter, T? defaultValue = default) {
        if (this.Root == null) {
            return defaultValue;
        }
        return getter(this.Root);
    }
}

/// <summary>
/// A binary search tree with no generic node type
/// </summary>
/// <typeparam name="TValue">The type of value in the tree</typeparam>
public class BinarySearchTree<TValue>
        : BinarySearchTree<Node<TValue>, TValue>
        where TValue : IComparable<TValue> {
    protected override Node<TValue> CreateNode(TValue value) => new(value);
}
