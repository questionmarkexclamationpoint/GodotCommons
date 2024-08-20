namespace QuestionMarkExclamationPoint.Commons.Graph;

using System.Collections.Generic;

public interface INode<TNode> where TNode : INode<TNode> {
    IEnumerable<TNode> Neighbors { get; }
}
