namespace QuestionMarkExclamationPoint.Commons.Graph;

using System.Collections;
using System.Collections.Generic;

public interface IValuedNodeCollection<TNode, TValue>
        : ICollection<TValue>, ITraversable<TNode>
        where TNode : IValuedNode<TNode, TValue> {
    IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator() => new MappedEnumerator<TNode, TValue>(
        this.GetTraverser(),
        n => n.Value
    );

    IEnumerator IEnumerable.GetEnumerator() => new MappedEnumerator<TNode, TValue>(
        this.GetTraverser(),
        n => n.Value
    );
}
