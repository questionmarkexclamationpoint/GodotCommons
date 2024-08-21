namespace QuestionMarkExclamationPoint.Commons.Graph;

using System;
using static QuestionMarkExclamationPoint.Commons.Graph.BinarySearchTree;

public class WeightTree<TValue>
        : BinarySearchTree<WeightTree.Node<TValue>, TValue>
        where TValue : IComparable<TValue> {
    public override WeightTree.Node<TValue> CreateNode(TValue value) => new(value);

    public void Add(TValue item, float weight) => throw new NotImplementedException();

    public TValue? Sample(float probability) => throw new NotImplementedException();
    //     if (this.Root == null) {
    //         throw new InvalidOperationException();
    //     }
    //     var index = Math.Clamp(probability, 0, 1) * this.Root.WeightSum;
    //     Side? traverse(Node node) {
    //         if (index >= node.WeightSum - node.Weight) {
    //             return null;
    //         }
    //         if (node.Left != null && index < node.Left.WeightSum) {
    //             return Side.LEFT;
    //         } else if (node.Left != null) {
    //             index -= node.Left.WeightSum;
    //         }
    //         if (node.Right != null) {
    //             return Side.RIGHT;
    //         }
    //         // TODO possible to reach this through rounding error, probably just return null here?
    //         throw new InvalidOperationException($"index {probability * this.Root.WeightSum} resulted in an illegal state");
    //     }
    //     return this.Traverse(traverse, false)!.Value;
    // }
}

public static class WeightTree {
    public class Node<TValue>(TValue value, float weight = 1) : Node<Node<TValue>, TValue>(value) where TValue : IComparable<TValue> {
        public float Weight { get; } = weight;

        private float weightSum = weight;
        internal float WeightSum {
            get => this.weightSum;
            private set {
                if (this.Parent != null) {
                    this.Parent.WeightSum += value - this.weightSum;
                }
                this.weightSum = value;
            }
        }

        internal override void SetChild(Node<TValue>? child, Direction direction) {
            var oldWeightSum = direction switch {
                Direction.Left => this.Left == null ? 0 : this.Left.WeightSum,
                Direction.Right => this.Right == null ? 0 : this.Right.WeightSum,
                Direction.Parent => 0,
                _ => 0
            };
            var increment = (child == null ? 0 : child.WeightSum) - oldWeightSum;
            base.SetChild(child, direction);
            this.WeightSum += increment;
        }
    }
}
