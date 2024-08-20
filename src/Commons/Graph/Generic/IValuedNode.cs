namespace QuestionMarkExclamationPoint.Commons.Graph;

public interface IValuedNode<TNode, TValue> : INode<TNode> where TNode : IValuedNode<TNode, TValue> {
    TValue Value { get; }
}
