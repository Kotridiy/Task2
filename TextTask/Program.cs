using System;
using TextParser;
using TextParser.DOM;
using TextTask.DomainModel;

namespace TextTask
{
    class Program
    {
        static void Main(string[] args)
        {
            using (FileReader file = new FileReader(@"D:\Сейф\С#\Tasks\TextTask\TextFile.txt"))
            {
                Parser parser = new Parser();
                Text text = parser.Parse(file.Reader);
                foreach (var sentence in text)
                {
                    Console.WriteLine(sentence.ToPrint());
                }
            }
        }
    }
}
