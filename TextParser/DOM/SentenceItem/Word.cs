using System;
using System.Collections.Generic;

namespace TextParser.DOM.SentenceItem
{
    public class Word : ISentenceItem
    {
        public List<Symbol> Letters { get; }
        private int hashCode;

        public string GetString() => Letters.ToString();

        public Word(SymbolList letters)
        {
            if (letters == null && letters.Count > 0)
            {
                throw new ArgumentNullException(nameof(letters));
            }
            Letters = letters;
            hashCode = Letters.ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj.GetHashCode() == hashCode;
        }

        public override int GetHashCode()
        {
            return hashCode;
        }
    }
}
