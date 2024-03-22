using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
namespace TheoryOfAlgorithms

{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static int n = 0;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out n) || n <= 0)
            {
                MessageBox.Show("Enter a valid input");
            }
            else
            {

            }

        }

        public static string pathString = "";

        private static bool DFS(int[,] graph, int s, int t, int[] parent, int[,] residualGraph, out List<int> augmentingPath)
        {
            augmentingPath = new List<int>(); // Initialize empty list for the path

            bool[] visited = new bool[graph.GetLength(0)];
            Stack<int> stack = new Stack<int>();

            visited[s] = true;
            stack.Push(s);

            while (stack.Count > 0)
            {
                int u = stack.Pop();
                if (u == t)
                {
                    // Reconstruct augmenting path from parent array
                    int v = t;
                    while (v != s)
                    {
                        augmentingPath.Insert(0, v + 1); // Insert nodes in reverse order (source to sink)
                        v = parent[v];
                    }
                    augmentingPath.Insert(0, s + 1); // Add source node
                    return true;
                }

                for (int v = 0; v < graph.GetLength(0); v++)
                {
                    if (!visited[v] && residualGraph[u, v] > 0)
                    {
                        parent[v] = u;
                        visited[v] = true;
                        stack.Push(v);
                    }
                }
            }

            return visited[t];
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Мяу мяу мяу!");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();



            int[,] graph = new int[,] { {0, 0, 0, 3*n, 0, 0,4*n,0,0,0,0},
                                     {0, 0, n, 0, 0, 0,0,0,n,0,2*n},
                                     {0, 0, 0, 0, 0, n,0,0,2*n,0,0},
                                     {3*n, 0, 0, 0, 0, 0,0,n,3*n,0,0},
                                     {0, 0,2*n, 0, 0, 0,0,0,0,4*n,0},
                                     {0, 0, n, 0, 0, 0,0,0,0,2*n,0},
                                     {4*n, 0, 0, 0, 0, 0,0,0,0,0,0},
                                     {0, 0, 0, 0, 4*n, 0,0,0,0,0,0},
                                     {0, n, 0, 3*n, 3*n, 0,0,0,0,0,0},
                                     {0, 0, 0, 0, 4*n, 2*n,0,0,0,0,0},
                                     {0, 2*n, 0, 0, 0, 2*n,0,0,0,0,0}
        };

            int source = 6; // Starting vertex
            int sink = 10;   // Ending vertex
            int maxFlow = 0;
            List<int> augmentingPath;
            pathString = "";

            int u, v;

            int[,] residualGraph = (int[,])graph.Clone(); // Create a copy of the graph

            int[] parent = new int[graph.GetLength(0)];

            while (DFS(graph, source, sink, parent, residualGraph, out augmentingPath))
            {
                int pathFlow = int.MaxValue;
                pathString = "Augmenting Path: " + string.Join(" -> ", augmentingPath);

                for (v = sink; v != source; v = parent[v])
                {
                    u = parent[v];
                    pathFlow = Math.Min(pathFlow, residualGraph[u, v]);
                }

                for (v = sink; v != source; v = parent[v])
                {
                    u = parent[v];
                    residualGraph[u, v] -= pathFlow;
                    residualGraph[v, u] += pathFlow;
                }

                maxFlow += pathFlow;
            }

            stopwatch.Stop();
            string resultText = "Час виконання алгоритму: " + stopwatch.Elapsed + " с\n" + "Максимальна кількість машин: " + maxFlow;
            if (textBox1.Text == "")
            {
                MessageBox.Show("Enter flow");
            }
            else
            {
                MessageBox.Show("The result:\n " + resultText + "\n" + pathString);
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Мяу мяу мяу!");
        }



        private void EdmonsKarp_Click(object sender, EventArgs e)
        {

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int[,] graph = new int[,] { {0, 0, 0, 3*n, 0, 0,4*n,0,0,0,0},
                                     {0, 0, n, 0, 0, 0,0,0,n,0,2*n},
                                     {0, 0, 0, 0, 0, n,0,0,2*n,0,0},
                                     {3*n, 0, 0, 0, 0, 0,0,n,3*n,0,0},
                                     {0, 0,2*n, 0, 0, 0,0,0,0,4*n,0},
                                     {0, 0, n, 0, 0, 0,0,0,0,2*n,0},
                                     {4*n, 0, 0, 0, 0, 0,0,0,0,0,0},
                                     {0, 0, 0, 0, 4*n, 0,0,0,0,0,0},
                                     {0, n, 0, 3*n, 3*n, 0,0,0,0,0,0},
                                     {0, 0, 0, 0, 4*n, 2*n,0,0,0,0,0},
                                     {0, 2*n, 0, 0, 0, 2*n,0,0,0,0,0}
                          };

            int source = 6; // Starting vertex
            int sink = 10;  // Ending vertex
            int maxFlow = 0;


            int[,] residualGraph = (int[,])graph.Clone(); // Create a copy of the graph

            while (BFS(residualGraph, source, sink, out int[] parent))
            {
                int pathFlow = FindBottleNeck(residualGraph, parent, source, sink);

                UpdateResidualCapacity(residualGraph, parent, pathFlow, source, sink);

                maxFlow += pathFlow;
            }

            stopwatch.Stop();
            string resultText = "Час виконання алгоритму: " + stopwatch.Elapsed + " с\n" + "Максимальна кількість машин: " + maxFlow;
            if (textBox1.Text == "")
            {
                MessageBox.Show("Enter flow");
            }
            else
            {
                MessageBox.Show("The result:\n " + resultText + "\n" + pathString);
            }
        }

        private bool BFS(int[,] graph, int s, int t, out int[] parent)
        {
            int n = graph.GetLength(0);
            bool[] visited = new bool[n];
            parent = new int[n];
            Queue<int> queue = new Queue<int>();

            visited[s] = true;
            queue.Enqueue(s);

            while (queue.Count > 0)
            {
                int u = queue.Dequeue();

                for (int v = 0; v < n; v++)
                {
                    if (!visited[v] && graph[u, v] > 0)
                    {
                        parent[v] = u;
                        visited[v] = true;
                        queue.Enqueue(v);
                    }
                }
            }
            return visited[t];
        }

        private int FindBottleNeck(int[,] graph, int[] parent, int source, int sink)
        {
            int pathFlow = int.MaxValue;
            int v = sink;

            while (v != source)
            {
                int u = parent[v];
                pathFlow = Math.Min(pathFlow, graph[u, v]);
                v = u;
            }

            return pathFlow;
        }

        private void UpdateResidualCapacity(int[,] graph, int[] parent, int flow, int source, int sink)
        {
            int v = sink;

            while (v != source)
            {
                int u = parent[v];
                graph[u, v] -= flow;
                graph[v, u] += flow;
                v = u;
            }
        }


        // laba 3

        public static int sccCount = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Random random = new Random();
            // Create a graph given in the above diagram
            int n = 30;
            Graph graph = new Graph(random.Next(n));
            graph.GenerateRandomEdges(60);


            //graph.addEdge(1, 7);
            //graph.addEdge(1, 4);
            //graph.addEdge(2, 3);
            //graph.addEdge(2, 9);
            //graph.addEdge(2, 11);
            //graph.addEdge(3, 6);
            //graph.addEdge(3, 9);
            //graph.addEdge(4, 1);
            //graph.addEdge(4, 9);
            //graph.addEdge(4, 8);
            //graph.addEdge(5, 3);
            //graph.addEdge(5, 8);
            //graph.addEdge(5, 10);
            //graph.addEdge(6, 3);
            //graph.addEdge(6, 10);
            //graph.addEdge(7, 1);
            //graph.addEdge(8, 5);
            //graph.addEdge(10, 5);
            //graph.addEdge(9, 2);
            //graph.addEdge(9, 5);
            //graph.addEdge(9, 4);
            //graph.addEdge(10, 6);
            //graph.addEdge(11, 6);
            //graph.addEdge(11, 2);

            pathString = "";

            graph.PrintSCCs(pathString);
            stopwatch.Stop();
            MessageBox.Show("Сильно зв'язані компоненти:" + "\n" + pathString + "Час виконання алгоритму: " + stopwatch.Elapsed + " mс\n" + "Їх кількість: " + --sccCount);
        }

        public class Graph
        {
            public int V; // No. of vertices
            private List<int>[] adj; // An array of adjacency lists

            public Graph(int V)
            {
                this.V = V;
                adj = new List<int>[V];
                for (int i = 0; i < V; ++i)
                {
                    adj[i] = new List<int>();
                }
            }

            public void addEdge(int v, int w)
            {
                adj[v].Add(w);
            }

            public void fillOrder(int v, bool[] visited, Stack<int> stack)
            {
                visited[v] = true;

                // Iterate through all adjacent vertices of v
                foreach (int neighbor in adj[v])
                {
                    if (!visited[neighbor])
                    {
                        fillOrder(neighbor, visited, stack);
                    }
                }

                // After exploring all adjacent vertices, push v to stack
                stack.Push(v);
            }

            public void GenerateRandomEdges(int numEdges)
            {
                Random random = new Random();
                int vertices = adj.Length - 1;

                for (int i = 0; i < numEdges; ++i)
                {
                    int source = random.Next(1, vertices + 1);
                    int destination = random.Next(1, vertices + 1);
                    addEdge(source, destination);
                }
            }

            private void DFSUtil(int v, bool[] visited, Stack<int> stack)
            {

                visited[v] = true;
                if (v == 0) { } else { pathString += v + " "; }

                // Recur for all the vertices adjacent to this vertex
                foreach (int i in adj[v])
                {
                    if (!visited[i])
                    {
                        DFSUtil(i, visited, stack);
                    }
                }

            }

            private Graph GetTranspose()
            {
                Graph g = new Graph(V);
                for (int v = 0; v < V; v++)
                {
                    // Add edges to transposed graph
                    foreach (int i in adj[v])
                    {
                        g.adj[i].Add(v);
                    }
                }

                return g;
            }

            public void PrintSCCs(string finalText)
            {
                Stack<int> stack = new Stack<int>();

                // Mark all the vertices as not visited (for first DFS)
                bool[] visited = new bool[V];
                for (int i = 0; i < V; i++)
                {
                    visited[i] = false;
                }

                // Fill vertices in stack according to their finishing times
                for (int i = 0; i < V; i++)
                {
                    if (!visited[i])
                    {
                        fillOrder(i, visited, stack);
                    }
                }

                // Create a reversed graph
                Graph gr = GetTranspose();

                // Mark all the vertices as not visited (for second DFS)
                for (int i = 0; i < V; i++)
                {
                    visited[i] = false;
                }

                // Now iterate through the stack in reverse and call DFS
                // for each vertex in the stack
                while (stack.Count > 0)
                {
                    int v = stack.Pop();

                    // Check if this vertex is not visited yet (in second DFS)
                    if (!visited[v])
                    {
                        gr.DFSUtil(v, visited, null); // Don't print here in SCC
                        pathString += "\n";
                        sccCount += 1;
                    }
                }
            }


        }

       

        private void button4_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            sccCount = 0;
            pathFinding graph = new pathFinding();
            int V = 200;
            List<List<int>> edges = graph.GenerateRandomEdges(50, 500);
        //    List<List<int>> edges = new List<List<int>>
        //{
        //    new List<int> { 1, 7 }, new List<int> { 1, 4 }, new List<int> { 2, 3 },
        //    new List<int> { 2, 9 }, new List<int> { 2, 11 }, new List<int> { 3, 6 },
        //    new List<int> { 3, 9 }, /*new List<int> { 4, 1 },*/ new List<int> { 4, 9 },
        //    new List<int> { 4, 8 }, new List<int> { 5, 3 }, new List<int> { 5, 8 },
        //    new List<int> { 5, 10 }, new List<int> { 6, 3 }, new List<int> { 6, 10 },
        //    new List<int> { 7, 1 }, new List<int> { 8, 5 }, new List<int> { 10, 5 },
        //    new List<int> { 9, 2 }, new List<int> { 9, 5 }, new List<int> { 9, 4 },
        //    new List<int> { 10, 6 }, new List<int> { 11, 6 }, new List<int> { 11, 2 },

            //};
            pathString = "";
            List<List<int>> ans = graph.FindSCC(V, edges);
            foreach (var x in ans)
            {
                foreach (var y in x)
                {
                    pathString += y + " ";
                }
                pathString += "\n";
            }

            stopwatch.Stop();
            MessageBox.Show("Сильно зв'язані компоненти:" + "\n" + pathString + "Час виконання алгоритму: " + stopwatch.Elapsed + " mс\n" + "Їх кількість: " + ans.Count);

        }

        class pathFinding
        {
            // dfs Function to reach destination
            public bool Dfs(int curr, int des, List<List<int>> adj, List<int> visited)
            {
                // If curr node is the destination, return true
                if (curr == des)
                {
                    return true;
                }
                visited[curr] = 1;
                foreach (var x in adj[curr])
                {
                    if (visited[x] == 0)
                    {
                        if (Dfs(x, des, adj, visited))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }


            public List<List<int>> GenerateRandomEdges(int vertices, int numEdges)
            {
                Random random = new Random();
                List<List<int>> edges = new List<List<int>>();
                HashSet<string> edgeSet = new HashSet<string>();

                // Ensure that each edge is unique
                while (edges.Count < numEdges)
                {
                    int from = random.Next(1, vertices + 1);
                    int to = random.Next(1, vertices + 1);

                    // Ensure that an edge doesn't connect a vertex to itself
                    if (from != to)
                    {
                        string edge = from + "-" + to;
                        if (!edgeSet.Contains(edge))
                        {
                            edgeSet.Add(edge);
                            edges.Add(new List<int> { from, to });
                        }
                    }
                }

                return edges;
            }

            // To tell whether there is a path from source to destination
            public bool IsPath(int src, int des, List<List<int>> adj)
            {
                var visited = new List<int>(adj.Count + 1);
                for (int i = 0; i < adj.Count + 1; i++)
                {
                    visited.Add(0);
                }
                return Dfs(src, des, adj, visited);
            }

            // Function to return all the strongly connected components of a graph
            public List<List<int>> FindSCC(int n, List<List<int>> edges)
            {
                // Stores all the strongly connected components
                var answer = new List<List<int>>();

                // Stores whether a vertex is a part of any Strongly Connected Component
                var isScc = new List<int>(n + 1);
                for (int i = 0; i < n + 1; i++)
                {
                    isScc.Add(0);
                }

                var adj = new List<List<int>>(n + 1);
                for (int i = 0; i < n + 1; i++)
                {
                    adj.Add(new List<int>());
                }

                for (int i = 0; i < edges.Count; i++)
                {
                    adj[edges[i][0]].Add(edges[i][1]);
                }

                // Traversing all the vertices
                for (int i = 1; i <= n; i++)
                {
                    if (isScc[i] == 0)
                    {
                        // If a vertex i is not a part of any SCC
                        // insert it into a new SCC list and check
                        // for other vertices whether they can be
                        // the part of this list.
                        var scc = new List<int>();
                        scc.Add(i);

                        for (int j = i + 1; j <= n; j++)
                        {
                            // If there is a path from vertex i to
                            // vertex j and vice versa, put vertex j
                            // into the current SCC list.
                            if (isScc[j] == 0 && IsPath(i, j, adj) && IsPath(j, i, adj))
                            {
                                isScc[j] = 1;
                                scc.Add(j);
                            }
                        }

                        // Insert the SCC containing vertex i into
                        // the final list.
                        answer.Add(scc);
                    }
                }
                return answer;
            }


        }


    }
}

