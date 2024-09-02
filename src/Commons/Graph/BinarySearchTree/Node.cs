namespace QuestionMarkExclamationPoint.Commons.Graph;

using System;
using System.Collections.Generic;
using System.Linq;
using QuestionMarkExclamationPoint.Commons.Graph.Generic;

public static partial class BinarySearchTree {
    public class Node<TNode, TValue>
            : IIndexedNode<TNode, Direction>,
            IGroupedNode<TNode, Relationship>,
            IValuedNode<TNode, TValue>
            where TValue : IComparable<TValue>
            where TNode : Node<TNode, TValue> {
        internal Node(TValue value) => this.Value = value;

        public TValue Value { get; private init; }

        private TNode? parent;
        public TNode? Parent {
            get => this.parent;
            private set {
                if (value != null) {
                    this.Root = value.Root;
                } else {
                    this.root = null;
                }
                this.parent = value;
            }
        }

        private TNode? left;
        public TNode? Left {
            get => this.left;
            internal set => this.SetChild(value, Direction.Left);
        }

        private TNode? right;
        public TNode? Right {
            get => this.right;
            internal set => this.SetChild(value, Direction.Right);
        }

        private TNode? root;
        internal TNode Root {
            get => this.root ?? (TNode)this;
            private set {
                this.root = value == this ? null : value;
                if (this.Parent != null && this.Parent.Root != value) {
                    this.Parent.Root = value;
                }
                if (this.Left != null && this.Left.Root != value) {
                    this.Left.Root = value;
                }
                if (this.Right != null && this.Right.Root != value) {
                    this.Right.Root = value;
                }
            }
        }

        private int count = 1;
        internal int SubCount {
            get => this.count;
            private set {
                var oldValue = this.count;
                this.count = value;
                if (this.Parent != null) {
                    this.Parent.SubCount += this.count - oldValue;
                }
            }
        }

        // TODO cache
        internal int SubHeight => 1 + Math.Max(this.Left?.SubHeight ?? 0, this.Right?.SubHeight ?? 0);

        internal int SubBalance => this.Right?.SubHeight ?? 0 - this.Left?.SubHeight ?? 0;

        public TNode? this[Direction index] {
            get => index switch {
                Direction.Left => this.Left,
                Direction.Right => this.Right,
                Direction.Parent => this.Parent,
                _ => throw new ArgumentException(
                        $"Invalid {nameof(Direction)} \"{index}\"",
                        nameof(index)
                )
            };
            internal set {
                switch (index) {
                    case Direction.Left:
                        this.Left = value;
                        break;
                    case Direction.Right:
                        this.Right = value;
                        break;
                    case Direction.Parent:
                        throw new ArgumentException(
                            $"Cannot set {nameof(Direction)} \"{index}\"",
                            nameof(index)
                        );
                    default:
                        throw new ArgumentOutOfRangeException(
                            nameof(index),
                            index,
                            $"Invalid {nameof(Direction)} \"{index}\""
                        );
                }
            }
        }

        public IEnumerable<TNode> this[Relationship group] {
            get {
                IEnumerable<TNode> result = [];
                switch (group) {
                    case Relationship.Parent:
                        if (this.Parent != null) {
                            result = result.Append(this.Parent);
                        }
                        break;
                    case Relationship.Ancestors:
                        if (this.Parent != null) {
                            result = result.Append(this.Parent).Concat(this.Parent[group]);
                        }
                        break;
                    case Relationship.Children:
                        if (this.Left != null) {
                            result = result.Append(this.Left);
                        }
                        if (this.Right != null) {
                            result = result.Append(this.Right);
                        }
                        break;
                    case Relationship.Descendents:
                        if (this.Left != null) {
                            result = result.Append(this.Left).Concat(this.Left[group]);
                        }
                        if (this.Right != null) {
                            result = result.Append(this.Right).Concat(this.Right[group]);
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(
                                nameof(group),
                                $"Invalid {nameof(Relationship)} \"{group}\""
                        );
                }
                return result;
            }
        }

        public IEnumerable<TNode> Neighbors {
            get {
                IEnumerable<TNode> result = [];
                if (this.Parent != null) {
                    result = result.Append(this.Parent);
                }
                if (this.Left != null) {
                    result = result.Append(this.Left);
                }
                if (this.Right != null) {
                    result = result.Append(this.Right);
                }
                return result;
            }
        }

        internal virtual void SetChild(TNode? node, Direction side) {
            TNode? previous = null;
            switch (side) {
                case Direction.Left:
                    previous = this.Left;
                    this.left = node;
                    break;
                case Direction.Right:
                    previous = this.Right;
                    this.right = node;
                    break;
                case Direction.Parent:
                default:
                    throw new ArgumentException(
                            $"{side} is not a valid child",
                            nameof(side)
                    );
            }
            if (previous != null) {
                previous.Parent = null;
                this.SubCount -= previous.SubCount;
            }
            if (node != null) {
                node.Parent = (TNode)this;
                this.SubCount += node.SubCount;
            }
        }
    }

    public class Node<TValue> : Node<Node<TValue>, TValue>
            where TValue : IComparable<TValue> {
        internal Node(TValue value) : base(value) { }
    }
}
