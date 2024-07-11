namespace Commons.BinarySearch;

using System;
using System.Collections;
using System.Collections.Generic;

public abstract partial class Tree<TValue, TNode>(TNode? root = null)
        : ICollection<TValue>, IEnumerable<TValue>
        where TNode : Node<TValue, TNode>
        where TValue : IComparable<TValue> {

    public TNode? Root { get; private set; } = root;

    public int Count => this.Root == null ? 0 : this.Root.Count;

    public bool IsReadOnly => false;

    public abstract TNode Create(TValue item);

    public TNode? GetNode(TValue item, bool lastParent = false) {
        if (this.Root == null) {
            return null;
        }
        return GetNode(this.Root, item, lastParent);
    }

    private static TNode? GetNode(TNode current, TValue item, bool lastParent) {
        var comparison = current.Value.CompareTo(item);
        if (comparison == 0) {
            return current;
        }
        var childSide = comparison > 0 ? Side.LEFT : Side.RIGHT;
        var child = childSide == Side.LEFT ? current.Left : current.Right;
        if (child == null) {
            return lastParent ? current : null;
        }
        return GetNode(child, item, lastParent);
    }

    public void Add(TValue item) => this.Add(this.Create(item));

    private void Add(TNode node) {
        var parent = this.GetNode(node.Value, true);
        if (parent == null) {
            this.Root = node;
            return;
        }
        var comparison = parent.Value.CompareTo(node.Value);
        if (comparison == 0) {
            return;
        }
        parent.SetChild(node, comparison > 0 ? Side.LEFT : Side.RIGHT);
    }

    public bool Remove(TValue item) {
        var node = this.GetNode(item);
        if (node == null) {
            return false;
        }
        if (node.Value.CompareTo(item) != 0) {
            return false;
        }
        var wasRoot = this.Root == node;
        node = ResetChild(node);
        if (wasRoot) {
            this.Root = node;
        }
        return true;
    }

    private static TNode? ResetChild(TNode? node) {
        if (node == null) {
            return node;
        }
        var left = node.Left;
        var right = node.Right;
        node.Clear();
        node = left ?? right;
        if (left != null && right != null) {
            var parent = GetNode(left, right.Value, true) ?? left;
            parent.SetChild(right, Side.RIGHT);
        }
        return node;
    }

    public void Clear() => this.Root = null;

    public bool Contains(TValue item) => this.GetNode(item) != null;

    public void CopyTo(TValue[] array, int arrayIndex) {
        var offset = 0;
        foreach (var value in this) {
            array[arrayIndex + offset++] = value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    public IEnumerator<TValue> GetEnumerator() => new ValueEnumerator(this.Root);

    public IEnumerator<TNode> GetNodeEnumerator() => new NodeEnumerator(this.Root);
}

public class Tree<TValue>
        : Tree<TValue, Node<TValue>>
        where TValue : IComparable<TValue> {
    public override Node<TValue> Create(TValue item) => new(item);
}
