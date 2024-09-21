using System.Numerics;

public class KeyboardHandler
{
    private int[,] maze;
    int height;
    int width;
    private bool gameEnded = false;
    private bool isModifiedPath = false;

    public bool IsGameEnded => gameEnded;

    public KeyboardHandler(int[,] maze, int height, int wight)
    {
        this.maze = maze;
        this.width = wight;
        this.height = height;
    }

    public void HandleKeyPress(ConsoleKey key)
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
        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(1); y++)
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
        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                if (maze[x, y] == 3)
                {
                    maze[x, y] = 2;
                }
            }
        }
    }
    private void EndGame()
    {
        gameEnded = true;
    }
}
