namespace Lexemes
{
    public interface ILexeme
    {        
        string Name { get; }
        int ID { get; }
        bool IsDelimiter { get; }

        bool IsMatch(string input, int startat = 0);
    }
}
