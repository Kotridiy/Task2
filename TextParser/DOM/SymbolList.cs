using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TextParser.DOM
{
    public class SymbolList : List<Symbol>
    {
        public SymbolList PopAll()
        {
            var clone = new SymbolList(this);
            Clear();
            return clone;
        }

        public SymbolList() : base() { }
        public SymbolList(SymbolList clone) : base(clone) { }

        public override string ToString()
        {
            return this.Aggregate(
                new StringBuilder(),
                (str, next) => str.Append(next.Char),
                str => str.ToString()
            );
        }
    }
}
