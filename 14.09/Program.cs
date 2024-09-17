using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

class Maze
{
    public static void Main(string[] args)
    {
        int width = 40;
        int height = 10;

        int[,] maze = GenerateMaze(width, height);
        GenerateDopPath(maze);
        KeyboardHandler handler = new KeyboardHandler(maze, height, width);

        while (!handler.IsGameEnded)
        {
            PrintMazeWithSolution(maze);

            ConsoleKeyInfo info = Console.ReadKey(true);
            handler.HandleKeyPress(info.Key);

            PrintMazeWithSolution(maze);
        }
    }
    public static int[,] GenerateMaze(int width, int height)
    {
        int[,] maze = new int[width, height];
        Random rand = new Random();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                maze[x, y] = 1;
            }
        }

        int StartMaze;
        StartMaze = rand.Next(0, height - 1);
        maze[0, StartMaze] = 2;

        int currentX = 1;
        int currentY = StartMaze;
        maze[currentX, currentY] = 2;

        while (currentX < width - 1)
        {
            int[] numbers = { 1, 2, 3, 4 };
            int randomIndex = rand.Next(numbers.Length);

            switch (randomIndex)
            {
                case 1:
                    if (currentY > 1)
                        if (maze[currentX, currentY - 1] != 2)
                            currentY--;
                    break;
                case 2:
                    if (currentY < height - 2)
                        if (maze[currentX, currentY + 1] != 2)
                            currentY++;
                    break;
                case 3:
                    if (currentX < width - 1)
                        currentX++;
                    break;
                case 4:
                    if (currentX > 1)
                        if (maze[currentX - 1, currentY] != 2)
                            currentX--;
                    break;
            }
            maze[currentX, currentY] = 2;
        }
        return maze;
    }
    public static int[,] GenerateDopPath(int[,] maze)
    {

        return maze;
    }
    private static void PrintMazeWithSolution(int[,] maze)
    {
        Console.Clear();
        for (int y = 0; y < maze.GetLength(1); y++)
        {
            for (int x = 0; x < maze.GetLength(0); x++)
            {
                switch (maze[x, y])
                {
                    case 0: // Пустое пространство
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("▒");
                        break;
                    case 1: // Стена
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("█");
                        break;
                    case 2: 
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" ");
                        break;
                    case 3: // Стена
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("▒");
                        break;

                }
            }
            Console.WriteLine();
        }
        Console.WriteLine("\nНажмите 'E' для подсказки\nНажмите 'X' для выхода из программы");
        Console.ResetColor();
    }
}