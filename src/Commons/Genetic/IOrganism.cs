namespace QuestionMarkExclamationPoint.Commons.Genetic;

public interface IOrganism<TSelf> where TSelf : IOrganism<TSelf> {
    public double Fitness { get; }

    public TSelf Clone();

    public void Mutate(double mutationChance = 0.01);

    public TSelf Reproduce(TSelf other);
}
