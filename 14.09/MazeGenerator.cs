using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

public class MazeGenerator
{
    static int width;
    static int height;
    static int[,] maze = new int[0, 0];

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

            Console.Clear();
        }

        if (width % 2 == 0)
        { width += 1; }
        if (height % 2 == 0)
        { height += 1; }

        maze = new int[width, height];

        GenerateMaze();
        KeyboardHandler handler = new KeyboardHandler(maze);

        while (!handler.IsGameEnded)
        {
            PrintMaze();
            ConsoleKeyInfo info = Console.ReadKey(true);
            handler.HandleKeyPress(info.Key);
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
            for (int x = 0; x < width; x++)
            {
                if (maze[x, y] == 1)
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
                else
                {
                    Console.Write(">");
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine("\nДля выхода - 'X'\nДля подсказки - 'E'\nУправление:\n\n   W\nA  S  D");
    }
}
