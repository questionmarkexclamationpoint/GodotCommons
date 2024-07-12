namespace QuestionMarkExclamationPoint.Commons.BinarySearch;

using System.Collections;
using System.Collections.Generic;

public abstract partial class Tree<TValue, TNode> {
    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    public IEnumerator<TValue> GetEnumerator() => new ValueEnumerator(this.Root);

    public sealed class ValueEnumerator(TNode? root) : IEnumerator<TValue> {
        private readonly NodeEnumerator nodeEnumerator = new(root);

        public TValue Current => this.nodeEnumerator.Current.Value;

        object IEnumerator.Current => this.Current;

        public void Dispose() => this.nodeEnumerator.Dispose();

        public bool MoveNext() => this.nodeEnumerator.MoveNext();

        public void Reset() => this.nodeEnumerator.Reset();
    }
}
