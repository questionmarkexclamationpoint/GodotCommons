namespace QuestionMarkExclamationPoint.Commons.Graph.Generic;

public interface IValuedNode<TNode, TValue> : INode<TNode> where TNode : IValuedNode<TNode, TValue> {
    TValue Value { get; }
}
