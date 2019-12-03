using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TextParser.DOM;

namespace TextTask.DomainModel
{
    public static class TextSerializeExtension
    {
        public static void Serialize(this Text text, TextWriter writer)
        {
            for (int i = 0; i < text.Length - 1; i++)
            {
                writer.Write(text[i].ToString() + ' ');
            }
            writer.Write(text[text.Length - 1].ToString());
        }
    }
}
