using System;

namespace KorobeynikovaMaze
{
    /// <summary>
    /// Обработка нажатий клавиш
    /// </summary>
    public class KeyboardHandler
    {
        private int[,] maze;
        public bool IsGameEnded { get; private set; }
        private bool isModifiedPath = true;

        /// <summary>
        /// Приравнивание массива лабиринта
        /// </summary>
        /// <param name="maze">Главный массив лабиринта</param>
        public KeyboardHandler(int[,] maze)
        {
            this.maze = maze;
        }

        /// <summary>
        /// Обработка нажатий клавиш
        /// </summary>
        /// <param name="key">Нажатая клавиша</param>
        /// <param name="playerX">Позиция x игрока</param>
        /// <param name="playerY"Позиция y игрока></param>
        public void HandleKeyPress(ConsoleKey key, ref int playerX, ref int playerY)
        {
            int newX = playerX, newY = playerY;

            switch (key)
            {
                case ConsoleKey.W:
                    newY -= 1;
                    break;
                case ConsoleKey.S:
                    newY += 1;
                    break;
                case ConsoleKey.A:
                    newX -= 1;
                    break;
                case ConsoleKey.D:
                    newX += 1;
                    break;
                case ConsoleKey.X:
                    EndGame();
                    return;
                case ConsoleKey.E:
                    var path = MazeSolver.FindShortestPath(maze, maze.GetLength(0), maze.GetLength(1));
                    if (isModifiedPath)
                    {
                        foreach (var (x, y) in path)
                        {
                            maze[x, y] = 4;
                        }
                    }
                    else
                    {
                        foreach (var (x, y) in path)
                        {
                            maze[x, y] = 0;
                        }
                    }
                    isModifiedPath = !isModifiedPath;
                    break;
            }

            if (maze[newX, newY] != 1)
            {
                playerX = newX;
                playerY = newY;
            }
        }

        /// <summary>
        /// Завершение игры
        /// </summary>
        public void EndGame()
        {
            IsGameEnded = true;
        }
    }
}
