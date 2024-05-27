using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SimpleHttpServer {

    public class SimpleHttpServer {

        public static void Main(string[] args) {
            string filename;
            if (args.Length != 0) {
                filename = args[0];
            }
            else {
                Console.WriteLine("Programmargument File fehlt...");
                return;
            }
            TcpListener listen = new TcpListener(IPAddress.Any, 8080);
            listen.Start();
            bool busy = true;
            while (busy) {
                Console.WriteLine("Warte auf Verbindung auf Port {0}...",
                    listen.LocalEndpoint);
                try {
                    new Thread(new HttpHandler(listen.AcceptTcpClient(), filename).Do).Start();
                }
                catch (Exception) {
                    busy = false;
                }
            }
            Console.WriteLine("SimpleHttpServer fertig...");
        }
    }
}
