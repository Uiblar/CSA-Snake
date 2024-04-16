using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Explorer700Library;
using UnitsNet;


namespace SnakeGame
{

    internal class SnakeGame
    {
        public Snake snake;
        public Food food;
        private bool gameOver = false;
        private Explorer700 exp;
        static Graphics g;
        static int delay = 100;
        public int BoardWidth;
        public int BoardHeight;
        public int Score { get; private set; } = 0;
        private volatile bool startGameRequested = false;

        public SnakeGame(Explorer700 exp, int BoardWidth, int BoardHeight)
        {
            this.exp = exp;
            this.BoardWidth = BoardWidth;
            this.BoardHeight = BoardHeight;
            g = exp.Display.Graphics;
            snake = new Snake(BoardWidth / 2, BoardHeight / 2, g);
            food = new Food(BoardWidth-1, BoardHeight-1, g);
        }
        public void UpdateDisplay() {
            exp.Display.Clear();
            snake.Move();
            food.Draw(2);
            // Drawing the score box
            g.DrawRectangle(Pens.White, BoardWidth - 47, 2, 45, 15); 
            g.DrawString($" {Score}", new Font("Arial", 8), Brushes.White, new PointF(BoardWidth - 42, 3));
            exp.Display.Update();
        }

        public void Run()
        {
            exp.Joystick.JoystickChanged += OnJoystickChanged;
            if (!startGameRequested) {
                DisplayHomeScreen();
            }
            while (!gameOver)
            {
                for (int i = 0; i < 100; i++)
                {
                    UpdateDisplay();
                    if (snake.DetectCollisionWithSelf() || snake.DetectWallCollision(BoardWidth, BoardHeight))
                    {
                        gameOver = true;
                        break;
                    }

                    if(snake.DetectFoodCollision(food.X, food.Y))
                    {
                        exp.Buzzer.Beep(10);
                        food.GenerateRandomPosition();                       
                        Score += 1;
                        UpdateDisplay();
                        i = 0;
                    };
                    Thread.Sleep(delay);
                }
                food.GenerateRandomPosition();
            }

            OnGameOver();
        }

        //void OnGameOver()
        //{
        //    Console.WriteLine("Game Over.");
        //    exp.Display.Clear();
        //    g.DrawString("Game Over :(", new Font(new FontFamily("arial"), 8, FontStyle.Bold), Brushes.White, new PointF(20, BoardHeight/2));
        //    exp.Display.Update();
        //    Thread.Sleep(9000);
        //}
        void OnGameOver() {
            Console.WriteLine("Game Over.");
            exp.Display.Clear();
            g.DrawString("Game Over :(", new Font(new FontFamily("arial"), 8, FontStyle.Bold), Brushes.White, new PointF(20, BoardHeight / 2 - 20));
            g.DrawString("Center to Restart", new Font(new FontFamily("arial"), 8), Brushes.White, new PointF(5, BoardHeight / 2 ));
            g.DrawString("Down to Quit", new Font(new FontFamily("arial"), 8), Brushes.White, new PointF(5, BoardHeight / 2 + 20));
            exp.Display.Update();
            // Wait for the center button to be pressed to restart
            while (true) {
                Thread.Sleep(100);
                if (exp.Joystick.Keys.HasFlag(Keys.Center)) {
                    ResetGame(); //Resets game variables
                    break;
                }
                if (exp.Joystick.Keys.HasFlag(Keys.Down)) {
                    //Close
                    break;
                }
            }
        }

        public void DisplayHomeScreen() {
            g.Clear(Color.Black);
            using (Stream imageStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SnakeGame.Resources.snake.png")) {
                if (imageStream != null) {
                    Image image = Image.FromStream(imageStream);
                    g.DrawImage(image, 0, 0);
                }
            }
            exp.Display.Update();
            // OnJoystickChanged Center sets startGameRequested = true
            while (!startGameRequested) {
                Thread.Sleep(100); // Poll every 100 ms
            }
        }
        void ResetGame() {
            // Reset game variables
            gameOver = false;
            snake = new Snake(BoardWidth / 2, BoardHeight / 2, g);
            food = new Food(BoardWidth - 1, BoardHeight - 1, g);
            // Start the game loop again
            Run();
        }


        void OnJoystickChanged(object sender, KeyEventArgs args)
        {
            if (args.Keys == Keys.Center) {
                startGameRequested = true;
            }
            if (args.Keys != Keys.NoKey && args.Keys != Keys.Center)
            {
                string direction = args.Keys.ToString();
                this.snake.Direction = direction;
            }
        }
    }
}
