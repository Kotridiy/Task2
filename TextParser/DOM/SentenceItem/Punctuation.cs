using System;

namespace TextParser.DOM.SentenceItem
{
    public class Punctuation : ISentenceItem
    {
        private string punctuation;

        public virtual string GetString() => punctuation;

        public Punctuation(string punctuation)
        {
            if (string.IsNullOrEmpty(punctuation))
            {
                throw new ArgumentNullException(nameof(punctuation));
            }
            this.punctuation = punctuation;
        }
    }
}
