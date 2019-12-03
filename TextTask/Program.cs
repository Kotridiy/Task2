using System;
using System.Configuration;
using TextParser;
using TextParser.DOM;
using TextTask.DomainModel;

namespace TextTask
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = ConfigurationManager.AppSettings.Get("filepath-in");
            Text text;

            using (FileReader file = new FileReader(path))
            {
                Parser parser = ParserBuilder.GetParser();
                text = parser.Parse(file.Reader);
            }

            path = ConfigurationManager.AppSettings.Get("filepath-out");
            using (FileWriter file = new FileWriter(path))
            {
                text.Serialize(file.Writer);
            }

            //var words = TextManager.GetWordsInQuestions(text, 3);

            /*foreach (var word in words)
            {
                Console.WriteLine(word.GetString());
            }*/

            /*TextManager.PasteSubstring(text, StringParser.Parse("(: i love you :)"), 7, 3);
            foreach (var sentence in text)
            {
                Console.WriteLine(sentence.ToString());
            }*/

            /*foreach (var sentence in TextManager.DeleteWords(text, 3))
            {
                Console.WriteLine(sentence.ToString());
            }*/

        }
    }
}
