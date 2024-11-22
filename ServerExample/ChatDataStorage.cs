using ChatCoreLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServerExample;

public class ChatDataStorage
{
    private readonly string usersFile = "users.json";
    private readonly string messagesFile = "messages.json";

    public ChatDataStorage()
    {
        if (File.Exists(usersFile))
        {
            Users = JsonSerializer.Deserialize<List<ChatUser>>(File.ReadAllText(usersFile));
        }
        if (File.Exists(messagesFile))
        {
            Messages = JsonSerializer.Deserialize<List<ChatMessage>>(File.ReadAllText(messagesFile));
        }
    }

    public async Task SaveChangesAsync()
    {
        using var userFileStream = new FileStream(usersFile, FileMode.Create, FileAccess.Write, FileShare.None);
        await JsonSerializer.SerializeAsync(userFileStream, Users);

        using var messagesFileStream = new FileStream(messagesFile, FileMode.Create, FileAccess.Write, FileShare.None);
        await JsonSerializer.SerializeAsync(messagesFileStream, Messages);
    }

    public List<ChatMessage> Messages { get; set; } = new();
    public List<ChatUser> Users { get; set; } = new();
}
