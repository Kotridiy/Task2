using System;
using TextParser.DOM;
using TextParser.DOM.SentenceItem;

namespace TextParser
{
    public class ParserSettings
    {
        public Func<char, ParserState> GetState { get; set; }
        public Func<SymbolList, ISentenceItem> CreateWord { get; set; }
        public Func<SymbolList, ISentenceItem> CreatePunctuation { get; set; }
        public Func<SymbolList, ISentenceItem> CreateSeparator { get; set; }
    }
}
