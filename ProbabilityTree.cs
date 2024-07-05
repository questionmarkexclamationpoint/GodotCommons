using System;
using System.Collections;
using System.Collections.Generic;

namespace Commons;

public abstract class ProbabilityTree<T> : ISet<T>, ICollection<T>, IEnumerable<T>, IEnumerable {
    private class Node {
        public readonly T value;
        public Node left;
        public Node right;
        public Node parent;
        public float weight = 0;

        public Node(T value) {
            this.value = value;
        }
    }

    public T Sample(float probability) {
        // TODO
        throw new NotImplementedException();
    }

    private Node root;

    protected abstract float Compare(T left, T right);

    protected abstract float Weight(T value);

    private int count = 0;
    public int Count => count;

    public bool IsReadOnly => false;

    public IEnumerator<T> GetEnumerator() {
        return new Enumerator(root);
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return new Enumerator(root);
    }

    public void Add(T item) {
        ((ISet<T>)this).Add(item);
    }

    bool ISet<T>.Add(T item) {
        if (root == null) {
            root = new Node(item);
            count++;
            return true;
        }
        return AddInternal(root, item);
    }

    private bool AddInternal(Node n, T value) {
        float comparison = Compare(n.value, value);
        if (comparison == 0) {
            // Value already exists, do nothing
            return false;
        }
        if (comparison < 0 && n.left == null) {
            float w = Weight(value);
            n.weight += w;
            n.left = new Node(value) { parent = n, weight = w };
            count++;
            return true;
        } else if (n.right == null) {
            float w = Weight(value);
            n.weight += w;
            n.right = new Node(value) { parent = n, weight = w };
            count++;
            return true;
        }
        return AddInternal(comparison < 0 ? n.left : n.right, value);
    }

    public void ExceptWith(IEnumerable<T> other) {
        throw new System.NotImplementedException();
    }

    public void IntersectWith(IEnumerable<T> other) {
        throw new System.NotImplementedException();
    }

    public bool IsProperSubsetOf(IEnumerable<T> other) {
        throw new System.NotImplementedException();
    }

    public bool IsProperSupersetOf(IEnumerable<T> other) {
        throw new System.NotImplementedException();
    }

    public bool IsSubsetOf(IEnumerable<T> other) {
        throw new System.NotImplementedException();
    }

    public bool IsSupersetOf(IEnumerable<T> other) {
        throw new System.NotImplementedException();
    }

    public bool Overlaps(IEnumerable<T> other) {
        throw new System.NotImplementedException();
    }

    public bool SetEquals(IEnumerable<T> other) {
        throw new System.NotImplementedException();
    }

    public void SymmetricExceptWith(IEnumerable<T> other) {
        throw new System.NotImplementedException();
    }

    public void UnionWith(IEnumerable<T> other) {
        throw new System.NotImplementedException();
    }

    public void Clear() {
        throw new System.NotImplementedException();
    }

    public bool Contains(T item) {
        throw new System.NotImplementedException();
    }

    public void CopyTo(T[] array, int arrayIndex) {
        throw new System.NotImplementedException();
    }

    public bool Remove(T item) {

        throw new System.NotImplementedException();
    }

    private class Enumerator : IEnumerator<T> {
        public Enumerator(Node root, Traversal traversal = Traversal.INORDER) {
            this.root = root;
            Traversal = traversal;
        }
        private readonly Node root;
        private Node current;
        public T Current {
            get {
                if (root == null) {
                    return default(T);
                }
                if (current == null) {
                    Reset();
                }
                return current.value;
            }
        }

        object IEnumerator.Current => Current;

        public Traversal Traversal { get; init; }

        public void Dispose() { }

        public bool MoveNext() {
            if (current == null) {
                return false;
            }
            switch (Traversal) {
                case Traversal.PREORDER:
                    if (current.parent == null) {
                        current = null;
                        return false;
                    }
                    if (current.left != null) {
                        current = current.left;
                        return true;
                    }
                    if (current == current.parent.left) {
                        if (current.parent.right != null) {
                            current = current.parent.right;
                            return true;
                        } else {
                            // TODO
                            return false;
                        }
                    } else {
                        // TODO
                        return false;
                    }
                case Traversal.INORDER:
                    if (current.right == null) {
                        if (current == current.parent.right) {
                            current = null;
                            return false;
                        }
                        current = current.parent;
                        return true;
                    }
                    current = current.right;
                    while (current.left != null) {
                        current = current.left;
                    }
                    return true;
                case Traversal.POSTORDER:
                    return false; // TODO
            }
            throw new System.NotImplementedException();
        }

        public void Reset() {
            current = null;
            switch (Traversal) {
                case Traversal.PREORDER:
                    current = root;
                    break;
                case Traversal.INORDER:
                case Traversal.POSTORDER:
                    if (root == null) {
                        return;
                    }
                    current = root;
                    while (current.left != null) {
                        current = current.left;
                    }
                    break;
            }
        }
    }

    enum Traversal {
        PREORDER,
        INORDER,
        POSTORDER
    }
}
