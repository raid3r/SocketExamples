using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ChatCoreLibrary.Models;

public class Client
{
    private IPAddress _address;
    private int _port;
    private Socket _socket;
    private IPEndPoint _ep;
    private bool _work = true;

    public Client(string ipAddress, int port)
    {
        _address = IPAddress.Parse(ipAddress); // Наша адреса.
        _port = port;
        _ep = new IPEndPoint(_address, _port);
    }

    public async Task ConnectAsync()
    {
        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
        await _socket.ConnectAsync(_ep);
    }

    public void Close()
    {
        _socket.Close();
    }

    public async Task SendAsync(string data)
    {
        Console.WriteLine("Send " + data);
        var bytes = Encoding.UTF8.GetBytes(data);
        await _socket.SendAsync(bytes);
    }

    public async Task<string> ReceiveAsync()
    {
        int totalReceiveCount = 0;
        int receiveCount;
        byte[] buffer = new byte[1024]; // max 1024
        StringBuilder sb = new();
        do
        {
            receiveCount = await _socket.ReceiveAsync(buffer);
            totalReceiveCount += receiveCount;
            sb.Append(Encoding.UTF8.GetString(buffer.Take(receiveCount).ToArray()));
            if (_socket.Available == 0)
            {
                break;
            }

        } while (receiveCount > 0);

        var data = sb.ToString();
        Console.WriteLine("Receive " + data);

        return data;
    }
}
