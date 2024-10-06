namespace QuestionMarkExclamationPoint.Commons.Genetic;

public interface IOrganism<TSelf> where TSelf : IOrganism<TSelf> {
    double Fitness { get; }

    TSelf Clone();

    void Mutate(double mutationChance = 0.01);

    TSelf Reproduce(TSelf other);
}
