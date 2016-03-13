using System.Collections.Generic;

namespace Lexemes
{
	public static class Lexemes
	{
		public static Dictionary<string, int> Table { get; private set; }
        public static void SetDictionary(string[] lexemes)
        {
            FillLexemeTable(lexemes);
        }
		static Lexemes()
		{
			Table = new Dictionary<string, int>();
            SetDictionary(new string[] {
                "var",
                "begin",
                "end",
                "real",
                "ReadLine",
                "WriteLine",
                "do",
                "to",
                "next",
                "if",
                "then",
                "goto",
                "and",
                "or",
                "not",
                "enter",
                ",",
                ":",
                "+",
                "-",
                "*",
                "/",
                "(",
                ")",
                ">",
                "<",
                "=",
                "[",
                "]",
                "<=",
                ">=",
                "!=",
                "==",
                "&",
                "id",
                "con",
                "label",
                "#"
            });
		}

		private static void FillLexemeTable(string[] lexemes)
		{
            Table.Clear();
			for (int i = 0; i < lexemes.Length; ) {
				Table.Add(lexemes[i], ++i);
			}
		}
	}
}
