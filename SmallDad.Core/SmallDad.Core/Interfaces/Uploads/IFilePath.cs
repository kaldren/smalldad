using System;
using System.Collections.Generic;
using System.Text;

namespace SmallDad.Core.Interfaces.Uploads
{
    public interface IFilePath
    {
        string Path { get; }
        string PublicPath { get; }
    }
}
