using System;
using System.Collections.Generic;
using TextParser;
using TextParser.DOM;
using TextParser.DOM.SentenceItem;

namespace TextTask.DomainModel
{
    static class StringParser
    {
        public static IEnumerable<ISentenceItem> Parse(string str)
        {
            var items = new List<ISentenceItem>();
            var symbols = new SymbolList();
            ParserState state = ParserState.None;
            ParserState newState = ParserState.None;
            char symbol;

            for (int i = 0; i < str.Length; i++)
            {
                symbol = str[i];
                newState = GetState(symbol);
                if (state != newState && state != ParserState.None)
                {
                    items.Add(CreateSentenseItem(symbols.PopAll(), state));
                }

                state = newState;
                symbols.Add(new Symbol(symbol));
            }

            items.Add(CreateSentenseItem(symbols.PopAll(), newState));

            return items;
        }

        private static ParserState GetState(char symbol)
        {
            if (Char.IsLetterOrDigit(symbol))
            {
                return ParserState.Letter;
            }
            else if (Char.IsPunctuation(symbol))
            {
                return ParserState.Punctuation;
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

        static private ISentenceItem CreateSentenseItem(SymbolList symbols, ParserState state)
        {
            if (symbols == null || symbols.Count == 0)
            {
                throw new ArgumentNullException(nameof(symbols));
            }

            switch (state)
            {
                case ParserState.Letter:
                    return new Word(symbols);
                case ParserState.Separator:
                    return SpaceSeparator.GetSeparator();
                case ParserState.Punctuation:
                case ParserState.Other:
                    return new Punctuation(symbols.ToString());
                case ParserState.EndSentense:
                case ParserState.None:
                default:
                    throw new AggregateException($"State can't be \"{state}\"");
            }
        }
    }
}
