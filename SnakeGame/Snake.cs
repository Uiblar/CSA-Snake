using Explorer700Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UnitsNet;

namespace SnakeGame
{
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    internal class Snake
    {
        private int length;
        private string direction;
        private LinkedList<Point> snakeBody;
        public Graphics g;

        public string Direction { get => direction; set => direction = value; }

        public Snake(int startX, int startY, Graphics g)
        {
            length = 20;
            snakeBody = new LinkedList<Point>();
            Direction = "Right";
            this.g = g;
            for (int i = 0; i < length; i++)
            {
                Console.WriteLine("Element der Schlange wird erstellt: " + i + ", Position: x: " + (startX - (i * 3)) + ", y: " + startY);
                snakeBody.AddLast(new Point(startX - (i * 3), startY));
            }
            Draw();
        }

        public void Draw()
        {
            foreach (Point snakeSegment in snakeBody)
            {
                g.FillRectangle(Brushes.White, snakeSegment.X, snakeSegment.Y, 2, 2);
                //g.DrawRectangle(p, snakeSegment.X * 20, snakeSegment.Y * 20, 20, 20);

            }
        }

        public void Move(string direction)
        {
            // Remove the tail
            snakeBody.RemoveLast();
            // Calculate new head position
            Point head = snakeBody.First.Value;
            Point newHead = new Point(head.X, head.Y);
            switch (direction)
            {
                case "Up":
                    newHead.Y -= 2;
                    break;
                case "Down":
                    newHead.Y += 2;
                    break;
                case "Left":
                    newHead.X -= 2;
                    break;
                case "Right":
                    newHead.X += 2;
                    break;
            }
            snakeBody.AddFirst(newHead);
            Draw();
        }

        public void Move()
        {
            this.Move(this.Direction);
        }

        void OnJoystickChanged(object sender, KeyEventArgs args)
        {
            Direction = "up";
        }

        public override string ToString()
        {
            return $"Anzahl Elemente: {snakeBody.Count}, direction: {this.Direction}";
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }

 

        internal bool DetectFoodCollision(int foodX, int foodY)
        {
            // Get the head position of the snake
            Point head = snakeBody.First.Value;
            // Check collision with food
            if (head.X == foodX && head.Y == foodY)
            {
                Console.WriteLine("Collision with food! ");
                AddSegment();                
                return true; // Collided with the food
            }
            return false; // No collision detected
        }

        private void AddSegment()
        {
            // Calculate new head position
            Point head = snakeBody.First.Value;
            Point newHead = new Point(head.X, head.Y);
            switch (direction)
            {
                case "Up":
                    newHead.Y -= 2;
                    break;
                case "Down":
                    newHead.Y += 2;
                    break;
                case "Left":
                    newHead.X -= 2;
                    break;
                case "Right":
                    newHead.X += 2;
                    break;
            }
            snakeBody.AddFirst(newHead);
            Draw();
        }

        internal bool DetectWallCollision(int BoardWidth, int BoardHeight)
        {
            // Get the head position of the snake
            Point head = snakeBody.First.Value;

            // Check collision with walls
            if (head.X < 0 || head.X > BoardWidth || head.Y < 0 || head.Y > BoardHeight)
            {
                Console.WriteLine("An die Wand gefahren! ");
                return true; // Collided with the walls
            }
            return false;
        }

        internal bool DetectCollisionWithSelf()
        {
            // Check collision with itself
            LinkedListNode<Point> headNode = snakeBody.First;
            LinkedListNode<Point> currentSegment = snakeBody.First.Next;//mit dem zweiten Element anfangen
            while (currentSegment != null)
            {
                if (headNode.Value.X == currentSegment.Value.X && headNode.Value.Y == currentSegment.Value.Y)
                {
                    Console.WriteLine("Collision with self! ");
                    return true; // Collided with itself
                }
                currentSegment = currentSegment.Next;
            }
            return false;
        }
    }
}
