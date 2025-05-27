namespace QuestionMarkExclamationPoint.Commons.Graph.Generic;

public interface ITree<TNode> : ITraversable<TNode> where TNode : IGraphNode<TNode> {
    TNode? Root { get; }
}
