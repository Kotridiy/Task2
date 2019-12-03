using System;
using System.Collections.Generic;
using System.Text;
using TextParser;
using TextParser.DOM;
using TextParser.DOM.SentenceItem;

namespace TextTask.DomainModel
{
    internal class ParserBuilder
    {
        static List<char> _ends = new List<char> { '.', '!', '?' };
        static List<char> _wordSeparators = new List<char> { '\'', '-', '_' };
        public static Parser GetParser()
        {
            ParserSettings settings = new ParserSettings()
            {
                GetState = GetState,
                CreateSeparator = CreateSeparator,
            };
            return new Parser(settings);
        }

        static ParserState GetState(char symbol)
        {
            if (Char.IsLetterOrDigit(symbol))
            {
                return ParserState.Letter;
            }
            else if (Char.IsPunctuation(symbol))
            {
                if (_wordSeparators.Contains(symbol))
                {
                    return ParserState.Letter;
                }
                if (_ends.Contains(symbol))
                {
                    return ParserState.EndSentense;
                }
                else
                {
                    return ParserState.Punctuation;
                }
            }
            else if (Char.IsSeparator(symbol))
            {
                return ParserState.Separator;
            }
            else
            {
                return ParserState.Other;
            }
        }

        static ISentenceItem CreateSeparator(SymbolList symbols)
        {
            return SpaceSeparator.GetSeparator();
        }
    }
}
