namespace QuestionMarkExclamationPoint.Commons.Graph.Generic;

public interface IIndexedNode<TNode, TIndex>
        : INode<TNode>
        where TNode : IIndexedNode<TNode, TIndex> {
    TNode? this[TIndex index] { get; }
}
