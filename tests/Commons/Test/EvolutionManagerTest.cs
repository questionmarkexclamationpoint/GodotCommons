namespace QuestionMarkExclamationPoint.Commons.Test;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuestionMarkExclamationPoint.Commons.Extensions;
using QuestionMarkExclamationPoint.Commons.Genetic;

[TestClass]
public class EvolutionManagerTest {
    private readonly struct Polynomial(double a, double b, double c) {
        public double A { get; init; } = a;

        public double B { get; init; } = b;

        public double C { get; init; } = c;

        public readonly double Evaluate(double x) => (this.A * x * x) + (this.B * x) + this.C;
    }

    private static readonly Random RANDOM = new(Math.PI.GetHashCode());

    private static readonly Polynomial TARGET_POLYNOMIAL = new(Math.PI, -Math.E, Math.Sqrt(2));

    private static readonly List<double> TEST_VALUES = [
        -10, -5, -1, -0.1, 0, 0.1, 1, 5, 10
    ];

    private sealed class TestOrganism(double a, double b, double c) : IOrganism<TestOrganism> {
        public Polynomial Polynomial { get; private set; } = new(a, b, c);

        public double Fitness {
            get {
                var fitness = 0.0;
                foreach (var x in TEST_VALUES) {
                    var expected = TARGET_POLYNOMIAL.Evaluate(x);
                    var actual = this.Polynomial.Evaluate(x);
                    fitness -= Math.Abs(expected - actual);
                }
                return fitness;
            }
        }

        public TestOrganism Clone()
            => new(this.Polynomial.A, this.Polynomial.B, this.Polynomial.C);
        public void Mutate(double mutationChance = 0.01) {
            for (var i = 0; i < 3; i++) {
                if (RANDOM.NextDouble() < mutationChance) {
                    switch (i) {
                        case 0:
                            this.Polynomial = this.Polynomial with { A = this.Polynomial.A + RANDOM.NextDoubleInRange(-0.5, 0.5) };
                            break;
                        case 1:
                            this.Polynomial = this.Polynomial with { B = this.Polynomial.B + RANDOM.NextDoubleInRange(-0.5, 0.5) };
                            break;
                        case 2:
                            this.Polynomial = this.Polynomial with { C = this.Polynomial.C + RANDOM.NextDoubleInRange(-0.5, 0.5) };
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public TestOrganism Reproduce(TestOrganism other) {
            var a = RANDOM.NextDouble() < 0.5 ? this.Polynomial.A : other.Polynomial.A;
            var b = RANDOM.NextDouble() < 0.5 ? this.Polynomial.B : other.Polynomial.B;
            var c = RANDOM.NextDouble() < 0.5 ? this.Polynomial.C : other.Polynomial.C;
            return new(a, b, c);
        }
    }

    [TestMethod("Test evolution manager")]
    public void TestEvolutionManager() {
        var organisms = new List<TestOrganism>();
        for (var i = 0; i < 100; i++) {
            organisms.Add(new(RANDOM.NextDoubleInRange(-10, 10), RANDOM.NextDoubleInRange(-10, 10), RANDOM.NextDoubleInRange(-10, 10)));
        }
        for (var i = 0; i < 500; i++) {
            organisms = EvolutionManager.Evolve(organisms);
        }
        var bestOrganism = organisms.MaxBy(o => o.Fitness);
        Assert.IsTrue(bestOrganism.Fitness > -1);
    }
}
