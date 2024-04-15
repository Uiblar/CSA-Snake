using System;
using System.Drawing;

namespace SnakeGame
{
    internal class Food
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        private readonly Random random;
        public Graphics g;
        public int maxX;
        public int maxY;
        public Food(int maxX, int maxY, Graphics g)
        {
            random = new Random();
            this.maxX = maxX;
            this.maxY = maxY;
            GenerateRandomPosition(maxX, maxY);
            this.g = g;
        }

        public void GenerateRandomPosition(int maxX, int maxY)
        {
            // Generate random X and Y coordinates within the specified range
            X = random.Next(1, maxX / 2)*2;
            Y = random.Next(1, maxY/2)*2;
        }

        public void GenerateRandomPosition()
        {
            GenerateRandomPosition(maxX, maxY);
        }

        public void Draw(int cellSize)
        {
            // Draw the food as a rectangle
            g.FillRectangle(Brushes.White, X, Y, cellSize, cellSize);
        }
    }
}