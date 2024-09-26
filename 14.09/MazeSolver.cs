using System;
using System.Collections.Generic;

namespace KorobeynikovaMaze
{
    /// <summary>
    /// Поиск кратчайшего пути
    /// </summary>
    public class MazeSolver
    {
        /// <summary>
        /// Поиск кратчайшего пути
        /// </summary>
        /// <param name="maze">Главный массив лабиринта</param>
        /// <param name="width">Ширина лабиринта</param>
        /// <param name="height">Высота лабиринта</param>
        /// <returns></returns>
        public static List<(int x, int y)> FindShortestPath(int[,] maze, int width, int height)
        {
            var start = (x: 1, y: 1);
            var end = (x: width - 2, y: height - 2);
            var queue = new Queue<(int x, int y)>();
            var visited = new HashSet<(int x, int y)>();
            var parent = new Dictionary<(int x, int y), (int x, int y)>();

            queue.Enqueue(start);
            visited.Add(start);

            (int x, int y)[] directions = { (0, -1), (0, 1), (-1, 0), (1, 0) };

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (current == end)
                {
                    var path = new List<(int x, int y)>();
                    while (current != start)
                    {
                        path.Add(current);
                        current = parent[current];
                    }
                    path.Add(start);
                    path.Reverse();
                    return path;
                }

                foreach (var (dx, dy) in directions)
                {
                    var neighbor = (x: current.x + dx, y: current.y + dy);
                    if (neighbor.x >= 0 && neighbor.x < width && neighbor.y >= 0 && neighbor.y < height &&
                        maze[neighbor.x, neighbor.y] != 1 && !visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        parent[neighbor] = current;
                        queue.Enqueue(neighbor);
                    }
                }
            }

            return new List<(int x, int y)>();
        }
    }
}
