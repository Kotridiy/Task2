using System.Collections.Generic;
using TextParser.DOM.SentenceItem;

namespace TextParser.DOM.TextItem
{
    public interface ISentencePart
    {
        IEnumerable<ISentenceItem> SentencePart { get; }
    }
}
