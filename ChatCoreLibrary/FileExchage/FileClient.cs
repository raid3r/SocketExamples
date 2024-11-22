using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ChatCoreLibrary.Models;

namespace ChatCoreLibrary.FileExchage;

public class FileClient
{
    int _port;
    string _ip;

    public FileClient(string ip, int port = 5001)
    {
        _ip = ip;
        _port = port;
    }

    private TcpClient Connect() => new TcpClient(_ip, _port);


    public async Task<FileData> UploadFileAsync(string filePath)
    {
        using var client = Connect();

        var filedata = new FileData
        {
            Operation = FileOperation.Put,
            Filename = Path.GetFileName(filePath),
        };

        await NetworkHelper.SendFileDataAsync(client, filedata);
        await NetworkHelper.SendFileAsync(client, filePath);
        return await NetworkHelper.ReceiveFileDataAsync(client);
    }

    public async Task DownloadFileAsync(FileData filedata, string filePath)
    {
        using var client = Connect();

        await NetworkHelper.SendFileDataAsync(client, filedata);
        await NetworkHelper.ReceiveFileAsync(client, filePath);
    }

}
