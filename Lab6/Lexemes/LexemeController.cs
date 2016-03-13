using System;
using System.Collections.Generic;
using Translator;

namespace Lexemes
{
    class LexemeController
    {
        #region Members
        private string[] code;
        private static int id = 1;
        List<Lexeme> lexemesList = new List<Lexeme>
            {
                new Lexeme(@"\s*var\s*", id++, false, "var"),
                new Lexeme(@"\s*begin\s*", id++, false, "begin"),
                new Lexeme(@"\s*end\s*", id++, false, "end"),
                new Lexeme(@"\s*real\s*", id++, false, "real"),
                new Lexeme(@"\s*ReadLine\s*", id++, false, "ReadLine"),
                new Lexeme(@"\s*WriteLine\s*", id++, false, "WriteLine"),
                new Lexeme(@"\s*do\s*", id++, false, "do"),
                new Lexeme(@"\s*to\s*", id++, false, "to"),
                new Lexeme(@"\s*next\s*", id++, false, "next"),
                new Lexeme(@"\s*if\s*", id++, false, "if"),
                new Lexeme(@"\s*then\s*", id++, false, "then"),
                new Lexeme(@"\s*goto\s*", id++, false, "goto"),
                new Lexeme(@"\s*or\s*", id++, false, "or"),
                new Lexeme(@"\s*and\s*", id++, false, "and"),
                new Lexeme(@"\s*not\s*", id++, false, "not"),
                new Lexeme(@"\s*\:\s*", id++, true, ":"),
                new Lexeme(@"\s*\=\s*", id++, true, "="),
                new Lexeme(@"\s*\,\s*", id++, true, ","),
                new Lexeme(@"\s*\=\=\s*", id++, false, "=="),
                new Lexeme(@"\s*\n\s*", id++, true, "enter"),
                new Lexeme(@"^\+\s*", id++, true, "+"),
                new Lexeme(@"^\-$", id++, true, "-"),
                new Lexeme(@"^\*$", id++, true, "*"),
                new Lexeme(@"^\/$", id++, true, "/"),
                new Lexeme(@"^\($", id++, true, "("),
                new Lexeme(@"^\)$", id++, true, ")"),
                new Lexeme(@"^\<$", id++, true, "<"),
                new Lexeme(@"^\<\=$", id++, false, "<="),
                new Lexeme(@"^\>$", id++, true, ">"),
                new Lexeme(@"^\>\=$", id++, false, ">="),
                new Lexeme(@"^\!\=$", id++, true, "!="),
                new Lexeme(@"^\[$", id++, true, "["),
                new Lexeme(@"^\]$", id++, true, "]"),
                new Lexeme(@"^\&$", id++, false, "&"),
                new Lexeme(@"^[a-zA-Z][a-zA-Z0-9]*\s*", id++, false, "id"),
                new Lexeme(@"^(((\-|\+){0,1}\d+\.{0,1}?\d{0,})|((\-|\+){0,1}?\.\d+))(E((\-|\+){0,1}?)\d{1,}){0,1}?$", id++, false, "con"),
                new Lexeme(@"^\&[a-zA-Z][a-zA-Z0-9]*\s*", id++, false, "label")
            };
        List<LexemeClass> classes = new List<LexemeClass>
            {
                new LexemeClass(@"[a-df-zA-DF-Z]", "Leter"),
                new LexemeClass(@"[0-9]", "Number"),
                new LexemeClass(@"(\:|\,|\*|\/|\(|\)|\[|\]|\&|\s)", "Delimiter"),
                new LexemeClass(@"\<", "<"),
                new LexemeClass(@"\>", ">"),
                new LexemeClass(@"\=", "="),
                new LexemeClass(@"E|e", "E"),
                new LexemeClass(@"\!", "!"),
                new LexemeClass(@"\.", "."),
                new LexemeClass(@"\+|\-", "+")
            };
        #endregion
        public HashSet<Identifier> Ids { get; private set; }

        public LexemeController(string[] code)
        {
            this.code = code;
            id = 1;
            string[] lexemes = new string[lexemesList.Count + 1];
            lexemes[lexemesList.Count] = "#";
            for(var i = 0; i < lexemesList.Count; ++i)
            {
                lexemes[i] = lexemesList[i].Name;
            }
            Lexemes.SetDictionary(lexemes);
        }

        public LexemeTable<OutputLexeme> LexicalAnalysis(Action<string> Writer)
        {
            var analizator = new Analizator(lexemesList, classes);
            string resultString;
            try {
                resultString = BuildeLexemeTable(code, analizator);
            }catch(Exception ex)
            {
                Writer(ex.Message);
                return null;
            }
            Writer(resultString);
            return analizator.OutputLexemeList;
        }

        public LexemeTable<OutputLexeme> LexicalAnalysis()
        {
            var analizator = new Analizator(lexemesList, classes);
            BuildeLexemeTable(code, analizator);
            Ids = analizator.IdsTable;
            return analizator.OutputLexemeList;
        }

        private string BuildeLexemeTable(string[] code, Analizator analizator)
        {
            foreach (var line in code)
            {
                analizator.AddString(line + '\n');
            }
            analizator.Build();
            if (analizator.HasError())
            {
                var errors = "Ошибки при лексическом разборе." + Environment.NewLine;
                analizator.ShowErrors((s) => { errors += s + Environment.NewLine; });
                throw new Exception(errors);
            }
            string table = "";
            analizator.ShowLexemes((s) => { table += s + Environment.NewLine; });
            return table;
        }
    }
}
