using Lexemes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Analysis
{
    class AnalysisController
    {
        //public string Errors { get; private set; } = "";
        LexemeTable<OutputLexeme> outLex;
        List<object[]> resultTable;
        Dictionary<string, double> variables;

        public AnalysisController(LexemeTable<OutputLexeme> lexemesTable)
        {
            this.outLex = lexemesTable;
            //Task.Run(Parse);
            //Parse();
        }
        public List<object[]> ResultTable
        {
            get { return resultTable; }
        }

        public Dictionary<string, double> Variables
        {
            get { return variables; }
        }

        public async Task ParseAsync()
        {
            resultTable = await Task.Run(() =>
            {
                var parser = new AscendingParser();
                parser.AscendingAnalysis(outLex);
                return parser.AscAn;
            });
        }

        public void Parse(string[] names = null)
        {
            var parser = new AscendingParser();
            if (names != null)
            {
                parser.AddVariables(names);
            }

            try
            {                
                parser.AscendingAnalysis(outLex);
            }
            finally
            {
                resultTable = parser.AscAn;
                variables = parser.Values;
            }
        }
        
    }
}
