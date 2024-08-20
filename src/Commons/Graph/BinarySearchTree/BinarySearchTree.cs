namespace QuestionMarkExclamationPoint.Commons.Graph;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using QuestionMarkExclamationPoint.Commons.Extensions;

public abstract class BinarySearchTree<TNode, TValue>
        : ITree<TNode>, ICollection<TValue>
        where TValue : IComparable<TValue>
        where TNode : BinarySearchTree.Node<TNode, TValue> {
    public TNode? Root { get; private set; }

    public int Count => this.RootDeferred(n => n.SubCount);

    public int Height => this.RootDeferred(n => n.SubHeight);

    public bool IsReadOnly => false;

    public abstract TNode CreateNode(TValue value);

    public bool Contains(TValue item)
        => this.Root != null
                && this.GetComparisonTraverser(item)
                    .Enumerate()
                    .Last()
                    .Value
                    .CompareTo(item) == 0;

    public void Add(TValue item) {
        if (this.Root == null) {
            this.Root = this.CreateNode(item);
            return;
        }
        var parent = this.GetComparisonTraverser(item).Enumerate().Last();
        var comparison = item.CompareTo(parent.Value);
        if (comparison == 0) {
            return;
        }
        var node = this.CreateNode(item);
        if (comparison > 0) {
            parent.Right = node;
        } else {
            parent.Left = node;
        }
    }

    public void Clear() => this.Root = null;

    public bool Remove(TValue item) {
        if (this.Root == null) {
            return false;
        }
        var node = this.GetComparisonTraverser(item).Enumerate().Last();
        if (item.CompareTo(node.Value) != 0) {
            return false;
        }
        // TODO
        throw new NotImplementedException();
    }

    internal Traverser<TNode> GetComparisonTraverser(TValue item) => new(
        this.Root,
        node => {
            var comparison = item.CompareTo(node.Value);
            if (comparison == 0) {
                return null;
            }
            return comparison > 0 ? node.Right : node.Left;
        }
    );

    public Traverser<TNode> GetTraverser()
        => BinarySearchTree.Traversal.InOrder.Traverser(this);

    public Traverser<TNode> GetTraverser(Traverser.Successor<TNode> successor)
        => new(this.Root, successor);

    public IEnumerator<TValue> GetEnumerator()
        => new MappedEnumerator<TNode, TValue>(
            this.GetTraverser(),
            n => n.Value
        );

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    public void CopyTo(TValue[] array, int arrayIndex) => throw new NotImplementedException();

    private T? RootDeferred<T>(Func<TNode, T> getter, T? defaultValue = default) {
        if (this.Root == null) {
            return defaultValue;
        }
        return getter(this.Root);
    }
}

public class BinarySearchTree<TValue>
        : BinarySearchTree<BinarySearchTree.Node<TValue>, TValue>
        where TValue : IComparable<TValue> {
    public override BinarySearchTree.Node<TValue> CreateNode(TValue value) => new(value);
}
