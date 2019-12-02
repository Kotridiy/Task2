using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextParser.DOM.SentenceItem;

namespace TextParser.DOM.TextItem
{
    public class Sentence : ISentencePart
    {
        public List<ISentenceItem> SentencePart { get; private set; }

        public Punctuation EndPunctuation { get; private set; }

        IEnumerable<ISentenceItem> ISentencePart.SentencePart => SentencePart;

        public Sentence(List<ISentenceItem> items, Punctuation endPunctuation)
        {
            this.SentencePart = items ?? throw new ArgumentNullException(nameof(items));
            EndPunctuation = endPunctuation ?? throw new ArgumentNullException(nameof(endPunctuation));
        }

        public override string ToString()
        {
            return SentencePart.Aggregate(
                new StringBuilder(),
                (str, next) => str.Append(next.GetString()),
                str => str.ToString()
            );
        }

        public string ToPrint()
        {
            StringBuilder str = new StringBuilder(ToString() + EndPunctuation.GetString());
            str.AppendLine();
            foreach (var item in SentencePart)
            {
                str.AppendLine($"  {item.GetType().Name}: \"{item.GetString()}\"");
            }
            return str.ToString();
        }
    }
}