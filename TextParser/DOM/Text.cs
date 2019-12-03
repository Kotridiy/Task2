using System;
using System.Collections;
using System.Collections.Generic;

namespace TextParser.DOM
{
    public class Text : IEnumerable<Sentence>
    {
        private Sentence[] _sentences;

        public Text(Sentence[] sentences)
        {
            _sentences = sentences ?? throw new ArgumentNullException(nameof(sentences));
        }

        public IEnumerator<Sentence> GetEnumerator()
        {
            foreach (var sentence in _sentences)
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
                return _sentences[index];
            }
            set
            {
                _sentences[index] = value;
            }
        }

        public int Length => _sentences.Length;
    }
}