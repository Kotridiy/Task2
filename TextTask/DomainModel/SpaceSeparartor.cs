using TextParser.DOM.SentenceItem;

namespace TextTask.DomainModel
{
    public class SpaceSeparator : Separator
    {
        private static Separator instance = null;

        public static Separator GetSeparator()
        {
            if (instance == null)
            {
                instance = new SpaceSeparator();
            }
            return instance;
        }

        private SpaceSeparator() : base()
        {
        }

        public override string GetString() => " ";
    }
}