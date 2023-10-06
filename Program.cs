namespace PrimeAlg
{
    /// <summary>
    /// Нахождение минимального остовного дерева по алгоритму Прима.
    /// </summary>
    internal class Program
    {

        //                                 ЛЕГЕНДА

        //                              A_______5_____B 
        //                             / \           / \   
        //                            /   \         /   \ 
        //                           /     \       /     \ 
        //                          / 7     \ 2   / 3     \ 9 
        //                         /         \   /         \ 
        //                        /           \ /           \ 
        //                     C /             G             \ D 
        //                       \            / \            / 
        //                        \          /   \          / 
        //                         \        /     \        / 
        //                          \ 8    / 4     \ 6    / 4 
        //                           \    /         \    / 
        //                            \  /           \  / 
        //                             \/______5______\/ 
        //                             E               F 

        static void Main()
        {
            // зададим вершины
            var verts = new char[]  { 'A', 'B', 'C', 'D', 'E', 'F', 'G' };

            // зададим матрицу смежности ребер
            var weigs = new int[,] { { 1000,    5,    7,    1000,   1000,    1000,    2 },

                                     {   5,   1000,  1000,    9,    1000,    1000,    3 },

                                     {   7,   1000,  1000,  1000,     8,     1000,   1000 },

                                     { 1000,    9,   1000,  1000,   1000,      4,    1000 },

                                     { 1000,  1000,   8,    1000,   1000,      5,     4 },

                                     { 1000,  1000,  1000,    4,      5,     1000,    6 },

                                     {   2,     3,   1000,   1000,    4,       6,    1000} };
            // строим связный граф
            Graph graph = new Graph(7,verts,weigs);
            MinimumSpanningTree mst = new MinimumSpanningTree(graph);
            mst.Prim(0);
            Console.ReadLine();
        }
    }

    /// <summary>
    /// Класс для создания и хранения информации о графике.
    /// </summary>
    class Graph
    {
        private int _vertexNum;     //количество вершин
        private char[] _vertexs;    // массив вершин
        private int[,] _weights;    // матрица смежности ребер
        public Graph(int vertexNum, char[] vertexs, int[,] weights)
        {
            _vertexNum = vertexNum;
            _vertexs = vertexs;
            _weights = weights;
        }
        public int VertexNum
        {
            get { return _vertexNum; }
        }
        public char[] Vertexs
        {
            get { return _vertexs; }
        }
        public int[,] Weights
        {
            get { return _weights; }
        }
    }

    /// <summary>
    /// класс для построения минимального остовного дерева
    /// </summary>
    class MinimumSpanningTree
    {
        private Graph _graph;
        public MinimumSpanningTree(Graph graph)
        {
            this._graph = graph;
        }
        public void Prim(int start)
        {
            bool[] visited = new bool[_graph.VertexNum]; // массив посещенных вершин
            visited[start] = true;

            // определяем дорожные карты от наименьших весов к большим
            for(int k = 0; k < _graph.VertexNum-1; k++)
            {
                var minWeight = 1000;
                var vertex1 = -1;
                var vertex2 = -1;

                // Для всех вершин, которые были посещены,
                // находим ребро с наименьшим весом среди всех из непосещенных ребер
                for (var i = 0; i < _graph.VertexNum; i++)
                {
                    if (!visited[i])
                        continue;
                    for( var j = 0; j < _graph.VertexNum; j++)
                    {
                        if ((_graph.Weights[i,j] < minWeight) & (!visited[j]))
                        {
                            minWeight = _graph.Weights[i,j];
                            vertex1 = i;
                            vertex2 = j;
                        }
                    }
                }
                // найденное ребро выводим на печать и помечаем его как посещенное.
                visited[vertex2] = true;
                Console.WriteLine($"{_graph.Vertexs[vertex1]}--{_graph.Vertexs[vertex2]} ({minWeight})");
            }
        }
    }
}