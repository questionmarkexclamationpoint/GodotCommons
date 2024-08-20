namespace QuestionMarkExclamationPoint.Commons.Graph;

public interface ITree<TNode> : ITraversable<TNode> where TNode : INode<TNode> {
    TNode? Root { get; }
}
