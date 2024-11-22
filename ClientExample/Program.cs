using System.Net.Sockets;
using System.Net;
using System.Text;
using ChatCoreLibrary.Models;


var chatClient = new ChatClient();

_ = await chatClient.SendMessageAsync(new ChatMessage
{
    UserName = "Test User",
    Text = "Hello world 1"
});

_ = await chatClient.SendMessageAsync(new ChatMessage
{
    UserName = "Test User",
    Text = "Hello world 2"
});

_ = await chatClient.SendMessageAsync(new ChatMessage
{
    UserName = "Test User",
    Text = "Hello world 3"
});

var messages = await chatClient.GetMessagesAsync(1);

foreach (var message in messages)
{
    Console.WriteLine($"{message.Id} : {message.DateTime.ToString("G")} : {message.Text}");
}

//Console.WriteLine($"{message.Id} : {message.DateTime.ToString("G")} : {message.Text}" );

Console.ReadLine();

// Клієнт
//Console.WriteLine("Client");

//var ipAdress = IPAddress.Parse("127.0.0.1"); // Адреса куди будимо підключатися.
////var ipAdress = IPAddress.Parse("192.168.1.27");
//var port = 5000;

//Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
//IPEndPoint endPoint = new IPEndPoint(ipAdress, port);

//socket.Connect(endPoint);

//if (socket.Connected)
//{
//    Console.WriteLine("Connected");

//    var message = "Test Test Test Test Test Test"; //Console.ReadLine();
//    byte[] buffer = Encoding.UTF8.GetBytes(message);
//    var sendCount = socket.Send(buffer);
//    Console.WriteLine($"Send {sendCount} bytes");

//    //read
//    int totalReceiveCount = 0;
//    int receiveCount;
//    byte[] readNuffer = new byte[10]; // max 1024
//    StringBuilder sb = new();
//    do
//    {
//        receiveCount = socket.Receive(buffer);
//        totalReceiveCount += receiveCount;
//        sb.Append(Encoding.UTF8.GetString(buffer));
//        if (socket.Available == 0)
//        {
//            break;
//        }
//    } while (receiveCount > 0);

//    Console.WriteLine(sb.ToString());
//}

//socket.Close();
//Console.ReadLine();
