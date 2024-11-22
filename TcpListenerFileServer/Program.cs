// See https://aka.ms/new-console-template for more information
using ChatCoreLibrary.FileExchage;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

var fileServer = new FileServer(5001);
fileServer.Run();
