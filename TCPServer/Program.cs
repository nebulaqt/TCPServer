using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TCPServer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a TCP listener object and start it
            TcpListener listener = new TcpListener(IPAddress.Any, 1252);
            listener.Start();

            // Continuously listen for incoming connections
            while (true)
            {
                // Wait for a bit to prevent the CPU from going crazy
                Thread.Sleep(250);
                // Accept a new client connection
                TcpClient client = listener.AcceptTcpClient();

                // Create a new thread to handle the connection
                Thread thread = new Thread(new ParameterizedThreadStart(HandleClient));
                thread.Start(client);
            }
        }

        static void HandleClient(object obj)
        {
            // Get the client object from the parameter
            TcpClient client = (TcpClient)obj;

            // Get the client's network stream
            NetworkStream stream = client.GetStream();

            // Continuously read from the stream
            while (true)
            {
                // Wait for a bit to prevent the CPU from going crazy
                Thread.Sleep(250);
                
                // Read data from the stream
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                // Log the message
                Console.WriteLine("Received message: " + message);

                // Check if the message is the "keep alive" message
                if (message.Trim() == "keep alive")
                {
                    // Send a response back to the client
                    byte[] response = Encoding.ASCII.GetBytes("ack");
                    stream.Write(response, 0, response.Length);
                }
                else
                {
                    // Handle other messages as needed
                }
            }
        }
    }
}
