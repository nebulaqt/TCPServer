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
            // Create a TCP listener object with IP address set to any available IP and port 1252
            TcpListener listener = new TcpListener(IPAddress.Any, 1252);
            // Start the listener to listen for incoming connections
            listener.Start();

            // Continuously listen for incoming connections from clients
            while (true)
            {
                // Wait for 250 milliseconds to prevent the CPU from overloading
                Thread.Sleep(250);

                // Accept incoming connection from a client
                TcpClient client = listener.AcceptTcpClient();

                // Create a new thread to handle the client's connection
                Thread thread = new Thread(new ParameterizedThreadStart(HandleClient));
                thread.Start(client);
            }
        }

        static void HandleClient(object obj)
        {
            // Convert the object to TcpClient type
            TcpClient client = (TcpClient)obj;

            try
            {
                // Get the network stream from the client
                NetworkStream stream = client.GetStream();

                // Continuously read data from the stream
                while (true)
                {
                    // Wait for 250 milliseconds to prevent the CPU from overloading
                    Thread.Sleep(250);

                    // Read data from the stream into a buffer
                    byte[] buffer = new byte[1024];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);

                    // Convert the data into a string
                    string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                    // Check if the message is not empty
                    if (!string.IsNullOrEmpty(message))
                    {
                        // Write the message to the console
                        Console.WriteLine("Received message: " + message);
                    }

                    // Check if the message is "keep alive"
                    if (message.Trim() == "keep alive")
                    {
                        // Send the "ack" response back to the client
                        byte[] response = Encoding.ASCII.GetBytes("ack");
                        stream.Write(response, 0, response.Length);
                    }
                    else
                    {
                        // Handle other messages as needed
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while handling client: " + ex.Message);
            }
            finally
            {
                // Close the client connection
                client.Close();
            }
        }
    }
}