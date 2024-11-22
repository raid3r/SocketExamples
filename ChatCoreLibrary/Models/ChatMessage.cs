using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatCoreLibrary.Models;

public class ChatMessage
{
    public int Id { get; set; }
    public ChatUser From { get; set; }
    public ChatUser To { get; set; }
    public DateTime DateTime { get; set; }
    public string Text { get; set; }
    public FileData? FileData { get; set; }

    public string FromLogin { get => From?.Login ?? string.Empty; }
    public string ToLogin { get => To?.Login ?? string.Empty; }
}
