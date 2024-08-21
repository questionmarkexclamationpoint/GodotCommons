namespace QuestionMarkExclamationPoint.Commons.Graph.Generic;

using System.Collections.Generic;

public interface INode<TNode> where TNode : INode<TNode> {
    IEnumerable<TNode> Neighbors { get; }
}
