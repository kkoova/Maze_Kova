using System;
using System.Numerics;
using System.Collections.Generic;

namespace KorobeynikovaMaze
{
    public class KeyboardHandler
    {
        private readonly int[,] maze;
        private bool gameEnded = false;
        private bool isModifiedPath = false;

        public bool IsGameEnded => gameEnded;

        public KeyboardHandler(int[,] maze)
        {
            this.maze = maze;
        }

        // Обработка нажатий клавиш
        public void HandleKeyPress(ConsoleKey key, ref int playerX, ref int playerY)
        {
            if (gameEnded)
            {
                return;
            }

            switch (key)
            {
                case ConsoleKey.E:
                    ToggleModifiedPath();  // Включение/выключение отображения кратчайшего пути
                    break;
                case ConsoleKey.X:
                    EndGame();  // Завершение игры
                    break;
                case ConsoleKey.W:
                    MoveUp(ref playerX, ref playerY);  // Движение вверх
                    break;
                case ConsoleKey.S:
                    MoveDown(ref playerX, ref playerY);  // Движение вниз
                    break;
                case ConsoleKey.A:
                    MoveLeft(ref playerX, ref playerY);  // Движение влево
                    break;
                case ConsoleKey.D:
                    MoveRight(ref playerX, ref playerY);  // Движение вправо
                    break;
            }
        }

        // Включение или выключение кратчайшего пути
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

        // Применение кратчайшего пути (помечаем его в лабиринте)
        private void ApplyModifiedPath()
        {
            var path = MazeGenerator.FindShortestPath();
            foreach (var (px, py) in path)
            {
                maze[px, py] = 4; // Обозначаем путь символом "."
            }
        }

        // Возвращение к исходному виду лабиринта (снятие отметок пути)
        private void ResetToOriginalPath()
        {
            var path = MazeGenerator.FindShortestPath();
            foreach (var (px, py) in path)
            {
                maze[px, py] = 0;  // Сбрасываем путь обратно на пустое место
            }
        }

        // Завершение игры
        public void EndGame()
        {
            gameEnded = true;
        }

        // Движение вверх
        private void MoveUp(ref int playerX, ref int playerY)
        {
            if (playerY > 0 && maze[playerX, playerY - 1] != 1)
            {
                playerY--;
            }
        }

        // Движение вниз
        private void MoveDown(ref int playerX, ref int playerY)
        {
            if (playerY < maze.GetLength(1) - 1 && maze[playerX, playerY + 1] != 1)
            {
                playerY++;
            }
        }

        // Движение влево
        private void MoveLeft(ref int playerX, ref int playerY)
        {
            if (playerX > 0 && maze[playerX - 1, playerY] != 1)
            {
                playerX--;
            }
        }

        // Движение вправо
        private void MoveRight(ref int playerX, ref int playerY)
        {
            if (playerX < maze.GetLength(0) - 1 && maze[playerX + 1, playerY] != 1)
            {
                playerX++;
            }
        }
    }
}
