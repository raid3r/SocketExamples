

// Sender

using System.Net;
using System.Net.Sockets;
using System.Text;

//Multicast
var port = 5005;
var client = new UdpClient();
var multicastAddress = IPAddress.Parse("224.10.10.10");
var ep = new IPEndPoint(multicastAddress, port);

while (true)
{
    var message = DateTime.Now.ToString("G");
    var bytes = Encoding.UTF8.GetBytes(message);
    client.Send(bytes, bytes.Length, ep);
    Thread.Sleep(1000);
}

//Broadcast
//var client = new UdpClient();
//client.EnableBroadcast = true;

//var port = 5005;
//var ep = new IPEndPoint(IPAddress.Broadcast, port);

//while (true)
//{
//    var message = DateTime.Now.ToString("G");
//    var bytes = Encoding.UTF8.GetBytes(message);
//    client.Send(bytes, bytes.Length, ep);
//    Thread.Sleep(50);
//}




