namespace QuestionMarkExclamationPoint.Commons.Genetic;

using System.Collections.Generic;
using System.Linq;

// TODO convert this into a builder pattern
public static class EvolutionManager {
    public static List<TOrganism> Evolve<TOrganism>(IEnumerable<TOrganism> organisms, double mutationRate = 0.01) where TOrganism : IOrganism<TOrganism> {
        var parents = organisms.OrderByDescending(o => o.Fitness).Take(organisms.Count() / 2).ToList();
        var children = new List<TOrganism>();
        for (var i = 0; i < organisms.Count(); i++) {
            var parent1 = parents[i % parents.Count];
            var parent2 = parents[(i + 1) % parents.Count];
            var child = parent1.Reproduce(parent2);
            child.Mutate(mutationRate);
            children.Add(child);
        }
        return children;
    }
}
