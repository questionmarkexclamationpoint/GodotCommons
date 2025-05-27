namespace QuestionMarkExclamationPoint.Commons.Graph;

using System.Collections.Generic;
using QuestionMarkExclamationPoint.Commons.Graph.Generic;

public class Digraph<TNode> : IGraph<TNode> where TNode : IGraphNode<TNode> {
    public IEnumerable<TNode> Nodes => this._nodesSet;
    private HashSet<TNode> _nodesSet = [];

    public TNode AddNode(TNode node) => this._nodesSet.Add(node) ? node : throw new System.InvalidOperationException("Node already exists in the graph.");
    public bool RemoveNode(TNode node) => this._nodesSet.Remove(node);
}
