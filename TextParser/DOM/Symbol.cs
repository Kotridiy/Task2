using System;
using TextParser.DOM.SentenceItem;

namespace TextParser.DOM
{
    public class Symbol : ISentenceItem
    {
        public char Char { get; }

        public Symbol(char symbol)
        {
            Char = symbol;
        }

        public string GetString()
        {
            throw new NotImplementedException();
        }
    }
}
