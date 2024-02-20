# TCP Server

This application implements a TCP server using Windows Forms, which listens for incoming client connections, handles messages, and sends acknowledgements. It provides basic functionality to start and stop the server, display logs, and handle client communication.

## Features

- Accepts incoming TCP client connections.
- Handles messages from clients and sends acknowledgements.
- Provides UI for starting and stopping the server.
- Displays logs of incoming messages and server status.

## Usage

1. Launch the application.
2. Enter the port number in the provided text box.
3. Click the "Start" button to start the server.
4. The server will start listening for incoming connections.
5. Once a client connects, the server will handle messages and display logs.
6. Click the "Stop" button to stop the server.

## Components

### `TcpServerFrm`

- Main form class responsible for handling UI events and server lifecycle.
- Implements methods for handling client connections and messages.
- Utilizes Windows Forms controls for UI elements such as buttons and text boxes.

### `HandleClient`

- Method for handling individual client connections.
- Reads messages from clients, sends acknowledgements, and logs communication.
- Runs on a separate thread to handle multiple clients concurrently.

### `AcceptClients`

- Method for accepting incoming client connections.
- Runs on a continuous loop while the server is running.
- Queues clients for processing on separate threads using the thread pool.

### `AppendToLog`

- Method for appending log messages to the UI log display.
- Handles thread synchronization to update the UI from background threads.

## Configuration

- **Port Number:** Specify the port number on which the server will listen for incoming connections.

## Error Handling

- If an error occurs while handling a client connection, the server logs the error message.
- Exceptions are caught to prevent crashing and maintain server stability.

## Dependencies

- .NET Framework
- Windows Forms

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
