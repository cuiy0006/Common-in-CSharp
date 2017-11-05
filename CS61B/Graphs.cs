using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS61B
{
    class Edge
    {
        private string _FromName;

        public string FromName
        {
            get { return _FromName; }
        }

        private string _ToName;

        public string ToName
        {
            get { return _ToName; }
        }
        private int _Weight;

        public int Weight
        {
            get { return _Weight; }
        }

        public Edge(string from, string to, int weight)
        {
            _FromName = from;
            _ToName = to;
            _Weight = weight;
        }
    }

    class Vertex
    {
        private string _Name;

        public string Name
        {
            get { return _Name; }
        }

        private Vertex _Parent;

        public Vertex Parent
        {
            get { return _Parent; }
        }

        private int _Depth = 0;

        public int Depth
        {
            get { return _Depth; }
        }

        private bool _Visited = false;

        public bool Visited
        {
            get { return _Visited; }
            set { _Visited = value; }
        }

        private List<Edge> _AdjacentEdges;

        public List<Edge> AdjacentEdges
        {
            get { return _AdjacentEdges; }
        }

        public Vertex(string name, List<Edge> lst)
        {
            _AdjacentEdges = lst;
            _Name = name;
        }

        public void initialize()
        {
            _Visited = false;
            _Depth = 0;
        }

        public void Visit(Vertex origin)
        {
            this._Parent = origin;
            if (origin == null)
                this._Depth = 0;
            else
                this._Depth = origin.Depth + 1;
            string parentName;
            if (this._Parent == null)
                parentName = "NULL";
            else
                parentName = this._Parent.Name;
            Console.WriteLine(this.Name + ", At Level: " + this.Depth + ", Its Parent: " + parentName);
        }

        public void Visit()
        {
            Console.WriteLine(this.Name);
        }
    }

    public interface myGraph
    {
        void AddEdge(string from, string to, int weigth);
        void DFS(string VertexName);
        void BFS(string VertexName);
        Graph GetMinWeight(); //Only for undirected graph, looking for minimun spanning tree
    }

    public class Graph : myGraph
    {
        private Dictionary<string, Vertex> _VertexDic;

        public int Size
        {
            get { return _VertexDic.Count(); }
        }

        public Graph()
        {
            _VertexDic = new Dictionary<string, Vertex>();
        }

        public void AddEdge(string from, string to, int weight)
        {
            if (_VertexDic.ContainsKey(from))
                _VertexDic[from].AdjacentEdges.Add(new Edge(from, to, weight));
            else
            {
                List<Edge> lst = new List<Edge>();
                lst.Add(new Edge(from, to, weight));
                Vertex newvertex = new Vertex(from, lst);
                _VertexDic.Add(from, newvertex);
            }

            if (!_VertexDic.ContainsKey(to))
            {
                Vertex newvertex = new Vertex(to, new List<Edge>());
                _VertexDic.Add(to, newvertex);
            }
        }        

        public void DFS(string VertexName)
        {
            foreach (var element in _VertexDic)
            {
                element.Value.initialize();
            }
            int time = 0;
            DFSLoop(_VertexDic[VertexName], null, ref time);
        }

        private void DFSLoop(Vertex vertex, Vertex parent, ref int time)
        {
            time++;
            Console.WriteLine(vertex.Name + "'s d Value : " + time);
            vertex.Visit(parent);
            vertex.Visited = true;

            for (int i = 0; i < vertex.AdjacentEdges.Count(); i++)
            {
                string neighbourName = vertex.AdjacentEdges[i].ToName;
                if (!_VertexDic[neighbourName].Visited)
                    DFSLoop(_VertexDic[neighbourName], vertex, ref time);
            }
            time++;
        }

        public void BFS(string VertexName)
        {
            foreach (var element in _VertexDic)
            {
                element.Value.initialize();
            }

            Vertex vertex = _VertexDic[VertexName];
            vertex.Visit(null);
            vertex.Visited = true;

            Queue<Vertex> queue = new Queue<Vertex>();
            queue.Enqueue(vertex);

            while (queue.Count() != 0)
            {
                Vertex tempVertex = queue.Dequeue();
                for (int i = 0; i < tempVertex.AdjacentEdges.Count(); i++)
                {
                    string neighbourName = tempVertex.AdjacentEdges[i].ToName;
                    if (!_VertexDic[neighbourName].Visited)
                    {
                        queue.Enqueue(_VertexDic[neighbourName]);
                        _VertexDic[neighbourName].Visit(tempVertex);
                        _VertexDic[neighbourName].Visited = true;
                    }
                }
            }
        }

        public Graph GetMinWeight() //Only for undirected graph, looking for minimun spanning tree
        {
            Graph newGraph = this.ShallowCopy();
            List<Edge> allEdges = new List<Edge>();
            Dictionary<string, TreeNode> dic = new Dictionary<string, TreeNode>();
            foreach (var element in this._VertexDic)
            {
                dic.Add(element.Key, new TreeNode(element.Key, null));
                foreach (var edge in element.Value.AdjacentEdges)
                {
                    allEdges.Add(edge);
                }
            }

            allEdges = allEdges.OrderBy(x => x.Weight).ToList();
            foreach (var edge in allEdges)
            {
                if(!IsConnected(edge.FromName, edge.ToName, dic))
                {
                    newGraph._VertexDic[edge.FromName].AdjacentEdges.Add(new Edge(edge.FromName, edge.ToName, edge.Weight));
                    newGraph._VertexDic[edge.ToName].AdjacentEdges.Add(new Edge(edge.ToName, edge.FromName, edge.Weight));
                }
            }

            return newGraph;
        }

        private bool IsConnected(string One, string theOther, Dictionary<string, TreeNode> dic) //Quick-Union
        {
            TreeNode node1 = dic[One];
            TreeNode node2 = dic[theOther];
            while (node1.Parent != null)
            {
                node1 = node1.Parent;
            }
            while (node2.Parent != null)
            {
                node2 = node2.Parent;
            }
            if (node1.Name == node2.Name)
                return true;
            else
            {
                node1.Parent = node2;
                return false;
            }
        }

        private class TreeNode
        {
            public string Name;
            public TreeNode Parent;
            public TreeNode(string name, TreeNode node)
            {
                Name = name;
                Parent = node;
            }
        }

        private Graph ShallowCopy()
        {
            Graph newGraph = new Graph();
            foreach (var element in this._VertexDic)
            {
                Vertex newVertex = new Vertex(element.Key, new List<Edge>());
                newGraph._VertexDic.Add(newVertex.Name, newVertex);
            }
            return newGraph;
        }
    }

    
}
