namespace QuestionMarkExclamationPoint.Commons.Graph;

using System;
using System.Collections.Generic;
using static QuestionMarkExclamationPoint.Commons.Graph.BinarySearchTree;

public static partial class BinarySearchTree {
    public enum Traversal {
        InOrder,
        PreOrder,
        PostOrder,
        DepthFirst,
        BreadthFirst
    }
}


internal static class BinarySearchTreeTraversalExt {
    public static Traverser<TNode> Traverser<TNode, TValue>(
            this Traversal traversal,
            BinarySearchTree<TNode, TValue> tree
    )
            where TNode : Node<TNode, TValue>
            where TValue : IComparable<TValue> {
        if (tree.Root == null) {
            return new();
        }
        return traversal.Traverser<TNode, TValue>(tree.Root);
    }

    // TODO avoid leaving sub-tree
    public static Traverser<TNode> Traverser<TNode, TValue>(
            this Traversal traversal,
            TNode node
    )
            where TNode : Node<TNode, TValue>
            where TValue : IComparable<TValue> {
        var successor = traversal.Successor<TNode, TValue>(node);
        var firstNode = traversal switch {
            Traversal.PreOrder or Traversal.DepthFirst or Traversal.BreadthFirst => node,
            Traversal.PostOrder => Deepest<TNode, TValue>(node, true),
            Traversal.InOrder => DirectionMost<TNode, TValue>(node, Direction.Left),
            _ => throw new NotImplementedException(),
        };
        return new(firstNode, successor);
    }

    private static Traverser.Successor<TNode> Successor<TNode, TValue>(this Traversal traversal, TNode? root)
            where TValue : IComparable<TValue>
            where TNode : Node<TNode, TValue> => traversal switch {
                Traversal.InOrder => InOrderSuccessor<TNode, TValue>(root),
                Traversal.PreOrder => PreOrderSuccessor<TNode, TValue>(root),
                Traversal.PostOrder => PostOrderSuccessor<TNode, TValue>(root),
                Traversal.DepthFirst => DepthFirstSuccessor<TNode, TValue>(),
                Traversal.BreadthFirst => BreadthFirstSuccessor<TNode, TValue>(),
                _ => throw new NotImplementedException(),
            };

    private static TNode DirectionMost<TNode, TValue>(TNode node, Direction edge)
            where TValue : IComparable<TValue>
            where TNode : Node<TNode, TValue> {
        TNode? next;
        while ((next = node[edge]) != null) {
            node = next;
        }
        return node;
    }

    private static Traverser.Successor<TNode> InOrderSuccessor<TNode, TValue>(TNode? root)
            where TValue : IComparable<TValue>
            where TNode : Node<TNode, TValue> {
        static TNode? successor(TNode? current, TNode? root) {
            if (current == null) {
                return null;
            }
            if (current.Right != null) {
                return DirectionMost<TNode, TValue>(current.Right, Direction.Left);
            }
            if (current.Parent == null) {
                return null;
            }
            while (ReferenceEquals(current, current.Parent?.Right)) {
                current = current.Parent;
                if (ReferenceEquals(current, root)) {
                    return null;
                }
            }
            if (ReferenceEquals(current, current.Parent?.Left)) {
                // if (ReferenceEquals(current.Parent, root)) {
                //     return null;
                // }
                return current.Parent;
            }
            return null;
        };
        return (current) => successor(current, root);
    }

    private static Traverser.Successor<TNode> PreOrderSuccessor<TNode, TValue>(TNode? root)
            where TValue : IComparable<TValue>
            where TNode : Node<TNode, TValue> {
        static TNode? successor(TNode? current, TNode? root) {
            if (current == null) {
                return null;
            }
            if (current.Left != null) {
                return current.Left;
            }
            if (current.Right != null) {
                return current.Right;
            }
            // Backtrack up right branch
            while (current.Parent != null && ReferenceEquals(current, current.Parent.Right)) {
                current = current.Parent;
                if (ReferenceEquals(current, root)) {
                    return null;
                }
            }
            // Backtrack up left branch
            while (current.Parent != null && ReferenceEquals(current, current.Parent.Left) && current.Parent.Right == null) {
                current = current.Parent;
                if (ReferenceEquals(current, root)) {
                    return null;
                }
            }
            return current.Right;
        }
        return (current) => successor(current, root);
    }

    private static TNode Deepest<TNode, TValue>(TNode source, bool favorLeft = true)
            where TNode : Node<TNode, TValue>
            where TValue : IComparable<TValue> {
        var favored = favorLeft ? Direction.Left : Direction.Right;
        var disfavored = favorLeft ? Direction.Right : Direction.Left;
        TNode? next;
        while ((next = source[favored]) != null) {
            source = next;
            while (source[favored] == null && (next = source[disfavored]) != null) {
                source = next;
            }
        }
        return source;
    }

    private static Traverser.Successor<TNode> PostOrderSuccessor<TNode, TValue>(TNode? root)
            where TValue : IComparable<TValue>
            where TNode : Node<TNode, TValue> {
        static TNode? successor(TNode? current, TNode? root) {
            if (current == null) {
                return null;
            }
            if (ReferenceEquals(current, current.Parent?.Right)) {
                if (ReferenceEquals(current.Parent, root)) {
                    return null;
                }
                return current.Parent;
            }
            if (ReferenceEquals(current, current.Parent?.Left)) {
                if (ReferenceEquals(current.Parent, root)) {
                    return null;
                }
                current = current.Parent;
                if (current.Right != null) {
                    current = Deepest<TNode, TValue>(current.Right);
                }
                return current;
            }
            return null;
        }
        return (current) => successor(current, root);
    }

    private static Traverser.Successor<TNode> DepthFirstSuccessor<TNode, TValue>()
            where TValue : IComparable<TValue>
            where TNode : Node<TNode, TValue> {
        static TNode? successor(TNode? current, Stack<TNode> stack, HashSet<TNode> visited) {
            if (current == null) {
                return null;
            }
            _ = visited.Add(current);
            if (current.Left != null && !visited.Contains(current.Left)) {
                stack.Push(current.Left);
            }
            if (current.Right != null && !visited.Contains(current.Right)) {
                stack.Push(current.Right);
            }
            return stack.Count == 0 ? null : stack.Pop();
        }
        Stack<TNode> stack = [];
        HashSet<TNode> visited = [];
        return (current) => successor(current, stack, visited);
    }

    private static Traverser.Successor<TNode> BreadthFirstSuccessor<TNode, TValue>()
            where TValue : IComparable<TValue>
            where TNode : Node<TNode, TValue> {
        static TNode? successor(TNode? current, Queue<TNode> queue, HashSet<TNode> visited) {
            if (current == null) {
                return null;
            }
            _ = visited.Add(current);
            if (current.Left != null && !visited.Contains(current.Left)) {
                queue.Enqueue(current.Left);
            }
            if (current.Right != null && !visited.Contains(current.Right)) {
                queue.Enqueue(current.Right);
            }
            return queue.Count == 0 ? null : queue.Dequeue();
        }
        Queue<TNode> queue = [];
        HashSet<TNode> visited = [];
        return (current) => successor(current, queue, visited);
    }
}
