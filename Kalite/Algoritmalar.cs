using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalite
{
    public class Flody
    {
        public void Main()
        {
            double[][] proximityMatrix = PrepareFirstState();
            Solve(ref proximityMatrix);
            Dump(proximityMatrix);
        }

        public void Solve(ref double[][] matrix)
        {
            int size = matrix.Count();

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    for (int k = 0; k < size; k++)
                    {
                        matrix[j][k] = Math.Min(matrix[j][k], matrix[j][i] + matrix[i][k]);
                    }
                }
            }
        }

        public double[][] PrepareFirstState()
        {
            double[][] matrix = new double[6][]{
                new double[6],
                new double[6],
                new double[6],
                new double[6],
                new double[6],
                new double[6]
            };

            matrix[0][0] = 0;
            matrix[0][1] = 5;
            matrix[0][2] = double.PositiveInfinity;
            matrix[0][3] = double.PositiveInfinity;
            matrix[0][4] = 16;
            matrix[0][5] = 8;

            matrix[1][0] = 5;
            matrix[1][1] = 0;
            matrix[1][2] = 1;
            matrix[1][3] = double.PositiveInfinity;
            matrix[1][4] = double.PositiveInfinity;
            matrix[1][5] = 2;

            matrix[2][0] = double.PositiveInfinity;
            matrix[2][1] = 1;
            matrix[2][2] = 0;
            matrix[2][3] = 1;
            matrix[2][4] = double.PositiveInfinity;
            matrix[2][5] = 6;

            matrix[3][0] = double.PositiveInfinity;
            matrix[3][1] = double.PositiveInfinity;
            matrix[3][2] = 1;
            matrix[3][3] = 0;
            matrix[3][4] = 4;
            matrix[3][5] = 5;

            matrix[4][0] = 16;
            matrix[4][1] = double.PositiveInfinity;
            matrix[4][2] = double.PositiveInfinity;
            matrix[4][3] = 4;
            matrix[4][4] = 0;
            matrix[4][5] = 4;

            matrix[5][0] = 8;
            matrix[5][1] = 2;
            matrix[5][2] = 6;
            matrix[5][3] = 5;
            matrix[5][4] = 4;
            matrix[5][5] = 0;

            return matrix;
        }

        public void Dump(double[][] matrix)
        {
            int size = matrix.Count();

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write("{0}\t", matrix[i][j]);
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }

    public class BellmanFord
    {
        public class Path
        {
            public string From;
            public string To;
            public int Cost;

            public Path(string from, string to, int cost)
            {
                From = from;
                To = to;
                Cost = cost;
            }
        }

        public string[] vertices = { "S", "A", "B", "C", "D", "E" };

        public List<Path> graph = new List<Path>()
        {
            new Path("S", "A", 1),
            new Path("S", "E", 2),
            new Path("A", "C", 3),
            new Path("B", "A", 2),
            new Path("C", "B", 1),
            new Path("D", "C", 3),
            new Path("D", "A", 1),
            new Path("E", "D", 2)
        };

        public Dictionary<string, int> memo = new Dictionary<string, int>()
        {
            { "S", 0 },
            { "A", int.MaxValue },
            { "B", int.MaxValue },
            { "C", int.MaxValue },
            { "D", int.MaxValue },
            { "E", int.MaxValue }
        };

        public bool Iterate()
        {
            bool doItAgain = false;

            foreach (var fromVertex in vertices)
            {
                Path[] edges = graph.Where(x => x.From == fromVertex).ToArray();
                foreach (var edge in edges)
                {
                    int potentialCost = memo[edge.From] == int.MaxValue ? int.MaxValue : memo[edge.From] + edge.Cost;
                    if (potentialCost < memo[edge.To])
                    {
                        memo[edge.To] = potentialCost;
                        doItAgain = true;
                    }
                }
            }
            return doItAgain;
        }

        public void Main()
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                if (!Iterate())
                    break;
            }

            foreach (var keyValue in memo)
            {
                Console.WriteLine($"{keyValue.Key}: {keyValue.Value}");
            }
        }
    }
}

public class Dijkstra
{
    public class Graph
    {
        Dictionary<char, Dictionary<char, int>> vertices = new Dictionary<char, Dictionary<char, int>>();
        public void add_vertex(char name, Dictionary<char, int> edges)
        {
            vertices[name] = edges;
        }
        public List<char> shortest_path(char start, char finish)
        {
            var previous = new Dictionary<char, char>();
            var distances = new Dictionary<char, int>();
            var nodes = new List<char>();
            List<char> path = null;
            foreach (var vertex in vertices)
            {
                if (vertex.Key == start)
                {
                    distances[vertex.Key] = 0;
                }
                else
                {
                    distances[vertex.Key] = int.MaxValue;
                }
                nodes.Add(vertex.Key);
            }
            while (nodes.Count != 0)
            {
                nodes.Sort((x, y) => distances[x] - distances[y]);
                var smallest = nodes[0];
                nodes.Remove(smallest);
                if (smallest == finish)
                {
                    path = new List<char>();
                    while (previous.ContainsKey(smallest))
                    {
                        path.Add(smallest);
                        smallest = previous[smallest];
                    }
                    break;
                }
                if (distances[smallest] == int.MaxValue)
                {
                    break;
                }
                foreach (var neighbor in vertices[smallest])
                {
                    var alt = distances[smallest] + neighbor.Value;
                    if (alt < distances[neighbor.Key])
                    {
                        distances[neighbor.Key] = alt;
                        previous[neighbor.Key] = smallest;
                    }
                }
            }
            return path;
        }
    }

    public void Main()
    {
        Graph g = new Graph();
        g.add_vertex('A', new Dictionary<char, int>() { { 'B', 7 }, { 'C', 8 } });
        g.add_vertex('B', new Dictionary<char, int>() { { 'A', 7 }, { 'F', 2 } });
        g.add_vertex('C', new Dictionary<char, int>() { { 'A', 8 }, { 'F', 6 }, { 'G', 4 } });
        g.add_vertex('D', new Dictionary<char, int>() { { 'F', 8 } });
        g.add_vertex('E', new Dictionary<char, int>() { { 'H', 1 } });
        g.add_vertex('F', new Dictionary<char, int>() { { 'B', 2 }, { 'C', 6 }, { 'D', 8 }, { 'G', 9 }, { 'H', 3 } });
        g.add_vertex('G', new Dictionary<char, int>() { { 'C', 4 }, { 'F', 9 } });
        g.add_vertex('H', new Dictionary<char, int>() { { 'E', 1 }, { 'F', 3 } });
        g.shortest_path('A', 'H').ForEach(x => Console.WriteLine(x));

    }
}




class FloydWarshallAlgo
{

    public const int cst = 9999;

    private static void Print(int[,] distance, int verticesCount)
    {
        Console.WriteLine("Shortest distances between every pair of vertices:");

        for (int i = 0; i < verticesCount; ++i)
        {
            for (int j = 0; j < verticesCount; ++j)
            {
                if (distance[i, j] == cst)
                    Console.Write("cst".PadLeft(7));
                else
                    Console.Write(distance[i, j].ToString().PadLeft(7));
            }

            Console.WriteLine();
        }
    }

    public static void FloydWarshall(int[,] graph, int verticesCount)
    {
        int[,] distance = new int[verticesCount, verticesCount];

        for (int i = 0; i < verticesCount; ++i)
            for (int j = 0; j < verticesCount; ++j)
                distance[i, j] = graph[i, j];

        for (int k = 0; k < verticesCount; ++k)
        {
            for (int i = 0; i < verticesCount; ++i)
            {
                for (int j = 0; j < verticesCount; ++j)
                {
                    if (distance[i, k] + distance[k, j] < distance[i, j])
                        distance[i, j] = distance[i, k] + distance[k, j];
                }
            }
        }

        Print(distance, verticesCount);
    }
    public void Main()
    {
        int[,] graph = {
                         { 0,   6,  cst, 11 },
                         { cst, 0,   4, cst },
                         { cst, cst, 0,   2 },
                         { cst, cst, cst, 0 }
                           };

        FloydWarshall(graph, 4);
    }
}


class BFS
{
    public class Employee
    {
        public Employee(string name)
        {
            this.name = name;
        }

        public string name { get; set; }
        public List<Employee> Employees
        {
            get
            {
                return EmployeesList;
            }
        }

        public void isEmployeeOf(Employee p)
        {
            EmployeesList.Add(p);
        }

        List<Employee> EmployeesList = new List<Employee>();

        public override string ToString()
        {
            return name;
        }
    }

    public class BreadthFirstAlgorithm
    {
        public Employee BuildEmployeeGraph()
        {
            Employee Eva = new Employee("Eva");
            Employee Sophia = new Employee("Sophia");
            Employee Brian = new Employee("Brian");
            Eva.isEmployeeOf(Sophia);
            Eva.isEmployeeOf(Brian);

            Employee Lisa = new Employee("Lisa");
            Employee Tina = new Employee("Tina");
            Employee John = new Employee("John");
            Employee Mike = new Employee("Mike");
            Sophia.isEmployeeOf(Lisa);
            Sophia.isEmployeeOf(John);
            Brian.isEmployeeOf(Tina);
            Brian.isEmployeeOf(Mike);

            return Eva;
        }

        public Employee Search(Employee root, string nameToSearchFor)
        {
            Queue<Employee> Q = new Queue<Employee>();
            HashSet<Employee> S = new HashSet<Employee>();
            Q.Enqueue(root);
            S.Add(root);

            while (Q.Count > 0)
            {
                Employee e = Q.Dequeue();
                if (e.name == nameToSearchFor)
                    return e;
                foreach (Employee friend in e.Employees)
                {
                    if (!S.Contains(friend))
                    {
                        Q.Enqueue(friend);
                        S.Add(friend);
                    }
                }
            }
            return null;
        }

        public void Traverse(Employee root)
        {
            Queue<Employee> traverseOrder = new Queue<Employee>();

            Queue<Employee> Q = new Queue<Employee>();
            HashSet<Employee> S = new HashSet<Employee>();
            Q.Enqueue(root);
            S.Add(root);

            while (Q.Count > 0)
            {
                Employee e = Q.Dequeue();
                traverseOrder.Enqueue(e);

                foreach (Employee emp in e.Employees)
                {
                    if (!S.Contains(emp))
                    {
                        Q.Enqueue(emp);
                        S.Add(emp);
                    }
                }
            }

            while (traverseOrder.Count > 0)
            {
                Employee e = traverseOrder.Dequeue();
                Console.WriteLine(e);
            }
        }
    }

    public void Main()
    {
        BreadthFirstAlgorithm b = new BreadthFirstAlgorithm();
        Employee root = b.BuildEmployeeGraph();
        Console.WriteLine("Traverse Graph\n------");
        b.Traverse(root);

        Console.WriteLine("\nSearch in Graph\n------");
        Employee e = b.Search(root, "Eva");
        Console.WriteLine(e == null ? "Employee not found" : e.name);
        e = b.Search(root, "Brian");
        Console.WriteLine(e == null ? "Employee not found" : e.name);
        e = b.Search(root, "Soni");
        Console.WriteLine(e == null ? "Employee not found" : e.name);
    }
}


class DFS
{
    public class Employee
    {
        public Employee(string name)
        {
            this.name = name;
        }

        public string name { get; set; }
        public List<Employee> Employees
        {
            get
            {
                return EmployeesList;
            }
        }

        public void isEmployeeOf(Employee e)
        {
            EmployeesList.Add(e);
        }

        List<Employee> EmployeesList = new List<Employee>();

        public override string ToString()
        {
            return name;
        }
    }

    public class DepthFirstAlgorithm
    {
        public Employee BuildEmployeeGraph()
        {
            Employee Eva = new Employee("Eva");
            Employee Sophia = new Employee("Sophia");
            Employee Brian = new Employee("Brian");
            Eva.isEmployeeOf(Sophia);
            Eva.isEmployeeOf(Brian);

            Employee Lisa = new Employee("Lisa");
            Employee Tina = new Employee("Tina");
            Employee John = new Employee("John");
            Employee Mike = new Employee("Mike");
            Sophia.isEmployeeOf(Lisa);
            Sophia.isEmployeeOf(John);
            Brian.isEmployeeOf(Tina);
            Brian.isEmployeeOf(Mike);

            return Eva;
        }

        public Employee Search(Employee root, string nameToSearchFor)
        {
            if (nameToSearchFor == root.name)
                return root;

            Employee personFound = null;
            for (int i = 0; i < root.Employees.Count; i++)
            {
                personFound = Search(root.Employees[i], nameToSearchFor);
                if (personFound != null)
                    break;
            }
            return personFound;
        }

        public void Traverse(Employee root)
        {
            Console.WriteLine(root.name);
            for (int i = 0; i < root.Employees.Count; i++)
            {
                Traverse(root.Employees[i]);
            }
        }
    }

    public void Main()
    {
        DepthFirstAlgorithm b = new DepthFirstAlgorithm();
        Employee root = b.BuildEmployeeGraph();
        Console.WriteLine("Traverse Graph\n------");
        b.Traverse(root);

        Console.WriteLine("\nSearch in Graph\n------");
        Employee e = b.Search(root, "Eva");
        Console.WriteLine(e == null ? "Employee not found" : e.name);
        e = b.Search(root, "Brian");
        Console.WriteLine(e == null ? "Employee not found" : e.name);
        e = b.Search(root, "Soni");
        Console.WriteLine(e == null ? "Employee not found" : e.name);
    }
}


class PriorityQueue
{
    int heapSize;
    List<Node> nodeList;

    public List<Node> NodeList
    {
        get
        {
            return nodeList;
        }
    }

    public PriorityQueue(List<Node> nl)
    {
        heapSize = nl.Count;
        nodeList = new List<Node>();

        for (int i = 0; i < nl.Count; i++)
            nodeList.Add(nl[i]);
    }

    public PriorityQueue()
    {
    }

    public void exchange(int i, int j)
    {
        Node temp = nodeList[i];

        nodeList[i] = nodeList[j];
        nodeList[j] = temp;
    }

    public void heapify(int i)
    {
        int l = 2 * i + 1;
        int r = 2 * i + 2;
        int largest = -1;

        if (l < heapSize && nodeList[l].Key > nodeList[i].Key)
            largest = l;
        else
            largest = i;
        if (r < heapSize && nodeList[r].Key > nodeList[largest].Key)
            largest = r;
        if (largest != i)
        {
            exchange(i, largest);
            heapify(largest);
        }
    }

    public void buildHeap()
    {
        for (int i = heapSize / 2; i >= 0; i--)
            heapify(i);
    }

    int heapSearch(Node node)
    {
        for (int i = 0; i < heapSize; i++)
        {
            Node aNode = nodeList[i];

            if (node.Id == aNode.Id)
                return i;
        }

        return -1;
    }

    public int size()
    {
        return heapSize;
    }

    public Node elementAt(int i)
    {
        return nodeList[i];
    }

    public void heapSort()
    {
        int temp = heapSize;

        buildHeap();
        for (int i = heapSize - 1; i >= 1; i--)
        {
            exchange(0, i);
            heapSize--;
            heapify(0);
        }

        heapSize = temp;
    }

    public Node extractMin()
    {
        if (heapSize < 1)
            return null;

        heapSort();

        exchange(0, heapSize - 1);
        heapSize--;
        return nodeList[heapSize];
    }

    public int find(Node node)
    {
        return heapSearch(node);
    }
}


class KruskalAlgorithm
{
    public void Main()
    {
        Initialization();
        Kruskal();
        Output();
        Console.ReadLine();
    }

    // struct
    struct Edge
    {
        public int u, v;
    }

    // Variables
    static int[,] graph; // do thi
    static List<int> MST; // danh sach dinh trong cay khung
    static List<Edge> EDGE; // danh sach canh trong cay khung

    // method
    static void Kruskal()
    {
        List<Edge> lstEdge;
        int[] Label;
        int n = graph.GetLength(0);

        // buoc 1: tao danh sach canh, sap xep tang dan trong so
        lstEdge = new List<Edge>(); // danh sach canh
        Label = new int[n];
        for (int i = 0; i < n; i++)
        {
            Label[i] = i;
            for (int j = i + 1; j < n; j++)
            {
                if (graph[i, j] != 0)
                {
                    Edge e = new Edge();
                    e.u = i; e.v = j;
                    lstEdge.Add(e);
                }
            }
        }
        // xuat danh sach sap xep
        Console.WriteLine("Danh sach canh: (u,v) : w");
        foreach (Edge e in lstEdge)
        {
            Console.WriteLine("({0},{1}) : {2}", e.u, e.v, graph[e.u, e.v]);
        }
        Console.WriteLine();

        for (int i = 0; i < lstEdge.Count - 1; i++)
        {
            for (int j = i + 1; j < lstEdge.Count; j++)
            {
                if (CompareEdge(lstEdge[i], lstEdge[j]) == 1)
                {
                    Edge t = lstEdge[i];
                    lstEdge[i] = lstEdge[j];
                    lstEdge[j] = t;
                }
            }
        }

        // buoc 2: tao cay T, duyet danh sach
        foreach (Edge e in lstEdge)
        {
            if (Label[e.u] != Label[e.v])
            {
                // them canh vao cay
                EDGE.Add(e);
                Console.WriteLine("Add ({0}, {1}) to EDGE MST", e.u, e.v);

                // them dinh vao danh sach canh cua cay
                if (!MST.Contains(e.u)) MST.Add(e.u);
                if (!MST.Contains(e.v)) MST.Add(e.v);

                // thay doi nhan
                int lab1 = (Label[e.u] < Label[e.v]) ? Label[e.u] : Label[e.v]; // lay cai nho nhat
                int lab2 = (Label[e.u] > Label[e.v]) ? Label[e.u] : Label[e.v]; // lay cai lon nhat
                for (int i = 0; i < n; i++)
                {
                    if (Label[i] == lab2) Label[i] = lab1;
                }
            }
        }
    }

    // compare
    static int CompareEdge(Edge e1, Edge e2)
    {
        if (graph[e1.u, e1.v] > graph[e2.u, e2.v]) return 1;
        else if (graph[e1.u, e1.v] == graph[e2.u, e2.v]) return 0;
        return -1;
    }

    // initial
    static void Initialization()
    {
        //graph = new int[,]
        //{
        //    {0,5,0,3,0},
        //    {5,0,0,8,10},
        //    {0,0,0,7,0},
        //    {3,8,7,0,1},
        //    {0,10,0,1,0}
        //};

        graph = new int[,]
        {
                {0,7,0,5,0,0,0},
                {7,0,8,9,7,0,0},
                {0,8,0,0,5,0,0},
                {5,9,0,0,15,6,0},
                {0,7,5,15,0,8,9},
                {0,0,0,6,8,0,11},
                {0,0,0,0,9,11,0}
        };
        MST = new List<int>();
        EDGE = new List<Edge>();
    }

    static void Output()
    {
        Console.Write("Danh sach dinh: ");
        foreach (int i in MST)
        {
            Console.Write("{0} ", i);
        }
        Console.WriteLine();

        Console.Write("Danh sach canh: ");
        foreach (Edge e in EDGE)
        {
            Console.Write("({0}, {1}) ", e.u, e.v);
        }
    }
}




