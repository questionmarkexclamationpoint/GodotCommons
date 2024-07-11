namespace Commons;

using System;

public class WeightTree<TValue>
        : BinarySearch.Tree<TValue, WeightTree<TValue>.Node>
        where TValue : IComparable<TValue> {

    public override Node Create(TValue item) => new(item);
    public TValue? Sample(float probability) => this.Root == null ? default : this.Root.Sample(probability);

    public class Node(TValue value, float weight = 1) : BinarySearch.Node<TValue, Node>(value) {

        public float Weight { get; } = weight;

        protected float WeightSum { get; private set; } = weight;

        public override void SetChild(Node? child, BinarySearch.Side side) {
            var oldProbability = side switch {
                BinarySearch.Side.LEFT => this.Left == null ? 0 : this.Left.WeightSum,
                BinarySearch.Side.RIGHT => this.Right == null ? 0 : this.Right.WeightSum,
                BinarySearch.Side.PARENT => 0,
                _ => 0
            };
            var probabilityIncrement = (child == null ? 0 : child.WeightSum) - oldProbability;
            base.SetChild(child, side);
            this.WeightSum += probabilityIncrement;
        }

        public TValue Sample(float probability) {
            probability = Math.Clamp(probability, 0, 1) * this.WeightSum;
            return this.SampleInternal(probability);
        }

        private TValue SampleInternal(float index) {
            if (index >= this.WeightSum - this.Weight) {
                return this.Value;
            }
            index -= this.Weight;
            if (this.Left != null && index <= this.Left.WeightSum) {
                return this.Left.SampleInternal(index);
            } else if (this.Left != null) {
                index -= this.Left.WeightSum;
            }
            if (this.Right != null) {
                return this.Right.SampleInternal(index);
            }
            throw new InvalidOperationException($"index {index} resulted in an illegal state");
        }
    }
}
