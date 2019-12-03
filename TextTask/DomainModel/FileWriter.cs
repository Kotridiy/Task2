using System;
using System.IO;

namespace TextTask.DomainModel
{
    class FileWriter : IDisposable
    {
        public TextWriter Writer { get; private set; }

        public FileWriter(string path)
        {
            Writer = File.CreateText(path);
        }

        public void Dispose()
        {
            Writer.Close();
        }
    }
}
