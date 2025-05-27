namespace QuestionMarkExclamationPoint.Commons.Graph;

using System.Collections.Generic;
using System.Linq;
using QuestionMarkExclamationPoint.Commons.Graph.Generic;

public class DiGraphNode<TNode> : IGraphNode<TNode>
    where TNode : DiGraphNode<TNode> {
    public IEnumerable<TNode> OutgoingNodes => this._outgoingNodes;
    protected HashSet<TNode> _outgoingNodes = [];

    public IEnumerable<TNode> IncomingNodes => this._incomingNodes;
    protected HashSet<TNode> _incomingNodes = [];

    public IEnumerable<TNode> Nodes => [.. this.OutgoingNodes.Concat(this.IncomingNodes).Distinct()];

    public void AddOutgoingNode(TNode node) {
        this._outgoingNodes.Add(node);
        node._incomingNodes.Add((TNode)this);
    }

    public void RemoveOutgoingNode(TNode node) {
        this._outgoingNodes.Remove(node);
        node._incomingNodes.Remove((TNode)this);
    }
}

public class DiGraphNode : DiGraphNode<DiGraphNode> { }
