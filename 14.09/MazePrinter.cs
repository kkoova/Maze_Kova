using System;

namespace KorobeynikovaMaze
{
    /// <summary>
    /// Вывод лабиринт 
    /// </summary>
    public class MazePrinter
    {
        /// <summary>
        /// Отображение лабиринта в консоли
        /// </summary>
        /// <param name="maze">Главный массив лабиринта</param>
        /// <param name="playerX">Позиция x игрока</param>
        /// <param name="playerY">Позиция y игрока</param>
        /// <param name="visibilityRadius">Радиус "тумана войны"</param>
        public static void PrintMaze(int[,] maze, int playerX, int playerY, int visibilityRadius)
        {
            int width = maze.GetLength(0);
            int height = maze.GetLength(1);

            maze[1, 1] = 2;
            maze[width - 2, height - 2] = 3;

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
    }
}
