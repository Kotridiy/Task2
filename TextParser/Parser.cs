using System;
using System.Collections.Generic;
using System.IO;
using TextParser.DOM;
using TextParser.DOM.SentenceItem;
using TextParser.DOM.TextItem;

namespace TextParser
{
    public class Parser
    {
        public Func<char, ParserState> GetState { get; set; }
        public Func<SymbolList, ISentenceItem> CreateWord { get; set; }
        public Func<SymbolList, ISentenceItem> CreatePunctuation { get; set; }
        public Func<SymbolList, ISentenceItem> CreateSeparator { get; set; }
        
        List<char> ends = new List<char>{'.', '!', '?'};
        HashSet<Word> Words { get; set; } 

        public Parser() : this(new ParserSettings())
        {
        }

        public Parser(ParserSettings settings)
        {
            Words = new HashSet<Word>();
            GetState = settings.GetState ?? BaseGetState;
            CreateWord = settings.CreateWord ?? BaseCreateWord;
            CreatePunctuation = settings.CreateSeparator ?? BaseCreatePunctuation;
            CreateSeparator = settings.CreateSeparator ?? BaseCreateSeparator;
        }

        public Text Parse(TextReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentException(nameof(reader));
            }
            var sentences = new List<Sentence>();
            var items = new List<ISentenceItem>();
            string str;
            var symbols = new SymbolList();
            ParserState state = ParserState.None;
            ParserState newState = ParserState.None;
            char symbol;

            while(reader.Peek() != -1)
            {
                str = reader.ReadLine();
                for (int i = 0; i < str.Length; i++)
                {
                    symbol = str[i];
                    newState = GetState(symbol);
                    if (state != newState && state != ParserState.None)
                    {
                        items.Add(CreateSentenseItem(symbols.PopAll(), state));

                        if (state == ParserState.EndSentense)
                        {
                            sentences.Add(CreateSectence(items));
                            items = new List<ISentenceItem>();
                        }
                    }


                    state = newState;
                    symbols.Add(new Symbol(symbol));
                }

                if(symbols.Count > 0)
                {
                    items.Add(CreateSentenseItem(symbols.PopAll(), newState));
                    if (newState == ParserState.EndSentense)
                    {
                        sentences.Add(CreateSectence(items));
                        items = new List<ISentenceItem>();
                        state = ParserState.None;
                    }

                }
            }

            return new Text(sentences.ToArray());
        }

        private Sentence CreateSectence(List<ISentenceItem> items)
        {
            if (items == null || items.Count == 0)
            {
                throw new ArgumentNullException(nameof(items));
            }

            Punctuation endPunctuation = items[items.Count - 1] as Punctuation;
            if (endPunctuation == null)
            {
                throw new ArgumentException("Last sentence item must be punctuation.");
            }

            items.Remove(endPunctuation);
            return new Sentence(items, endPunctuation);
        }

        private ISentenceItem CreateSentenseItem(SymbolList symbols, ParserState state)
        {
            if(symbols == null || symbols.Count == 0)
            {
                throw new ArgumentNullException(nameof(symbols));
            }

            switch (state)
            {
                case ParserState.Letter:
                    return CreateWord(symbols);
                case ParserState.Separator:
                    return CreateSeparator(symbols);
                case ParserState.EndSentense:
                case ParserState.Punctuation:
                case ParserState.Other:
                    return CreatePunctuation(symbols);
                case ParserState.None:
                default:
                    throw new AggregateException($"State can't be \"{state}\"");
            }
        }

        private ParserState BaseGetState(char symbol)
        {
            if (Char.IsLetterOrDigit(symbol))
            {
                return ParserState.Letter;
            }
            else if (Char.IsPunctuation(symbol))
            {
                if (ends.Contains(symbol))
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

        private ISentenceItem BaseCreateWord(SymbolList symbols)
        {
            Word word = new Word(symbols);
            if (Words.Contains(word))
            {
                Words.TryGetValue(word, out Word newWord);
                return newWord ?? word;
            }
            else
            {
                Words.Add(word);
                return word;
            }
        }

        private ISentenceItem BaseCreatePunctuation(SymbolList symbols)
        {
            return new Punctuation(symbols.ToString());
        }

        private ISentenceItem BaseCreateSeparator(SymbolList symbols)
        {
            return new Separator(symbols.ToString());
        }
    }
}
