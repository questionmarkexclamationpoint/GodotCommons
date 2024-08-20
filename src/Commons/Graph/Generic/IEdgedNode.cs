namespace QuestionMarkExclamationPoint.Commons.Graph;

public interface IEdgedNode<TNode, TIndex, TEdge>
        : IIndexedNode<TNode, TEdge>
        where TNode : IEdgedNode<TNode, TIndex, TEdge> {
    TEdge this[TIndex index] { get; }
}
