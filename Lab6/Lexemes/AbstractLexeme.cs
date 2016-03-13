using Lexemes;
using System;
using System.Text.RegularExpressions;

namespace Translator
{
    abstract class AbstractLexeme : ILexeme
    {
        #region Abstract Properties
        public abstract string Pattern { get; protected set; }
        public abstract int ID { get; protected set; }
        public abstract bool IsDelimiter { get; protected set; }
        public abstract string Name { get; protected set; }
        #endregion

        #region Virtual Methods
        public virtual bool IsMatch(string input, int startat = 0)
        {            
            var rgx = new Regex(Pattern);
            return rgx.IsMatch(input, startat);
        }
        #endregion
    }
}
