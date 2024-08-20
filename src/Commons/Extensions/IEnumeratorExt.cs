namespace QuestionMarkExclamationPoint.Commons.Extensions;

using System.Collections.Generic;

public static class IEnumeratorExt {
    public static IEnumerable<T> Enumerate<T>(this IEnumerator<T> enumerator) {
        while (enumerator.MoveNext()) {
            yield return enumerator.Current;
        }
    }
}
