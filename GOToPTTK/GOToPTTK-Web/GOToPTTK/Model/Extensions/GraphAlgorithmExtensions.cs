using QuickGraph;
using QuickGraph.Algorithms.Observers;
using QuickGraph.Algorithms.ShortestPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GOToPTTK.Model.Extensions
{
    public static class GraphAlgorithmExtensions
    {
        private class AStarWrapper<TVertex, TEdge>
          where TEdge : IEdge<TVertex>
        {
            private readonly TVertex target;
            private readonly AStarShortestPathAlgorithm<TVertex, TEdge> innerAlgorithm;
            public VertexPredecessorRecorderObserver<TVertex, TEdge> Observer { get; set; }

            public AStarWrapper(AStarShortestPathAlgorithm<TVertex, TEdge> innerAlgo, TVertex root, TVertex target)
            {
                innerAlgorithm = innerAlgo;
                this.innerAlgorithm.SetRootVertex(root);
                this.target = target;
                this.innerAlgorithm.FinishVertex += new VertexAction<TVertex>(innerAlgorithm_FinishVertex);
            }
            void innerAlgorithm_FinishVertex(TVertex vertex)
            {
                if (object.Equals(vertex, target))
                {
                    this.innerAlgorithm.Abort();
                }
            }
            public bool Compute(out IEnumerable<TEdge> path)
            {
                this.Observer = new VertexPredecessorRecorderObserver<TVertex, TEdge>();
                this.Observer.Attach(this.innerAlgorithm);
                this.innerAlgorithm.Compute();
                double distance = innerAlgorithm.Distances[target];
                return this.Observer.TryGetPath(target, out path);
            }
        }

        public static bool ComputeDistanceBetween<TVertex, TEdge>(this AStarShortestPathAlgorithm<TVertex, TEdge> algo, TVertex start, TVertex end, out IEnumerable<TEdge> path)
            where TEdge : IEdge<TVertex>
        {
            var wrap = new AStarWrapper<TVertex, TEdge>(algo, start, end);
            return wrap.Compute(out path);
        }
    }
}
