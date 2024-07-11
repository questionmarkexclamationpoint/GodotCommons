namespace Commons.BinarySearch;

using System;
using System.Collections;
using System.Collections.Generic;

public abstract partial class Tree<TValue, TNode> {
    public sealed class NodeEnumerator(TNode? root) : IEnumerator<TNode> {
        private readonly TNode? root = root;

        private TNode? current;
        public TNode Current {
            get {
                if (this.current == null) {
                    throw new InvalidOperationException($"{nameof(this.Current)} illegally accessed");
                }
                return this.current;
            }
            private set => this.current = value;
        }

        object IEnumerator.Current => this.Current;

        public void Dispose() { }

        public bool MoveNext() {
            if (this.current == null) {
                this.current = LeftMostChild(this.root);
                return this.current != null;
            }
            if (this.Current.Right != null) {
                this.Current = LeftMostChild(this.Current.Right);
                return true;
            }
            if (this.Current?.Parent == null) {
                return false;
            }
            if (this.Current.Equals(this.Current.Parent.Left)) {
                this.Current = this.Current.Parent;
                return true;
            }
            return false;
        }

        public void Reset() => this.current = null;

        private static TN LeftMostChild<TN>(TN node) where TN : TNode? {
            while (node?.Left != null) {
                node = (TN)node.Left;
            }
            return node;
        }
    }
}
