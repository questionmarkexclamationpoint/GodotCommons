namespace QuestionMarkExclamationPoint.Commons.Graph;

public static partial class BinarySearchTree {
    public enum Direction {
        Parent,
        Left,
        Right
    }

    public static Direction? GetDirection(int? value) {
        if (value == null) {
            return Direction.Parent;
        }
        if (value == 0) {
            return null;
        }
        return value < 0 ? Direction.Left : Direction.Right;
    }
}
