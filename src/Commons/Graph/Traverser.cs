namespace QuestionMarkExclamationPoint.Commons.Graph;

using System;
using System.Collections;
using System.Collections.Generic;

public class Traverser<TNode>(
        TNode? firstNode = default,
        Traverser.Successor<TNode>? successor = null
) : IEnumerator<TNode> {
    private readonly TNode? firstNode = firstNode;

    public Traverser.Successor<TNode> Successor { get; } = successor ?? ((_) => default);

    private bool started;
    private TNode? current;
    public TNode Current {
        get {
            if (this.current == null) {
                throw new InvalidOperationException($"{nameof(this.Current)} illegally accessed");
            }
            return this.current;
        }
        private set => this.current = value;
    }

    object IEnumerator.Current => this.Current!;

    public void Dispose() => GC.SuppressFinalize(this);

    public bool MoveNext() {
        this.current = this.started
            ? this.Successor(this.Current)
            : this.firstNode;
        this.started = true;
        return this.current != null;
    }

    public void Reset() {
        this.started = false;
        this.current = default;
    }
}

public static class Traverser {
    public delegate T? Successor<T>(T current);
}
