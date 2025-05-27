namespace QuestionMarkExclamationPoint.Commons.Graph.Generic;

using System.Collections.Generic;

public interface IGraph<TNode> where TNode : IGraphNode<TNode> {
    public IEnumerable<TNode> Nodes { get; }
    public TNode AddNode(TNode node);
    public bool RemoveNode(TNode node);
}
