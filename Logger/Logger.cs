using System.Text;

namespace Logger
{
    public class Logger
    {
        private String filename;
        private Mutex mutex;

        public Logger(String filename, Mutex mutex)
        {
            this.filename = filename;
            this.mutex = mutex;
            if (!File.Exists(this.filename))
            {
                try
                {
                    mutex.WaitOne();
                    using (StreamWriter writer = new StreamWriter(this.filename, true, Encoding.ASCII))
                    {
                        writer.WriteLine("//Logs from SnakeGame - Team 07");
                        writer.Flush();
                    }
                }
                finally { mutex.ReleaseMutex(); }

            }
        }

        public void Log(String message)
        {
            try
            {
                mutex.WaitOne();
                //Console.WriteLine($"Logger acquiring mutex: {mutex.GetHashCode()}");
                using (StreamWriter writer = new StreamWriter(this.filename, true, Encoding.ASCII)) //Streamwriter im append-Modus öffnen
                {
                    writer.WriteLine($"{message} - {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                    writer.Flush();
                }
            }
            finally
            {
                mutex.ReleaseMutex();
                //Console.WriteLine($"Logger has released mutex: {mutex.GetHashCode()}");

            }


        }
    }


}


