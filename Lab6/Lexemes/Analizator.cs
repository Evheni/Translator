using Lexemes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translator.Lexemes;

namespace Translator
{    
    class Analizator
    {
        #region Private Members
        private LexemeTable<Lexeme> inputLexemeList;
        private LexemeTable<OutputLexeme> outputLexemeList;
        private List<LexemeClass> lexemesClass;

        private HashSet<Identifier> idsTable;
        private HashSet<Tuple<int, string>> consts;
        private HashSet<Tuple<int, string>> labels;
        private List<string> errors;
        private Dictionary<int, string> afterGoto;        
        private Queue<string> lastDelimiter;

        private int currentLine = 0;
        private bool startVar = false;
        private bool build = false;
        #endregion

        internal LexemeTable<OutputLexeme> OutputLexemeList
        {
            get
            {
                return outputLexemeList;
            }
        }

        internal HashSet<Identifier> IdsTable
        {
            get
            {
                return idsTable;
            }
        }

        #region Constructors
        public Analizator(List<Lexeme> lexemeList, List<Lexemes.LexemeClass> lexemesClass) 
            : this(
                  new LexemeTable<Lexeme>(
                      lexemeList, 
                      lexemeList.Find(x => x.Name == "id").ID,
                      lexemeList.Find(x => x.Name == "con").ID,
                      lexemeList.Find(x => x.Name == "label").ID
                ), lexemesClass) { }

        public Analizator(LexemeTable<Lexeme> lexemeList, List<LexemeClass> lexemesClass)
        {
            inputLexemeList = lexemeList;
            this.lexemesClass = lexemesClass;
            outputLexemeList = new LexemeTable<OutputLexeme>(new List<OutputLexeme>(),
                      lexemeList.FindByName("id").ID,
                      lexemeList.FindByName("con").ID,
                      lexemeList.FindByName("label").ID);            
            idsTable = new HashSet<Identifier>();
            consts = new HashSet<Tuple<int, string>>();
            labels = new HashSet<Tuple<int, string>>();
            afterGoto = new Dictionary<int, string>();
            lastDelimiter = new Queue<string>();
        }
        #endregion

        #region View Methods
        public void ShowErrors(Action<string> Writer)
        {

            if (errors == null)
            {
                Writer("Ошибок не найдено." + Environment.NewLine);
            }
            else
            {
                int number = 1;
                foreach (var error in errors)
                {
                    Writer(number + ": " + error + Environment.NewLine);
                    number++;
                }
            }
        }

        public void ShowLexemes(Action<string> Writer)
        {
            Writer(new string('─', 79));
            Writer(String.Format("{0,-9}│{1,-48}│{2,-10}│{3,-9}", " №", "Substring", "id", "index"));
            Writer(new string('─', 79));
            foreach(var lexeme in outputLexemeList)
            {
                Writer(String.Format(" {0,-8}│{1,-48}│{2,-10}│{3,-9}", 
                    ((OutputLexeme)lexeme).StringNumber, 
                    lexeme.Name, 
                    lexeme.ID,
                    ((OutputLexeme)lexeme).Index));
            }
            Writer(new string('─', 79));
        }

        public bool HasError()
        {
            return errors != null && errors.Count > 0;
        }

        public void ShowLabels(Action<string> Writer)
        {
            Writer(new string('─', 40));
            Writer(String.Format("│ {0,-26} │ {1,-7} │", "Labels", "index"));
            Writer(new string('─', 40));
            int index = 1;
            foreach (var label in labels)
            {
                Writer(String.Format("│ {0,-26} │ {1,-7} │",
                    label.Item2,
                    index++));
            }
            Writer(new string('─', 40));
        }

        public void ShowConstatnts(Action<string> Writer)
        {
            Writer(new string('─', 40));
            Writer(String.Format("│ {0,-26} │ {1,-7} │", "Const", "index"));
            Writer(new string('─', 40));
            int index = 1;
            foreach (var con in consts)
            {
                Writer(String.Format("│ {0,-26} │ {1,-7} │",
                    con.Item2,
                    index++));
            }
            Writer(new string('─', 40));
        }

        public void ShowIds(Action<string> Writer)
        {
            Writer(new string('─', 40));
            Writer(String.Format("│ {0,-26} │ {1,-7} │", "ID", "index"));
            Writer(new string('─', 40));
            int index = 1;
            foreach (var id in idsTable)
            {
                Writer(String.Format("│ {0,-26} │ {1,-7} │",
                    id,
                    index++));
            }
            Writer(new string('─', 40));
        }
        #endregion

        public void AddString(string line)
        {
            if (build)
            {
                throw new Exception("Building was completed.");
            }
            currentLine++;
            try {
                for (int index = 0; index < line.Length; ++index)
                {
                    string lexeme = "";                    
                    while (index < line.Length)
                    {
                        switch (FindClass(line[index].ToString()))
                        {
                            case "E":
                            case "Leter":
                                lexeme = State2(line, ref index);
                                AddToTable(lexeme);
                                break;
                            case "Number":
                                lexeme = State3(line, ref index);
                                AddToTable(lexeme);
                                break;
                            case ".":
                                lexeme = State4(line, ref index);
                                AddToTable(lexeme);
                                break;
                            case "<":
                                lexeme = State9(line, ref index);
                                AddToTable(lexeme);
                                break;
                            case ">":
                                lexeme = State9(line, ref index);
                                AddToTable(lexeme);
                                break;
                            case "=":
                                lexeme = State9(line, ref index);
                                AddToTable(lexeme);
                                break;
                            case "!":
                                lexeme = State12(line, ref index);
                                AddToTable(lexeme);
                                break;
                            case "+":
                            case "Delimiter":
                                lastDelimiter.Enqueue(line[index].ToString());                                
                                index++;                                                                                            
                                break;
                            default:
                                throw new Exception("State 1. Ошибка в строке: " + line);
                        }
                    }                   
                }
                AddDelimiters();
            }
            catch(Exception e)
            {
                if (errors == null)
                    errors = new List<string>();
                errors.Add(e.Message);
            }
        }

        public void Build(Action<string> Writer = null)
        {
            build = true;
            FindErrorLabels();
            if (Writer != null)
            {
                ShowErrors(Writer);

                ShowLexemes(Writer);
                Writer(Environment.NewLine);

                ShowLabels(Writer);
                Writer(Environment.NewLine);

                ShowIds(Writer);
                Writer(Environment.NewLine);

                ShowConstatnts(Writer);
                Writer(Environment.NewLine);
            }
        }

        #region States
        //+
        private string State2(string line, ref int start)
        {
            StringBuilder s = new StringBuilder();
            s.Append(line[start]);
            start++;
            for (; start < line.Length; start++)
            {
                switch(FindClass(line[start].ToString()))
                {
                    case "E":
                    case "Leter":
                    case "Number":
                        s.Append(line[start]);
                        break;
                    default: return s.ToString();
                }
            }
            return s.ToString();
        }
        //+
        private string State3(string line, ref int start)
        {
            StringBuilder s = new StringBuilder();
            s.Append(line[start]);
            start++;
            for (; start < line.Length; start++)
            {
                switch (FindClass(line[start].ToString()))
                {
                    case "Number":
                        s.Append(line[start]);
                        break;
                    case "E":
                        s.Append(State6(line, ref start));
                        return s.ToString();
                    case ".":
                        s.Append(State5(line, ref start));
                        return s.ToString();
                    default: return s.ToString();
                }
            }
            return s.ToString();
        }
        //+
        private string State4(string line, ref int start)
        {
            StringBuilder s = new StringBuilder();
            s.Append(line[start]);
            start++;
            if (start < line.Length)
            {
                switch (FindClass(line[start].ToString()))
                {
                    case "Number":
                        s.Append(State5(line, ref start));
                        return s.ToString();
                    default:
                        throw new Exception("State 4. Ошибка в строке: " + line);
                }
            }
            return s.ToString();
        }
        //+
        private string State5(string line, ref int start)
        {
            StringBuilder s = new StringBuilder();
            s.Append(line[start]);
            start++;
            for (; start < line.Length; start++)
            {
                switch (FindClass(line[start].ToString()))
                {
                    case "Number":
                        s.Append(line[start]);
                        break;
                    case "E":
                        s.Append(State6(line, ref start));
                        return s.ToString();
                    default: return s.ToString();
                }
            }
            return s.ToString();
        }
        //+
        private string State6(string line, ref int start)
        {
            StringBuilder s = new StringBuilder();
            s.Append(line[start]);
            start++;
            if (start < line.Length)
            {
                switch (FindClass(line[start].ToString()))
                {
                    case "Number":
                        s.Append(State8(line, ref start));
                        return s.ToString();
                    case "+":
                        s.Append(State7(line, ref start));
                        return s.ToString(); 
                    default:
                        throw new Exception("State6. Ошибка в строке: " + line);
                }
            }
            return s.ToString();
        }
        //+
        private string State7(string line, ref int start)
        {
            StringBuilder s = new StringBuilder();
            s.Append(line[start]);
            start++;
            if (start < line.Length)
            {
                switch (FindClass(line[start].ToString()))
                {
                    case "Number":
                        s.Append(State8(line, ref start));
                        return s.ToString();
                    default:
                        throw new Exception("State7. Ошибка в строке: " + line);
                }
            }
            return s.ToString();
        }    
        //+
        private string State8(string line, ref int start)
        {
            StringBuilder s = new StringBuilder();
            s.Append(line[start]);
            start++;
            for (; start < line.Length; start++)
            {
                switch (FindClass(line[start].ToString()))
                {
                    case "Number":
                        s.Append(line[start]);
                        break;
                    default:
                        return s.ToString();
                }
            }
            return s.ToString();
        }    
        //+
        private string State9(string line, ref int start)
        {
            StringBuilder s = new StringBuilder();
            s.Append(line[start]);
            start++;
            for (; start < line.Length; start++)
            {
                switch (FindClass(line[start].ToString()))
                {
                    case "=":
                        s.Append(line[start]);
                        break;
                    default:
                        return s.ToString();
                }
            }
            return s.ToString();
        }
        //+ ==state9
        private string State10(string line, ref int start)
        {
            StringBuilder s = new StringBuilder();
            s.Append(line[start]);
            start++;
            for (; start < line.Length; start++)
            {
                switch (FindClass(line[start].ToString()))
                {
                    case "=":
                        s.Append(line[start]);
                        break;
                    default:
                        return s.ToString();
                }
            }
            return s.ToString();
        }
        //+ ==state9
        private string State11(string line, ref int start)
        {
            StringBuilder s = new StringBuilder();
            s.Append(line[start]);
            start++;
            for (; start < line.Length; start++)
            {
                switch (FindClass(line[start].ToString()))
                {
                    case "=":
                        s.Append(line[start]);
                        break;
                    default:
                        return s.ToString();
                }
            }
            return s.ToString();
        }
        //+
        private string State12(string line, ref int start)
        {
            StringBuilder s = new StringBuilder();
            s.Append(line[start]);
            start++;
            for (; start < line.Length; start++)
            {
                switch (FindClass(line[start].ToString()))
                {
                    case "=":
                        s.Append(line[start]);
                        break;
                    default:
                        throw new Exception("State12. Ошибка в строке: " + line);
                }
            }
            return s.ToString();
        }
        #endregion

        #region Search in tables
        private string FindClass(string s)
        {
            foreach(var cl in lexemesClass)
            {
                if (cl.IsMatch(s))
                    return cl.Name;
            }
            return null;
        }

        private int FindLexemeByName(string name)
        {
            var lexeme = inputLexemeList.FindByName(name);

            if (lexeme == null)
                return 0;
            return lexeme.ID;
        }

        private int FindLexeme(string var)
        {
            var lexeme = inputLexemeList.FindLexeme(var);

            if (lexeme == null)
                return 0;
            return lexeme.ID;
        }

        private void FindErrorLabels()
        {
            foreach (var el in afterGoto)
            {
                bool t = true;
                foreach (var lab in labels)
                {
                    if (lab.Item2 == el.Value)
                    {
                        t = false;
                        break;
                    }
                }
                if (t)
                {
                    errors.Add(
                        "Необъявленная метка " + el.Value + ". В сроке " + el.Key);
                }
            }
        }
        #endregion

        private void AddToTable(string lexeme)
        {
            int id = FindLexeme(lexeme);
            if (lastDelimiter.Count > 0 &&
                lastDelimiter.Peek() == "&" &&
                id == FindLexeme("id"))
            {
                id = FindLexeme("&" + lexeme);
            }
            if (outputLexemeList.Count != 0 &&
                outputLexemeList.Last().Name == "goto")
            {
                afterGoto.Add(currentLine, lexeme);
                id = FindLexeme("&" + lexeme);
            }
            AddDelimiters();
            if (id != 0)
                AddLexeme(id, lexeme);
        }

        private void AddDelimiters()
        {
            while (lastDelimiter.Count > 0)
            {
                var el = lastDelimiter.Dequeue();
                int k = FindLexeme(el);
                if (k != 0)
                {
                    AddLexeme(k, el);
                }
            }
        }

        private void AddLexeme(int id, string name)
        {
            int? index = null;
            if (id == FindLexemeByName("id"))
            {
                index = AddId(name);            
            }
            else if (id == FindLexemeByName("label"))
            {
                index = AddLabel(name);            
            }
            else if (id == FindLexemeByName("con"))
            {
                index = AddConst(name);             
            }
            if (name == "Program" || name == "var"
                || name == "real")
            {
                startVar = true;
            }
            if ((name == "begin" || name == ":") && startVar)
                startVar = false;
            outputLexemeList.Add(
                new Lexemes.OutputLexeme(
                    currentLine,
                    name == "\n" ? "enter" : name,
                    id,
                    index)
                    );
        }

        private int AddConst(string name)
        {
            consts.Add(new Tuple<int, string>(consts.Count+1, name));
            return consts.Count;
        }

        private int? AddId(string name)
        {
            if (startVar)
            {
                idsTable.Add(new Identifier(idsTable.Count + 1, name));
                return idsTable.Count;
            }
            else
            {
                if (!idsTable.Contains(new Identifier(name)))
                    throw new Exception("Необъявленная переменная: " + name +
                        Environment.NewLine + "В строке №" + currentLine);
            }
            return (idsTable.FirstOrDefault(x => x.Name == name)).Index;
        }

        private int AddLabel(string name)
        {
            var labl = labels.FirstOrDefault(x => x.Item2 == name);
            if (labl == null)
            {
                labels.Add(new Tuple<int, string>(labels.Count + 1, name));
                return labels.Count;
            }
            else
                return labl.Item1;
        }
    }
}
