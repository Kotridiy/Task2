using System;
using System.Collections.Generic;
using System.Text;
using TextParser.DOM;
using System.Linq;
using TextParser.DOM.SentenceItem;

namespace TextTask.DomainModel
{
    public static class TextManager
    {
        static char[] consonants = {'q', 'w', 'r', 't', 'p', 's', 'd', 'f', 'g', 'h',
                                    'j', 'k', 'l', 'z', 'x', 'c', 'v', 'b', 'n', 'm'};

        public static IEnumerable<Sentence> SortSentences(Text text)
        {
            return from sentence in text
                   orderby GetWordsCount(sentence)
                   select sentence;
        }

        private static int GetWordsCount(Sentence sentence)
        {
            return sentence.SentencePart.Aggregate(0,
                (count, next) => count + (next is Word ? 1 : 0) );
        }

        public static IEnumerable<Word> GetWordsInQuestions(Text text, int wordLength)
        {
            return (from sentence in text
                   where sentence.EndPunctuation.GetString() == "?"
                   from item in sentence.SentencePart
                   let word = item as Word
                   where word != null && word.Letters.Count == wordLength
                   select word)
                   .Distinct();
        }

        public static Text DeleteWords(Text text, int wordLength)
        {
            var sentences = from sentence in text
                            let items =
                                from item in sentence.SentencePart
                                let word = item as Word
                                where !(word != null && word.Letters.Count == wordLength
                                && consonants.Contains(char.ToLower(word.Letters[0].Char)))
                                select item
                            select new Sentence(items.ToList(), sentence.EndPunctuation);
            return new Text(sentences.ToArray());
        }

        public static void PasteSubstring(Text text, IEnumerable<ISentenceItem> substring, int index, int wordLength)
        {
            if (index < 0 || index >= text.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            LinkedList<ISentenceItem> newList = new LinkedList<ISentenceItem>(text[index].SentencePart);
            foreach (var item in text[index].SentencePart)
            {
                var word = item as Word;
                if (word != null && word.Letters.Count == wordLength)
                {
                    var node = newList.Find(item);
                    foreach (var element in substring)
                    {
                        newList.AddBefore(node, element);
                    }
                    newList.Remove(node);
                }
            }
            text[index] = new Sentence(newList.ToList(), text[index].EndPunctuation);
        }
    }
}
