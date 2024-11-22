using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatCoreLibrary.Models;

public enum TransactionTypes
{
    GetMessages,
    SendMessage,
    Register,
    Login,
    GetUsers
}

public class ChatTransaction
{
    public TransactionTypes Type { get; set; }
    public string Data { get; set; }
}
