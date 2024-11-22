using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace ChatCoreLibrary.Models;

public class ChatClient
{
    private Client _client;
    public ChatClient()
    {
        _client = new Client("127.0.0.1", 5000);
    }

    public ChatUser Me { get; set; }

    public async Task<AuthData> RegisterAsync(AuthData authData)
    {
        var transaction = new ChatTransaction
        {
            Type = TransactionTypes.Register,
            Data = JsonSerializer.Serialize(authData)
        };
        await _client.ConnectAsync();
        await _client.SendAsync(JsonSerializer.Serialize(transaction));
        var result = GetTransactionData<AuthData>(await _client.ReceiveAsync());
        _client.Close();
        return result;
    }

    public async Task<AuthData> LoginAsync(AuthData authData)
    {
        var transaction = new ChatTransaction
        {
            Type = TransactionTypes.Login,
            Data = JsonSerializer.Serialize(authData)
        };
        await _client.ConnectAsync();
        await _client.SendAsync(JsonSerializer.Serialize(transaction));
        var result = GetTransactionData<AuthData>(await _client.ReceiveAsync());
        _client.Close();
        return result;
    }

    public async Task<ChatMessage> SendMessageAsync(ChatMessage message)
    {
        message.From = Me;

        var transaction = new ChatTransaction
        {
            Type = TransactionTypes.SendMessage,
            Data = JsonSerializer.Serialize(message)
        };
        await _client.ConnectAsync();
        await _client.SendAsync(JsonSerializer.Serialize(transaction));
        var result = GetTransactionData<ChatMessage>(await _client.ReceiveAsync());
        _client.Close();
        return result;
    }

    public async Task<List<ChatMessage>> GetMessagesAsync(int fromId)
    {
        var transaction = new ChatTransaction
        {
            Type = TransactionTypes.GetMessages,
            Data = JsonSerializer.Serialize(new GetMessages
            {
                FromId = fromId,
                User = Me
            })
        };
        await _client.ConnectAsync();
        await _client.SendAsync(JsonSerializer.Serialize(transaction));

        var result = GetTransactionData<GetMessages>(await _client.ReceiveAsync())
            .Messages;

        _client.Close();

        return result;
    }

    public async Task<List<ChatUser>> GetUsersAsync()
    {
        var transaction = new ChatTransaction
        {
            Type = TransactionTypes.GetUsers,
            Data = JsonSerializer.Serialize(new GetUsers())
        };
        await _client.ConnectAsync();
        await _client.SendAsync(JsonSerializer.Serialize(transaction));

        var result = GetTransactionData<GetUsers>(await _client.ReceiveAsync())
            .Users;

        _client.Close();

        return result;
    }

    private static T GetTransactionData<T>(string json)
    {
        var response = JsonSerializer.Deserialize<ChatTransaction>(json);
        return JsonSerializer.Deserialize<T>(response.Data);
    }


}
