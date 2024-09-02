namespace QuestionMarkExclamationPoint.Commons.Graph;

public static partial class BinarySearchTree {
    public enum Direction {
        Parent,
        Left,
        Right
    }

    public static Direction? GetDirection(int? comparison) {
        if (comparison == null) {
            return Direction.Parent;
        }
        if (comparison == 0) {
            return null;
        }
        return comparison < 0 ? Direction.Left : Direction.Right;
    }
}
