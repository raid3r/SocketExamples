using ChatCoreLibrary.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServerExample;

public class ChatServer
{
    private ChatDataStorage _storage = new();

    private Server _server { get; set; }
    public ChatServer(int port = 5000)
    {
        _server = new Server("127.0.0.1", port);
    }

    public void Run()
    {
        _server.ClientHandler = HandleClientAsync;
        _server.Run();
    }

    public static T GetTransactionData<T>(string json)
    {
        //var response = JsonSerializer.Deserialize<ChatTransaction>(json);
        return JsonSerializer.Deserialize<T>(json);
    }

    //private

    private async Task<ChatTransaction> HandleSendMessage(ChatTransaction transaction)
    {
        var message = GetTransactionData<ChatMessage>(transaction.Data);
        var fromUser = _storage.Users.FirstOrDefault(x => x.Token == message.From.Token);
        if (fromUser == null)
        {
            // TODO Error - Forbidden !
        }

        message.DateTime = DateTime.Now;
        if (_storage.Messages.Count == 0)
        {
            message.Id = 1;
        }
        else
        {
            message.Id = _storage.Messages.Max(x => x.Id) + 1;
        }

        _storage.Messages.Add(message);
        await _storage.SaveChangesAsync();

        return new ChatTransaction
        {
            Type = transaction.Type,
            Data = JsonSerializer.Serialize(message)
        };
    }


    public static string GenerateToken(string password, string salt)
    {
        using (var sha256 = SHA256.Create())
        {
            // Преобразование пароля и соли в байты
            var combinedBytes = Encoding.UTF8.GetBytes(password + salt);

            // Вычисление хеша
            var hashBytes = sha256.ComputeHash(combinedBytes);

            // Преобразование хеша в строку Base64
            var token = Convert.ToBase64String(hashBytes);

            return token;
        }
    }

    private async Task<ChatTransaction> HandleRegistration(ChatTransaction transaction)
    {
        var register = GetTransactionData<AuthData>(transaction.Data);

        if (_storage.Users.Exists(x => x.Login == register.Login))
        {
            return new ChatTransaction
            {
                Type = transaction.Type,
                Data = JsonSerializer.Serialize(
                 new AuthData
                 {
                     User = null,
                     ErrorMessage = "User already exists"
                 }
             )
            };
        }
        var user = new ChatUser();

        if (_storage.Users.Count == 0)
        {
            user.Id = 1;
        }
        else
        {
            user.Id = _storage.Users.Max(x => x.Id) + 1;
        }
        user.Login = register.Login;
        user.Token = GenerateToken(register.Password, register.Login);

        _storage.Users.Add(user);
        await _storage.SaveChangesAsync();


        return new ChatTransaction
        {
            Type = transaction.Type,
            Data = JsonSerializer.Serialize(
             new AuthData
             {
                 User = user,
                 ErrorMessage = string.Empty
             }
         )
        };


    }

    private async Task<ChatTransaction> HandleLogin(ChatTransaction transaction)
    {
        var auth = GetTransactionData<AuthData>(transaction.Data);

        var user = _storage.Users.FirstOrDefault(x => x.Login == auth.Login);

        if (user == null)
        {
            return new ChatTransaction
            {
                Type = transaction.Type,
                Data = JsonSerializer.Serialize(
                 new AuthData
                 {
                     User = null,
                     ErrorMessage = "User not found"
                 }
             )
            };
        }

        var token = GenerateToken(auth.Password, auth.Login);
        if (token != user.Token)
        {
            return new ChatTransaction
            {
                Type = transaction.Type,
                Data = JsonSerializer.Serialize(
                 new AuthData
                 {
                     User = null,
                     ErrorMessage = "Wrong password"
                 }
             )
            };
        }

        return new ChatTransaction
        {
            Type = transaction.Type,
            Data = JsonSerializer.Serialize(
             new AuthData
             {
                 User = user,
                 ErrorMessage = string.Empty
             }
         )
        };
    }

    private async Task<ChatTransaction> HandleGetMessages(ChatTransaction transaction)
    {
        var message = GetTransactionData<GetMessages>(transaction.Data);

        var fromUser = _storage.Users.FirstOrDefault(x => x.Token == message.User.Token);
        if (fromUser == null)
        {
            // TODO Error - Forbidden !
        }


        return new ChatTransaction
        {
            Type = transaction.Type,
            Data = JsonSerializer.Serialize(
                new GetMessages
                {
                    FromId = message.FromId,
                    Messages = _storage
                    .Messages
                    .Where(x => x.Id > message.FromId &&
                    (x.From.Id == fromUser.Id || x.To.Id == fromUser.Id))
                    .ToList()
                }
            )
        };
    }

    private async Task<ChatTransaction> HandleGetUsers(ChatTransaction transaction)
    {
        var message = GetTransactionData<GetUsers>(transaction.Data);

        return new ChatTransaction
        {
            Type = transaction.Type,
            Data = JsonSerializer.Serialize(
                new GetUsers
                {
                    Users = _storage
                    .Users
                    .Select(x => new ChatUser
                    {
                        Id = x.Id,
                        Login = x.Login,
                        Token = string.Empty
                    })
                    .ToList()
                }
            )
        };
    }

    private async Task HandleClientAsync(Socket socket)
    {
        Console.WriteLine("Client connected");
        Console.WriteLine(socket.RemoteEndPoint);

        var json = await BaseServer.ReceiveAsync(socket);
        try
        {
            var transaction = JsonSerializer.Deserialize<ChatTransaction>(json);



            Console.WriteLine(transaction.Data);
            ChatTransaction response = transaction.Type switch
            {
                TransactionTypes.SendMessage => await HandleSendMessage(transaction),
                TransactionTypes.GetMessages => await HandleGetMessages(transaction),
                TransactionTypes.Register => await HandleRegistration(transaction),
                TransactionTypes.Login => await HandleLogin(transaction),
                TransactionTypes.GetUsers => await HandleGetUsers(transaction),
                _ => throw new Exception("Unknown transaction type")
            };

            await BaseServer.SendAsync(socket, JsonSerializer.Serialize(response));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        socket.Close();
    }


}
