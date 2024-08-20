namespace QuestionMarkExclamationPoint.Commons.Graph;

public interface ITraversable<T> {
    Traverser<T> GetTraverser();

    Traverser<T> GetTraverser(Traverser.Successor<T> successor);
}
