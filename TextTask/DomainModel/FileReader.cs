using System;
using System.IO;

namespace TextTask.DomainModel
{
    class FileReader : IDisposable
    {
        public TextReader Reader { get; private set; } 

        public FileReader(string path)
        {
            Reader = File.OpenText(path);
        }

        public void Dispose()
        {
            Reader.Close();
        }
    }
}