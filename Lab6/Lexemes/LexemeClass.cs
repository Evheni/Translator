using System.Text.RegularExpressions;

namespace Lexemes
{
    class LexemeClass
    {
        #region Private Members
        private string pattern;
        #endregion

        #region Properties
        public string Name { get; protected set; }
        #endregion

        #region Constructors
        public LexemeClass(string pattern, string name)
        {
            Name = name;
            this.pattern = pattern;
        }
        #endregion

        #region Methods
        public bool IsMatch(string input)
        {
            var rgx = new Regex(pattern);
            return rgx.IsMatch(input);
        }
        #endregion
    }
}
