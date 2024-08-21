namespace QuestionMarkExclamationPoint.Commons.Graph.Generic;

public interface ITree<TNode> : ITraversable<TNode> where TNode : INode<TNode> {
    TNode? Root { get; }
}
