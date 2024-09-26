using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using KorobeynikovaMaze;

public class MazeGenerator
{
    static int width;
    static int height;
    static int[,] maze = new int[0, 0];
    static int playerX = 1, playerY = 1;
    static int visibilityRadius = 3;

    public static void Main()
    {
        while (width < 4 || height < 4)
        {
            Console.Write("Введите ширину: ");
            var widthInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(widthInput) || !int.TryParse(widthInput, out width) || width < 4)
            {
                Console.WriteLine("Ошибка: введите корректное число больше 4.");
                continue;
            }
            Console.Write("Введите высоту: ");
            var heightInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(heightInput) || !int.TryParse(heightInput, out height) || height < 4)
            {
                Console.WriteLine("Ошибка: введите корректное число больше 4.");
                continue;
            }
        }

        Console.Clear();

        if (width % 2 == 0)
        { width += 1; }
        if (height % 2 == 0)
        { height += 1; }
        Console.WriteLine("_____________________________" +
            "\nДля выхода - 'X'" +
            "\nДля подсказки - 'E'" +
            "\nУправление:\n\n   W" +
            "\nA  S  D" +
            "\n_____________________________");
        Console.WriteLine("Для продолжение, нажмите на любую клавишу");
        Console.ReadLine();
        Console.Clear();
        maze = new int[width, height];
        GenerateMaze();
        var handler = new KeyboardHandler(maze);

        while (!handler.IsGameEnded)
        {
            PrintMaze();
            ConsoleKeyInfo info = Console.ReadKey(true);
            handler.HandleKeyPress(info.Key, ref playerX, ref playerY);
            if (maze[playerX, playerY] == 3)
            {
                Console.WriteLine("Вы прошли лабиринт, Ура!");
                Console.ReadLine();
                handler.EndGame();
            }
        }
    }
    static void GenerateMaze()
    {
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                maze[x, y] = 1;
            }
        }

        var rand = new Random();
        var stack = new Stack<(int x, int y)>();
        int startX = 1, startY = 1;
        maze[startX, startY] = 0;
        stack.Push((startX, startY));

        (int x, int y)[] directions = { (0, -2), (0, 2), (-2, 0), (2, 0) };

        while (stack.Count > 0)
        {
            var (currentX, currentY) = stack.Peek();
            var neighbors = new List<(int x, int y)>();

            foreach (var (dx, dy) in directions)
            {
                var nx = currentX + dx;
                var ny = currentY + dy;
                if (nx > 0 && nx < width - 1 && ny > 0 && ny < height - 1 && maze[nx, ny] == 1)
                {
                    neighbors.Add((nx, ny));
                }
            }

            if (neighbors.Count > 0)
            {
                var (nextX, nextY) = neighbors[rand.Next(neighbors.Count)];
                maze[(currentX + nextX) / 2, (currentY + nextY) / 2] = 0;
                maze[nextX, nextY] = 0;
                stack.Push((nextX, nextY));
            }
            else
            {
                stack.Pop();
            }
        }
    }

    static void PrintMaze()
    {
        maze[1, 1] = 2;
        maze[maze.GetLength(0) - 2, maze.GetLength(1) - 2] = 3;

        for (int y = 0; y < height; y++)
        {
            Console.SetCursorPosition(0, y);
            for (int x = 0; x < width; x++)
            {
                if (Math.Abs(x - playerX) <= visibilityRadius && Math.Abs(y - playerY) <= visibilityRadius)
                {
                    if (x == playerX && y == playerY)
                    {
                        Console.Write('P');
                    }
                    else if (maze[x, y] == 1)
                    {
                        Console.Write("█");
                    }
                    else if (maze[x, y] == 0)
                    {
                        Console.Write(" ");
                    }
                    else if (maze[x, y] == 2)
                    {
                        Console.Write("<");
                    }
                    else if (maze[x, y] == 3)
                    {
                        Console.Write(">");
                    }
                    else if (maze[x, y] == 4)
                    {
                        Console.Write(".");
                    }
                }
                else
                {
                    Console.Write("▒");
                }
            }
            Console.WriteLine();
        }
    }
    static public List<(int x, int y)> FindShortestPath()
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
