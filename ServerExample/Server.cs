using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServerExample;



public class Server: BaseServer
{
    public delegate Task ClientHandlerDelegate(Socket socket);

    public ClientHandlerDelegate ClientHandler;

    public Server(string ipAddress, int port): base(ipAddress,port) {}
    

    public override async Task HandleClientAsync(Socket socket)
    {
       await ClientHandler(socket);
    }


}
