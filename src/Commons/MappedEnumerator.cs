namespace QuestionMarkExclamationPoint.Commons;

using System;
using System.Collections;
using System.Collections.Generic;

public class MappedEnumerator<TIn, TOut>(IEnumerator<TIn> sourceEnumerator, Func<TIn, TOut> transformFunc) : IEnumerator<TOut> {
    private readonly IEnumerator<TIn> inEnum = sourceEnumerator;
    private readonly Func<TIn, TOut> mapOut = transformFunc;

    public TOut Current => this.mapOut(this.inEnum.Current);

    object IEnumerator.Current => this.mapOut(this.inEnum.Current)!;

    public void Dispose() {
        this.inEnum.Dispose();
        GC.SuppressFinalize(this);
    }

    public bool MoveNext() => this.inEnum.MoveNext();

    public void Reset() => this.inEnum.Reset();
}
