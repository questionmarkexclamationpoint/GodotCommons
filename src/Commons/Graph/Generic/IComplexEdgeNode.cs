namespace QuestionMarkExclamationPoint.Commons.Graph.Generic;

public interface IComplexEdgeNode<TNode, TIndex, TEdge>
        : IIndexedNode<TNode, TEdge>
        where TNode : IComplexEdgeNode<TNode, TIndex, TEdge> {
    TEdge this[TIndex index] { get; }
}
