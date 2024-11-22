using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatCoreLibrary.Models;

public enum FileOperation
{
    Get,
    Put
}

public class FileData
{
    public FileOperation Operation { get; set; }
    public string Id { get; set; } = string.Empty;
    public string Filename { get; set; } = string.Empty;
}

