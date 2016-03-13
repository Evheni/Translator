
namespace Translator.Lexemes
{
    class Lexeme : AbstractLexeme
    {
        #region Overrided Properties
        public override string Pattern { get; protected set; }
        public override int ID { get; protected set; }
        public override bool IsDelimiter { get; protected set; }
        public override string Name { get; protected set; }
        #endregion

        #region Constructors
        public Lexeme(string pattern, int id, bool isDelimiter, string name)
        {
            this.Pattern = pattern;
            ID = id;
            IsDelimiter = isDelimiter;
            Name = name;
        }
        #endregion
    }
}
