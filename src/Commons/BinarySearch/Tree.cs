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

    protected abstract TNode Create(TValue item);

    public TNode? GetNode(TValue item, bool lastNonNull = false)
            => this.Traverse((curr) => ValueDirectionFromNode(
                    curr,
                    item
            ), lastNonNull);

    protected TNode? Traverse(Func<TNode, Side?> nextSide, bool lastNonNull = false) {
        if (this.Root == null) {
            return null;
        }
        return Traverse(this.Root, nextSide, lastNonNull);
    }

    private static TNode? GetNode(TNode current, TValue item, bool lastNonNull = false)
            => Traverse(current, (node) => ValueDirectionFromNode(node, item), lastNonNull);

    private static TNode? Traverse(TNode? current, Func<TNode, Side?> nextSide, bool lastNonNull) {
        if (current == null) {
            return null;
        }
        var side = nextSide(current);
        if (side == null) {
            return current;
        }
        var child = current[(Side)side];
        if (child == null) {
            return lastNonNull ? current : null;
        }
        return Traverse(child, nextSide, lastNonNull);
    }

    public void Add(TValue item) => this.Add(this.Create(item));

    protected void Add(TNode node) {
        var parent = this.GetNode(node.Value, true);
        if (parent == null) {
            this.Root = node;
            return;
        }
        var side = ValueDirectionFromNode(parent, node.Value);
        if (side == null) {
            return;
        }
        parent.SetChild(node, (Side)side);
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

    private static Side? ValueDirectionFromNode(TNode node, TValue value)
            => SideFromInt(value.CompareTo(node.Value));

    private static Side? SideFromInt(int comparison) {
        if (comparison == 0) {
            return null;
        }
        return comparison < 0 ? Side.LEFT : Side.RIGHT;
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    public IEnumerator<TValue> GetEnumerator() => new ValueEnumerator(this.Root);

    public IEnumerator<TNode> GetNodeEnumerator() => new NodeEnumerator(this.Root);
}

public class Tree<TValue>
        : Tree<TValue, Node<TValue>>
        where TValue : IComparable<TValue> {
    protected override Node<TValue> Create(TValue item) => new(item);
}
