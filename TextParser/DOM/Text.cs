using System;
using System.Collections;
using System.Collections.Generic;
using TextParser.DOM.TextItem;

namespace TextParser.DOM
{
    public class Text : IEnumerable<Sentence>
    {
        private Sentence[] sentences;

        public Text(Sentence[] sentences)
        {
            this.sentences = sentences ?? throw new ArgumentNullException(nameof(sentences));
        }

        public IEnumerator<Sentence> GetEnumerator()
        {
            foreach (var sentence in sentences)
            {
                yield return sentence;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public Sentence this[int index] 
        {
            get
            {
                return sentences[index];
            }
        }
    }
}