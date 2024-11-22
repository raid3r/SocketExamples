using System.Net.Sockets;
using System.Text.Json;
using System.Text;
using System.Net;
using ChatCoreLibrary.FileExchage;


var client = new FileClient("127.0.0.1", 5001);

var localFile = @"C:\Users\kvvkv\Downloads\PHP_urok_01.pdf";
var downloadedFile = @"C:\Users\kvvkv\Downloads\PHP_urok_111111.pdf";

var filedata = await client.UploadFileAsync(localFile);

Console.WriteLine($"{filedata.Id} {filedata.Filename}");

if (File.Exists(downloadedFile))
{
    File.Delete(downloadedFile);
}

await client.DownloadFileAsync(filedata, downloadedFile);

