using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

class Maze
{
    public static void Main(string[] args)
    {
        int width = 50;
        int height = 10;

        int[,] maze = GenerateMaze(width, height);
        GenNewMaz(maze);

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
            int randomIndex = rand.Next(1,4);

            switch (randomIndex)
            {
                case 1:
                    if (currentY > 1)
                        if (maze[currentX, currentY - 1] != 2)
                        currentY--;
                    break;
                case 2:
                    if (currentY < maze.GetLength(1) - 2)
                        if (maze[currentX, currentY + 1] != 2)
                        currentY++;
                    break;
                case 3:
                    if (currentX < maze.GetLength(0) - 1)
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

    private static int [,] GenNewMaz(int[,] maze)
    {
        Random rand = new Random();
        for (int i = 0; i < maze.GetLength(0) + maze.GetLength(1); i++)
        {
            int X = rand.Next(1, maze.GetLength(0) - 2);
            int Y = rand.Next(1, maze.GetLength(1) - 2); 
            
            for (int j = 0; j <= rand.Next(15, maze.GetLength(0) + 5); j++)
            {
                switch (rand.Next(1, 4))
                {
                    case 1:
                        if (Y > 1)
                                Y--;
                        break;
                    case 2:
                        if (Y < maze.GetLength(1) - 2)
                                Y++;
                        break;
                    case 3:
                        if (X < maze.GetLength(0) - 2)
                            X++;
                        break;
                    case 4:
                        if (X > 1)
                                X--;
                        break;
                }
                if (maze[X, Y] == 2)
                    break;
                else
                    maze[X, Y] = 0;
            }
        }

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
                    case 0: 
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" ");
                        break;
                    case 1: 
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("█");
                        break;
                    case 2: 
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" ");
                        break;
                    case 3: 
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("∙");
                        break;

                }
            }
            Console.WriteLine();
        }
        Console.WriteLine("\nНажмите 'E' для подсказки\nНажмите 'X' для выхода из программы");
        Console.ResetColor();
    }
}