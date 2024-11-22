﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatCoreLibrary.Models;

public class ChatUser
{
    public int Id { get; set; }
    public string Token { get; set; }
    public string Login { get; set; }

    public override string ToString()
    {
        return Login;
    }
}
