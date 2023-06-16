using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TCPServer
{
    public partial class TcpServerFrm : Form
    {
        private Thread _acceptClientsThread; // Thread for accepting client connections
        private CancellationTokenSource _cancellationTokenSource; // Source for canceling asynchronous operations
        private bool _isServerRunning; // Flag to track the server running state
        private TcpListener _listener; // TCP listener for incoming connections

        public TcpServerFrm()
        {
            InitializeComponent();
        }

        private void HandleClient(TcpClient client)
        {
            try
            {
                // Enable TCP keep-alive on the client socket
                client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);

                var stream = client.GetStream(); // Get the network stream for reading and writing data
                var buffer = new byte[1024]; // Buffer to hold the received data

                while (!_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    if (stream.DataAvailable) // Check if there is data available to read from the stream
                    {
                        var bytesRead = stream.Read(buffer, 0, buffer.Length); // Read data from the stream into the buffer
                        var message = Encoding.ASCII.GetString(buffer, 0, bytesRead); // Convert the received bytes to a string
                        var clientEndPoint = client.Client.RemoteEndPoint as IPEndPoint; // Get the client's IP endpoint

                        if (!string.IsNullOrEmpty(message)) // Check if the received message is not empty
                        {
                            var logMessage = $"Received message from {clientEndPoint}: {message}\n"; // Create a log message
                            AppendToLog(logMessage); // Append the log message to the logRichTextBox
                        }

                        if (message.Trim() == "keep alive") // Check if the received message is "keep alive"
                        {
                            var response = Encoding.ASCII.GetBytes("ack"); // Convert the response string to bytes
                            stream.Write(response, 0, response.Length); // Write the response to the stream
                        }
                    }

                    // Introduce a short delay to reduce CPU usage
                    Thread.Sleep(10);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"Error occurred while handling client: " + ex.Message);
            }
            finally
            {
                client.Close(); // Close the client connection
            }
        }

        private void AcceptClients()
        {
            while (_isServerRunning)
            {
                try
                {
                    var client = _listener.AcceptTcpClient(); // Accept an incoming TCP client connection
                    ThreadPool.QueueUserWorkItem(state => HandleClient(client)); // Queue the client for processing on a thread pool thread
                }
                catch (ObjectDisposedException)
                {
                    // Handle ObjectDisposedException if the server is stopped
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(@"Error occurred while accepting client: " + ex.Message);
                }
            }
        }

        private void AppendToLog(string logMessage)
        {
            if (logRtb.InvokeRequired) // Check if invoking is required to access the UI control from a different thread
            {
                logRtb.BeginInvoke((MethodInvoker)(() =>
                {
                    logRtb.AppendText(logMessage); // Append the log message to the logRichTextBox
                    logRtb.ScrollToCaret(); // Scroll to the end of the logRichTextBox
                }));
            }
            else
            {
                logRtb.AppendText(logMessage); // Append the log message to the logRichTextBox
                logRtb.ScrollToCaret(); // Scroll to the end of the logRichTextBox
            }
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            if (_isServerRunning)
                return;

            string input = portTb.Text; // PortTb is the TextBox containing the numeric value
            int port = Convert.ToInt32(input);

            _listener = new TcpListener(IPAddress.Any, port); // Create a TCP listener on all available network interfaces and port 1252
            _listener.Start(); // Start listening for incoming connections
            _isServerRunning = true; // Set the server running flag to true
            startBtn.Enabled = false; // Disable the Start button
            stopBtn.Enabled = true; // Enable the Stop button

            _cancellationTokenSource = new CancellationTokenSource(); // Create a new cancellation token source
            // Update label with IP address and port
            statusLb.Text = @"Status: Running " + _listener.LocalEndpoint; // Set the status label text to the server's local endpoint

            _acceptClientsThread = new Thread(AcceptClients); // Create a new thread for accepting clients
            _acceptClientsThread.Start(); // Start the thread for accepting clients
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            if (!_isServerRunning)
                return;

            // Update label with IP address and port
            statusLb.Text = @"Status: Stopped"; // Set the status label text to "Stopped"
            _listener.Stop(); // Stop the TCP listener
            _isServerRunning = false; // Set the server running flag to false
            _cancellationTokenSource?.Cancel(); // Cancel any pending asynchronous operations
            _cancellationTokenSource?.Dispose(); // Dispose the cancellation token source
            _cancellationTokenSource = null; // Set the cancellation token source to null
            stopBtn.Enabled = false; // Disable the Stop button
            startBtn.Enabled = true; // Enable the Start button
        }

        private void portTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the pressed key is a number or a control key (backspace, delete, etc.)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Cancel the key press event
                e.Handled = true;
            }
        }
    }
}
