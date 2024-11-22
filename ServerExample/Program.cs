// See https://aka.ms/new-console-template for more information
using ChatCoreLibrary.FileExchage;
using ServerExample;
using System.Net;
using System.Net.Sockets;
using System.Text;

//File server
var fileServer = new FileServer(5001);
Task.Run(() => fileServer.Run());

// Сервер
var chatServer = new ChatServer();
chatServer.Run();




//static void AcceptCallback(IAsyncResult result)
//{
//    Socket serverSocket = (Socket)result.AsyncState;
//    Socket clientSocket = serverSocket.EndAccept(result);

//    serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), serverSocket);

//    Console.WriteLine("Client connected");
//    int totalReceiveCount = 0;
//    int receiveCount;
//    byte[] buffer = new byte[1024]; // max 1024
//    StringBuilder sb = new();
//    do
//    {
//        receiveCount = clientSocket.Receive(buffer);
//        totalReceiveCount += receiveCount;
//        sb.Append(Encoding.UTF8.GetString(buffer));
//        if (clientSocket.Available == 0)
//        {
//            break;
//        }

//    } while (receiveCount > 0);
//    // receive

//    Console.WriteLine($"Receive {totalReceiveCount} bytes");
//    var message = sb.ToString();
//    Console.WriteLine("Message: " + message);

//    //await Task.Delay(10000);
//    Thread.Sleep(1000);

//    Console.WriteLine($"Send response");
//    var sendCount = clientSocket.Send(Encoding.UTF8.GetBytes("Ok:" + totalReceiveCount.ToString()));
//    Console.WriteLine($"Disconnect");

//    // if "exit" break
//    clientSocket.Close();


//}

//var ipAdress = IPAddress.Parse("127.0.0.1"); // Наша адреса.
////var ipAdress = IPAddress.Parse("192.168.1.27");
//var port = 5000;

//Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

//IPEndPoint endPoint = new IPEndPoint(ipAdress, port);

//Console.WriteLine("Server");

//try
//{
//    serverSocket.Bind(endPoint);
//    serverSocket.Listen(10);


//    // + 1. Розмір повідомлення обмежений розміром буферу
//    // + 2. Після спілкування з клієнтом сервер виключається
//    // + 3. Обслуговує клієнтів послідовно інші чекають
//    // 4. Обмін даними проходить лише послідовно
//    // 
//    Console.WriteLine("Wait client....");


//    //Socket clientSocket = await serverSocket.AcceptAsync();
//    serverSocket.BeginAccept(AcceptCallback, serverSocket);
//    Console.ReadLine();
//}
//catch (Exception e)
//{
//    Console.WriteLine(e.Message);
//}


//// 0-1000 // 21 FTP 25 SMTP 80 - HTTP 443 - HTTPS
//// 1000-20000 - 
//// 20000 - 65000

///*
// * Порт 0-65555
// * 
// * soft 1       - server - [Socket] <=> 127.0.0.1:5000 <=>         ->[кому-від кого+дані+порт+порт][]
// * soft 2       - client 6000 -> 8.8.8.8 <--  ->[][][]          NETWORK WIFI ))) ==== ((( R
// * soft 3       - client 6001 -> ukr.net <--  ->[][]                    фізичний
// *                                                                  канал
// * 
// * 
// *  
// */










