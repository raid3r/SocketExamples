using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ChatCoreLibrary.Models;

namespace ChatCoreLibrary.FileExchage;

public class FileServer
{
    TcpListener _listener;

    public FileServer(int port = 5001)
    {
        _listener = new TcpListener(IPAddress.Any, port);
    }

    private async Task ListenAsync()
    {
        while (true)
        {
            var client = await _listener.AcceptTcpClientAsync();
            Task.Run(() => { HandleClientAsync(client); });
        }
    }

    static async Task HandleClientAsync(TcpClient client)
    {
        Console.WriteLine($"Connect");
        var fileData = await NetworkHelper.ReceiveFileDataAsync(client);
        Console.WriteLine($"Receive data: {fileData.Filename}");

        var storageDir = Path.Combine(Directory.GetCurrentDirectory(), "storage");
        if (!Directory.Exists(storageDir))
        {
            Directory.CreateDirectory(storageDir);
        }

        if (fileData.Operation == FileOperation.Put)
        {
            Console.WriteLine($"PUT {fileData.Filename}");
            var id = Guid.NewGuid().ToString() + Path.GetExtension(fileData.Filename);
            // image.png -> xxxx-xxxx-xxxx-xxxxxxxx.png
            var filename = Path.Combine(storageDir, id);
            
            await NetworkHelper.ReceiveFileAsync(client, filename);
            Console.WriteLine($"Upload file {filename}");

            await NetworkHelper.SendFileDataAsync(client, new FileData
            {
                Id = id,
                Filename = fileData.Filename
            });
            client.Close();
            Console.WriteLine($"Disconnect");
            return;
        }

        if (fileData.Operation == FileOperation.Get)
        {
            Console.WriteLine($"GET {fileData.Id}");
            var id = fileData.Id;
            var filename = Path.Combine(storageDir, id);
            await NetworkHelper.SendFileAsync(client, filename);
            Console.WriteLine($"Download file {filename}");
            client.Close();
            Console.WriteLine($"Disconnect");
            return;
        }

        // Сервер
        // - прийняти файл
        //
        //  - з'єднання

        //  - отримали json {Filename: "", Size: 12345}
        //  - відповіли json { Id: ""}
        //  - прийняли дані та записали у файл
        //  - завершення
        //
        //
        // - віддати файл
        //  - з'єднання
        //  - отримали json {Id: ""}
        //  - відповіли json {Filename: "", Size: 12345}
        //  - відправили дані з файлу
        //  - завершення


        // зберігати у локальному сховищі
    }


    public void Run()
    {
        _listener.Start();
        Console.WriteLine($"Fileserver start listening");

        Task.Run(() => ListenAsync());

        do
        {
            Thread.Sleep(1000);
        } while (true);
    }

}
