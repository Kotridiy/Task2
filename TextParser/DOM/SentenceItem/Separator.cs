using System;

namespace TextParser.DOM.SentenceItem
{
    public class Separator : ISentenceItem
    {
        private string separator;

        public virtual string GetString() => separator;

        public Separator(string separator)
        {
            if (string.IsNullOrEmpty(separator))
            {
                throw new ArgumentNullException(nameof(separator));
            }
            this.separator = separator;
        }

        protected Separator() { }
    }
}