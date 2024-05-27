using System;
using System.Net.Sockets;
using System.IO;

namespace SimpleHttpServer {

    public class HttpHandler {

        private readonly StreamReader sr;
        private readonly StreamWriter sw;
        private readonly TcpClient client;
        private readonly string filename;

        public HttpHandler(TcpClient client, String filename) {
            this.client = client;
            this.filename = filename;
            this.sr = new StreamReader(this.client.GetStream());
            this.sw = new StreamWriter(this.client.GetStream());
        }
        public void Do() {
            try {
                Console.WriteLine("Verbindung zu " + client.Client.RemoteEndPoint);
                string request = sr.ReadLine();
                Console.WriteLine("Request: " + request);
                if (request != null && request.Contains("GET")) {
                    while (true) {
                        // Test des MIME header
                        string line = sr.ReadLine();
                        //Console.WriteLine(thisLine);
                        if (line.Trim() == "")
                            break;
                    }
                    string theData;
                    using (StreamReader file = new StreamReader(this.filename)) {
                        theData = file.ReadToEnd();
                    }
                    sw.WriteLine("HTTP/1.0 200 OK");
                    sw.WriteLine("Date: " + DateTime.Now.ToString());
                    sw.WriteLine("Server: TestFileServer 1.0");
                    sw.WriteLine("Content-length: " + theData.Length);
                    sw.WriteLine("Content-type: text/plain");
                    sw.WriteLine(); // Leerzeile senden
                    sw.WriteLine(theData);
                    sw.Flush();
                    Console.WriteLine("File gesendet");
                }
            }
            catch (IOException e) {
                Console.WriteLine(e.Message);
            }
            finally {
                client.Close();
            }
        }
    }
}