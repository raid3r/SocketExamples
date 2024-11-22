using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatCoreLibrary.Models;

public class AuthData
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public ChatUser? User { get; set; }
    public string? ErrorMessage { get; set; }   
}
