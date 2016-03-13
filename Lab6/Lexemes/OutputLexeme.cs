
namespace Translator.Lexemes
{
    class OutputLexeme : AbstractLexeme
    {
        #region Properties
        #region Overrided Properties
        public override string Pattern { get; protected set; }
        public override int ID { get; protected set; }
        public override bool IsDelimiter { get; protected set; }
        public override string Name { get; protected set; }
        #endregion
        public int StringNumber { get; protected set; }
        public int? Index { get; protected set; }
        #endregion

        #region Constructors
        public OutputLexeme(int strNumber, string name, int id, int? index = null)
        {
            ID = id;
            Index = index;
            IsDelimiter = (index == null);
            Name = name;
            StringNumber = strNumber;
        }
        #endregion
    }
}
