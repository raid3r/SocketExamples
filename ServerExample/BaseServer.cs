using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ServerExample;

public abstract class BaseServer
{
    private IPAddress _address;
    private int _port;
    private Socket _socket;
    private IPEndPoint _ep;
    private bool _work = true;

    public BaseServer(string ipAddress, int port) {
        _address = IPAddress.Parse(ipAddress); // Наша адреса.
        _port = port;
        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
        _ep = new IPEndPoint(_address, _port);
    }

    private async Task ListenAsync()
    {
        while (_work) {
            var socket = await _socket.AcceptAsync();
            Task.Run(() => { HandleClientAsync(socket); });
        }
    }

    public abstract Task HandleClientAsync(Socket socket);

    public static async Task SendAsync(Socket socket, string data)
    {
        Console.WriteLine("Send " + data);
        var bytes = Encoding.UTF8.GetBytes(data);
        await socket.SendAsync(bytes);
    }

    public static async Task<string> ReceiveAsync(Socket socket)
    {
        int totalReceiveCount = 0;
        int receiveCount;
        byte[] buffer = new byte[1024]; // max 1024
        StringBuilder sb = new();
        do
        {
            receiveCount = await socket.ReceiveAsync(buffer);
            totalReceiveCount += receiveCount;
            sb.Append(Encoding.UTF8.GetString(buffer.Take(receiveCount).ToArray()));
            if (socket.Available == 0)
            {
                break;
            }

        } while (receiveCount > 0);
        var data = sb.ToString();
        Console.WriteLine("Receive " + data);
        return data;
    }


    public void Run()
    {
        _socket.Bind(_ep);
        _socket.Listen(10);

        Console.WriteLine("Start listening");

        Task.Run(() => ListenAsync());

        do
        {
            Thread.Sleep(1000);
        } while (_work);
       
    }
}
