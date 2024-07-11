namespace Commons;

using System;
using Commons.BinarySearch;

public class WeightTree<TValue> : Tree<TValue, WeightTree<TValue>.Node> where TValue : IComparable<TValue> {
    protected override Node Create(TValue item) => new(item);

    public void Add(TValue value, float weight) => this.Add(new Node(value, weight));

    public TValue? Sample(float probability) {
        if (this.Root == null) {
            throw new InvalidOperationException();
        }
        var index = Math.Clamp(probability, 0, 1) * this.Root.WeightSum;
        Side? traverse(Node node) {
            Console.WriteLine(index);
            if (index >= node.WeightSum - node.Weight) {
                return null;
            }
            if (node.Left != null && index < node.Left.WeightSum) {
                return Side.LEFT;
            } else if (node.Left != null) {
                index -= node.Left.WeightSum;
            }
            if (node.Right != null) {
                return Side.RIGHT;
            }
            // TODO possible to reach this through rounding error, probably just return null here?
            throw new InvalidOperationException($"index {probability * this.Root.WeightSum} resulted in an illegal state");
        }
        return this.Traverse(traverse, false)!.Value;
    }

    public class Node(TValue value, float weight = 1) : Node<TValue, Node>(value) {
        public float Weight { get; } = weight;

        private float weightSum = weight;
        public float WeightSum {
            get => this.weightSum;
            private set {
                if (this.Parent != null) {
                    this.Parent.WeightSum += value - this.weightSum;
                }
                this.weightSum = value;
            }
        }

        public override void SetChild(Node? child, Side side) {
            var oldWeightSum = side switch {
                Side.LEFT => this.Left == null ? 0 : this.Left.WeightSum,
                Side.RIGHT => this.Right == null ? 0 : this.Right.WeightSum,
                Side.PARENT => 0,
                _ => 0
            };
            var increment = (child == null ? 0 : child.WeightSum) - oldWeightSum;
            base.SetChild(child, side);
            this.WeightSum += increment;
        }
    }
}
