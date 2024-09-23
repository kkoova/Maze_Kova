using System.Numerics;

namespace KorobeynikovaMaze
{
    public class KeyboardHandler(int[,] maze)
    {
        private readonly int[,] maze = maze;
        private bool gameEnded = false;
        private bool isModifiedPath = false;

        public bool IsGameEnded => gameEnded;

        public void HandleKeyPress(ConsoleKey key, ref int playerX, ref int playerY)
        {
            if (gameEnded)
            {
                return;
            }

            switch (key)
            {
                case ConsoleKey.E:
                    ToggleModifiedPath();
                    break;
                case ConsoleKey.X:
                    EndGame();
                    break;
                case ConsoleKey.W:
                    if (playerY > 0 && maze[playerX, playerY - 1] != 1)
                    { playerY--; }
                    break;
                case ConsoleKey.S:
                    if (playerY < maze.GetLength(1) - 1 && maze[playerX, playerY + 1] != 1)
                    { playerY++; }
                    break;
                case ConsoleKey.A:
                    if (playerX > 0 && maze[playerX - 1, playerY] != 1)
                    { playerX--; }
                    break;
                case ConsoleKey.D:
                    if (playerX < maze.GetLength(0) - 1 && maze[playerX + 1, playerY] != 1)
                    { playerX++; }
                    break;
            }
        }
        private void ToggleModifiedPath()
        {
            if (isModifiedPath)
            {
                ResetToOriginalPath();
            }
            else
            {
                ApplyModifiedPath();
            }
            isModifiedPath = !isModifiedPath;
        }

        private void ApplyModifiedPath()
        {
            for (var x = 0; x < maze.GetLength(0); x++)
            {
                for (var y = 0; y < maze.GetLength(1); y++)
                {
                    if (maze[x, y] == 2)
                    {
                        maze[x, y] = 3;
                    }
                }
            }
        }

        private void ResetToOriginalPath()
        {
            for (var x = 0; x < maze.GetLength(0); x++)
            {
                for (var y = 0; y < maze.GetLength(1); y++)
                {
                    if (maze[x, y] == 3)
                    {
                        maze[x, y] = 2;
                    }
                }
            }
        }
        public void EndGame()
        {
            gameEnded = true;
        }
    }
}
