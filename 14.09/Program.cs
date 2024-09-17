using System;
using System.Collections.Generic;

class Maze
{
    public static void Main(string[] args)
    {
        int width = 40;
        int height = 20;

        int[,] maze = GenerateMaze(width, height);

        PrintMazeWithSolution(maze);

    }

    public static int[,] GenerateMaze(int width, int height)
    {
        int[,] maze = new int[width, height];
        Random rand = new Random();

        // Создание границы вокруг всего лабиринта
        for (int x = 0; x < width; x++)
        {
            maze[x, 0] = 1; // Верхняя граница
            maze[x, height - 1] = 1; // Нижняя граница
        }
        for (int y = 0; y < height; y++)
        {
            maze[0, y] = 1; // Левая граница
            maze[width - 1, y] = 1; // Правая граница
        }

        // Случайный выбор начала лабиринта
        int StartMaze;
        StartMaze = rand.Next(0, height - 1);
        maze[0, StartMaze] = 0;

        // Генерация пути
        int currentX = 1;
        int currentY = StartMaze;
        while (currentX < width - 1)
        {
            GenerateWalls();
            maze[currentX, currentY] = 3; // Отмечаем текущую ячейку как часть пути

            // Двигаемся вправо
            currentX++;
            GenerateWalls();
            maze[currentX, currentY] = 3;

            // Случайно поднимаемся или опускаемся на одну ячейку
            if (rand.NextDouble() < 0.5 && currentY > 1)
            {
                currentY--;
                maze[currentX, currentY] = 3;
                GenerateWalls();
                currentX++;
            }
            else if (rand.NextDouble() > 0.5 && currentY < height - 2)
            {
                currentY++;
                maze[currentX, currentY] = 3;
                GenerateWalls();
                currentX++;
            }
            else if (rand.NextDouble() < 0.9 && currentX < width - 1)
            {
                maze[currentX, currentY] = 3;
                currentX++;
            }
        }

        //Выход из лабиринта
        maze[currentX, currentY] = 0;

        return maze;

        void GenerateWalls()
        {
            // Создаем стену сверху
            if (currentY > 0 && maze[currentX, currentY - 1] != 3)
                maze[currentX, currentY - 1] = 1;

            // Создаем стену снизу
            if (currentY < height - 1 && maze[currentX, currentY + 1] != 3)
                maze[currentX, currentY + 1] = 1;
        }
    }


    private static void PrintMazeWithSolution(int[,] maze)
    {
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
                    case 3: // Стена
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("▒");
                        break;
                }
            }
            Console.WriteLine();
        }
        Console.ResetColor();
    }
}