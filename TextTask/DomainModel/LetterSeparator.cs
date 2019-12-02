using TextParser.DOM;

namespace TextTask.DomainModel
{
    class LetterSeparator : Symbol, ILetter
    {
        LetterSeparator() : base(' ') { }
    }
}
