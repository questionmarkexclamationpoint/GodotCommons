namespace QuestionMarkExclamationPoint.Commons.Graph.Generic;

using System.Collections.Generic;

public interface IGraphNode<TNode> where TNode : IGraphNode<TNode> {
    public IEnumerable<TNode> OutgoingNodes { get; }
    public IEnumerable<TNode> IncomingNodes { get; }
    public IEnumerable<TNode> Nodes { get; }
}

public static class IGraphNodeExtensions {
    public static bool HasPath<TNode>(this TNode node, TNode target) where TNode : IGraphNode<TNode> {
        var visited = new HashSet<TNode>();
        var queue = new Queue<TNode>();
        queue.Enqueue(node);
        while (queue.Count > 0) {
            var current = queue.Dequeue();
            if (ReferenceEquals(current, target)) {
                return true;
            }
            visited.Add(current);
            foreach (var c in current.OutgoingNodes) {
                if (!visited.Contains(c)) {
                    queue.Enqueue(c);
                }
            }
        }
        return false;
    }
}
