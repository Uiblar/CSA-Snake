using Explorer700Library;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;

namespace Explorer700Demo
{
    class Program
    {
        private static Explorer700 exp;

        static void Main(string[] args)
        {
            Console.WriteLine("Start...");
            exp = new Explorer700();
            exp.Led1.Enabled = false;
            exp.Led2.Enabled = true;
            exp.Buzzer.Beep(1000);

            //exp.Joystick.JoystickChanged += OnJoystickChanged;



            // Eingebettete Bild Ressource "test.png" laden und auf dem Display darstellen
            var resNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            Stream imageStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Explorer700Demo.Ressources.snake3.png");
            Image image = Image.FromStream(imageStream);

            Graphics g = exp.Display.Graphics;
            //Pen pen = new Pen(Brushes.White);
            //g.DrawEllipse(pen, -10, -10, 30, 30);
            //g.DrawEllipse(pen, 30, 10, 10, 10);
            //pen.Width = 2;
            //g.DrawBezier(pen, new Point(10, 30), new Point(30, 30), new Point(70, 40), new Point
            //(75, 5));
            //g.DrawString("Hello .NET :-)", new Font(new FontFamily("arial"), 8, FontStyle.Bold),
            //Brushes.White, new PointF(5, 50));
            g.DrawImage(image, 0, 0);
            exp.Display.Update();



            ///static void OnJoystickChanged(object sender, KeyEventArgs args)
            ///{
            ///Console.WriteLine($"Taste {args.Keys} gedrückt.");
            ///}

            Console.ReadKey();
        }
    }
}
