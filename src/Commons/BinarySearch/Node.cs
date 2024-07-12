namespace QuestionMarkExclamationPoint.Commons.BinarySearch;

using System;

// TODO generalize this to a graph node
public class Node<TValue, TNode>(TValue value)
        where TValue : IComparable<TValue>
        where TNode : Node<TValue, TNode> {
    public TValue Value { get; } = value;

    public TNode? this[Side side] => side switch {
        Side.LEFT => this.Left,
        Side.RIGHT => this.Right,
        Side.PARENT => this.Parent,
        _ => null
    };

    public TNode? Parent { get; private set; }

    private TNode? left;
    public TNode? Left {
        get => this.left;
        private set => this.SetChild(value, Side.RIGHT);
    }

    private TNode? right;
    public TNode? Right {
        get => this.right;
        private set => this.SetChild(value, Side.RIGHT);
    }

    private int count = 1;
    public int Count {
        get => this.count;
        private set {
            if (this.Parent != null) {
                this.Parent.Count += value - this.count;
            }
            this.count = value;
        }
    }

    public void Clear() {
        this.SetChild(null, Side.LEFT);
        this.SetChild(null, Side.RIGHT);
        if (this.Parent == null) {
            return;
        }
        this.Parent.SetChild(null, this.Equals(this.Parent.Left) ? Side.LEFT : Side.RIGHT);
    }

    public virtual void SetChild(TNode? child, Side side) {
        TNode? current = null;
        switch (side) {
            case Side.LEFT:
                current = this.left;
                this.left = child;
                break;
            case Side.RIGHT:
                current = this.right;
                this.right = child;
                break;
            case Side.PARENT:
            default:
                throw new ArgumentException(
                        $"${side} is not a valid child node",
                        nameof(side)
                );
        }
        if (current != null) {
            current.Parent = null;
            this.Count -= current.Count;
        }
        if (child != null) {
            child.Parent = (TNode)this;
            this.Count += child.Count;
        }
    }

    public bool Contains(TValue item) {
        var comparison = this.Value.CompareTo(item);
        if (comparison == 0) {
            return true;
        }
        var child = comparison > 0 ? this.Left : this.Right;
        return child != null && child.Contains(item);
    }
}

public class Node<TValue>(TValue value)
        : Node<TValue, Node<TValue>>(value)
        where TValue : IComparable<TValue> { }
