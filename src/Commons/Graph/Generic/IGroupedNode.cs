namespace QuestionMarkExclamationPoint.Commons.Graph.Generic;

using System.Collections.Generic;

public interface IGroupedNode<TNode, TGroup> where TNode : IGroupedNode<TNode, TGroup> {
    IEnumerable<TNode> this[TGroup group] { get; }
}
