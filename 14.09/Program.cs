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
            maze[x, 0] = 1; 
            maze[x, height - 1] = 1;
        }
        for (int y = 0; y < height; y++)
        {
            maze[0, y] = 1;
            maze[width - 1, y] = 1;
        }

        int StartMaze;
        StartMaze = rand.Next(0, height - 1);
        maze[0, StartMaze] = 0;

        int currentX = 1;
        int currentY = StartMaze;
        while (currentX < width - 1)
        {
            int[] numbers = { 1, 2, 3, 4, 5};
            int randomIndex = rand.Next(numbers.Length);

            switch (randomIndex)
            {
                case 1:
                    if (currentY > 1)
                        currentY--;
                    maze[currentX, currentY] = 2;
                    if (currentY > 1)
                        currentY--;
                    break;
                case 2:
                    if (currentY < height - 2)
                        currentY++;
                    maze[currentX, currentY] = 2;
                    if (currentX < width - 1)
                        currentX++;
                    break;
                case 3:
                    if (currentX < width - 1)
                        currentX++;
                    maze[currentX, currentY] = 2;
                    if (currentX < width - 1)
                        currentX++;
                    break;
                case 4:
                    if (currentX > 1)
                        currentX--;
                    break;
                case 5:
                    if (currentY < height - 2)
                        currentY++;
                    maze[currentX, currentY] = 2;
                    if (currentX > 1)
                        currentX--;
                    break;
            }
            maze[currentX, currentY] = 2;
            //GenerateWalls();
            //currentX++;

            //Выход из лабиринта
            //maze[currentX, currentY] = 0;

            void GenerateWalls()
            {
                // Создаем стену сверху
                if (currentY > 0 && maze[currentX, currentY - 1] != 3)
                    maze[currentX, currentY - 1] = 1;
                if (rand.NextDouble() < 0.5 && currentY > 1)
                {
                    //GenerateMazeDop(currentX, currentY - 1, maze);
                }

                // Создаем стену снизу
                if (currentY < height - 1 && maze[currentX, currentY + 1] != 3)
                    maze[currentX, currentY + 1] = 1;
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
                        Console.Write("*");
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