// ----------------------------------------------------------------------------
// CSA - C# in Action
// (c) 2024, Christian Jost, HSLU
// ----------------------------------------------------------------------------
using System;
using System.Device.Gpio;
using System.Diagnostics;
using System.Threading;

namespace Explorer700Library
{
    public class Joystick
    {
        #region members & events
        private int centerPin;
        private int UP;
        private int DOWN;
        private int LEFT;
        private int RIGHT;
        public event EventHandler<KeyEventArgs> JoystickChanged; //vorgegebene Form
        #endregion

        #region constructor & destructor
        public Joystick(Pcf8574 pcf8574, GpioController gpioController)
        {
            Pcf8574 = pcf8574;
            pcf8574.Mask |= 0x0F;
            UP = 1;
            DOWN = 2;
            LEFT = 0;
            RIGHT = 3;
            centerPin = 20;
            GpioController = gpioController;
            GpioController.OpenPin(centerPin, PinMode.InputPullUp);

            // Start Polling-Thread
            Thread t = new Thread(Run);
            t.IsBackground = true;
            t.Start();
        }
        #endregion

        #region properties
        private Pcf8574 Pcf8574 { get; set; }

        internal GpioController GpioController { get; }

        /// <summary>
        /// Liest und liefert den Zustand des Joysticks
        /// </summary>
        public Keys Keys
        {
            get
            {
                Keys k = Keys.NoKey;
                // ToDo
                if (!(bool)GpioController.Read(centerPin)) k |= Keys.Center; //wenn die Mitteltaste nicht gedrückt ist (?)
                if (!(bool)Pcf8574[UP]) k |= Keys.Up; 
                if (!(bool)Pcf8574[DOWN]) k |= Keys.Down;
                if (!(bool)Pcf8574[LEFT]) k |= Keys.Left;
                if (!(bool)Pcf8574[RIGHT]) k |= Keys.Right;
                return k;
            }
        }
        #endregion

        #region methods
        /// <summary>
        /// Pollt alle 50ms den Joystick und generiert ein JoystickChanged Event, falls
        /// sich der Zustand des Joysticks (Taste gedrückt/losgelassen) verändert hat.
        /// </summary>
        private void Run()
        {
            Keys oldState = Keys;
            while (true)
            {

                Keys newState = this.Keys;
                // ToDo
                if (oldState != newState) //wenn sich die gedrückte Taste verändert hat
                {
                    JoystickChanged.Invoke(this, new KeyEventArgs(newState));
                    oldState = this.Keys; //den alten Status kopieren
                }
                Thread.Sleep(50);
            }
        }
        #endregion
    }
}
