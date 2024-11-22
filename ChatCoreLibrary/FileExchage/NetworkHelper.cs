using ChatCoreLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChatCoreLibrary.FileExchage;

public static class NetworkHelper
{
    public static async Task SendFileDataAsync(TcpClient client, FileData data)
    {
        string json = JsonSerializer.Serialize(data);
        var jsonBytes = Encoding.UTF8.GetBytes(json);
        var sizeBytes = BitConverter.GetBytes(jsonBytes.Length);

        NetworkStream ns = client.GetStream();
        await ns.WriteAsync(sizeBytes, 0, sizeBytes.Length);
        await ns.WriteAsync(jsonBytes, 0, jsonBytes.Length);
    }

    public static async Task<FileData> ReceiveFileDataAsync(TcpClient client)
    {
        NetworkStream ns = client.GetStream();
        var sizeBytes = new byte[sizeof(int)];
        await ns.ReadAsync(sizeBytes, 0, sizeBytes.Length);
        int size = BitConverter.ToInt32(sizeBytes, 0);
        var jsonBytes = new byte[size];
        int read = 0;
        while (read < size)
        {
            read += await ns.ReadAsync(jsonBytes, read, size - read);
        }
        string json = Encoding.UTF8.GetString(jsonBytes);
        return JsonSerializer.Deserialize<FileData>(json);
    }

    public static async Task SendFileAsync(TcpClient client, string path)
    {
        var fileSize = (int)new FileInfo(path).Length;
        var sizeBytes = BitConverter.GetBytes(fileSize);

        using FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        NetworkStream ns = client.GetStream();

        await ns.WriteAsync(sizeBytes, 0, sizeBytes.Length);
        await fs.CopyToAsync(ns);
    }

    public static async Task ReceiveFileAsync(TcpClient client, string path)
    {
        NetworkStream ns = client.GetStream();
        using FileStream fs = new FileStream(path, FileMode.CreateNew, FileAccess.Write);

        var sizeBytes = new byte[sizeof(int)];
        await ns.ReadAsync(sizeBytes, 0, sizeBytes.Length);
        int size = BitConverter.ToInt32(sizeBytes, 0);

        byte[] buffer = new byte[65536];

        int totalRead = 0;
        int read = 0;
        do
        {
            read = await ns.ReadAsync(buffer, 0, buffer.Length);
            totalRead += read;
            if (read == 0) break;
            await fs.WriteAsync(buffer, 0, read);
        } while (totalRead < size);
    }
}
