// NOT DONE - WORK IN PROGRESS
using System.Collections.Generic;

namespace DSA.Structures
{
    internal abstract class GraphTemplate<T>
    {
        protected int numOfVertices;
        public int NumOfVertices
        {
            get
            {
                return numOfVertices;
            }
        }

        public abstract void AddEdge(int Vertex1, int Vertex2, double Weight, bool IsDirected = false);
        public abstract List<int> GetAdjacentVertices(int Vertex_Index);
        public abstract double GetEdgeWeight(T Vertex1, T Vertex2);
        public abstract void AddVertex(T Data);
    }
    internal class MGraph<T> : GraphTemplate<T>
    {
        /// <summary>
        /// The adjacency matrix for the graph instance
        /// </summary>
        private Matrix AdjMatrix;
        private Dictionary<int, T> VertexData;
        public MGraph(int NoOfVertices)
        {
            AdjMatrix = new Matrix(NoOfVertices, NoOfVertices);
            VertexData = new Dictionary<int, T>();
            numOfVertices = NoOfVertices;
        }
        public override void AddEdge(int V1_index, int V2_index, double Weight, bool IsDirected = false)
        {
            if (V1_index >= AdjMatrix.Cols || V2_index >= AdjMatrix.Rows)
            {
                throw new System.Exception("Error: tried to add edge between a non-existing vertex pair");
            }
            AdjMatrix[V1_index, V2_index] = Weight; //setting v1 to v2 weight
            if (!IsDirected)
            {
                AdjMatrix[V2_index, V1_index] = Weight; //setting v2 to v1 weight if nodes are not directed
            }
        }
        public override List<int> GetAdjacentVertices(int Vertex_Index)
        {
            List<int> Vertices = new List<int>(); //list of vertices connected
            for (int i = 0; i < numOfVertices; i++)
            {
                if (AdjMatrix[Vertex_Index, i] > 0) //checking if there is an associated weight
                {
                    Vertices.Add(i);
                }
            }
            return Vertices;
        }
        public override void AddVertex(T Data)
        {
            
            throw new System.NotImplementedException();
        }
        public override double GetEdgeWeight(T Vertex1, T Vertex2)
        {
            throw new System.NotImplementedException();
        }
    }
}