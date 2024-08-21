namespace QuestionMarkExclamationPoint.Commons.Graph.Generic;

public interface ITraversable<T> {
    Traverser<T> GetTraverser();

    Traverser<T> GetTraverser(Traverser.Successor<T> successor);
}
