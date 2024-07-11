namespace Commons.BinarySearch;

using System;
using System.Collections;
using System.Collections.Generic;

public abstract partial class Tree<TValue, TNode> {
    public sealed class NodeEnumerator(TNode? root) : IEnumerator<TNode> {
        private readonly TNode? root = root;

        private bool started;

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

        private static TNode? Leftmost(TNode? node)
            => Traverse(node, (_) => Side.LEFT, true);

        public bool MoveNext() {
            if (!this.started) {
                this.started = true;
                this.current = Leftmost(this.root);
                return this.current != null;
            }
            if (this.Current.Right != null) {
                this.current = Leftmost(this.Current.Right);
                return true;
            }
            if (this.Current.Parent == null) {
                return false;
            }
            if (this.Current.Equals(this.Current.Parent.Left)) {
                this.Current = this.Current.Parent;
                return true;
            }
            this.current = Traverse(
                    this.Current,
                    (n) => n == n.Parent?.Right ? Side.PARENT : null,
                    false
            )?.Parent;
            return this.current != null;
        }

        public void Reset() => this.started = false;
    }
}
