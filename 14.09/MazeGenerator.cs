using System;
using System.Collections.Generic;

public class MazeGenerator
{
    static int width = 41;
    static int height = 21;
    static int[,] maze = new int[width, height];

    public static void Main()
    {
        GenerateMaze();
        PrintMaze();
    }

    static void GenerateMaze()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                maze[x, y] = 1;
            }
        }

        Random rand = new Random();
        Stack<(int x, int y)> stack = new Stack<(int x, int y)>();
        int startX = 1, startY = 1;
        maze[startX, startY] = 0;
        stack.Push((startX, startY));

        (int x, int y)[] directions = { (0, -2), (0, 2), (-2, 0), (2, 0) };

        while (stack.Count > 0)
        {
            var (currentX, currentY) = stack.Peek();
            List<(int x, int y)> neighbors = new List<(int x, int y)>();

            foreach (var (dx, dy) in directions)
            {
                int nx = currentX + dx;
                int ny = currentY + dy;
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
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (maze[x, y] == 1)
                {
                    Console.Write("█");
                }
                else
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }
    }
}
