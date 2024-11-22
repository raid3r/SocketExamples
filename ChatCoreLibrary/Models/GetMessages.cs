using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatCoreLibrary.Models;

public class GetMessages
{
    public int FromId { get; set; }

    public ChatUser User { get; set; }

    public List<ChatMessage> Messages { get; set; } = [];
}
