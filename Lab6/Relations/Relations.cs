using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrammarRelations
{
	public class Relations
	{
		public List<string> Headers { get; private set; }
		public string[,] Table { get; private set; }

        private static Relations instance;

        public int HeaderIndex(string header)
        {
            for(var i = 0; i < Headers.Count; ++i)
            {
                if (string.Compare(Headers[i], header, true) == 0)
                    return i;
            }
            return -1;
        }

        public static Relations GetInstance()
        {            
            if (instance == null)
                instance = new Relations();                            
            return instance;
        }

		private Relations()
		{
			Headers = new List<string>();
			foreach (var line in Grammar.Lines) {
				Headers.Add(line.Left);
			}
			Headers.AddRange(Lexemes.Lexemes.Table.Keys);
			Table = new string[Headers.Count, Headers.Count];
			for (int i = 0; i < Table.GetLength(0); i++) {
				for (int j = 0; j < Table.GetLength(1); j++) {
					Table[i, j] = "";
				}
			}

            for (int i = 0; i < Table.GetLength(0)-1; i++)
            {
                Table[i, Table.GetLength(0)-1] = ">";
                Table[Table.GetLength(0)-1, i] = "<";
            }

            SetEquality();
			SetMoreLess();
		}

		private void SetEquality()
		{
            foreach (var line in Grammar.Lines)
            {
                foreach (var rightPart in line.Right)
                {
                    for (int i = 0; i < rightPart.Count - 1; i++)
                    {
                        Table[Headers.IndexOf(rightPart[i]), Headers.IndexOf(rightPart[i + 1])] = "=";
                    }
                }
            }
		}

		private bool IsTerminal(string part)
		{
			return !(part.Contains("<") && part.Contains(">")) || part == "<>";
		}

		private void SetMoreLess()
		{
            for (int i = 0; i < Table.GetLength(0); i++)
            {
                for (int j = 0; j < Table.GetLength(1); j++)
                {
                    if (Table[i, j] == "=")
                    {
                        if (!IsTerminal(Headers[i]) && IsTerminal(Headers[j]))
                        {
                            List<string> lastPlus = LastPlus(Headers[i]);
                            foreach (var last in lastPlus)
                            {
                                if (Table[Headers.IndexOf(last), j] != ">")
                                    Table[Headers.IndexOf(last), j] += ">"; 
                            }
                        }
                        else if (IsTerminal(Headers[i]) && !IsTerminal(Headers[j]))
                        {
                            List<string> firstPlus = FirstPlus(Headers[j]);
                            foreach (var first in firstPlus)
                            {
                                if (Table[i, Headers.IndexOf(first)] != "<")
                                    Table[i, Headers.IndexOf(first)] += "<"; 
                            }
                        }
                        else if (!IsTerminal(Headers[i]) && !IsTerminal(Headers[j]))
                        {
                            List<string> lastPlus = LastPlus(Headers[i]);
                            List<string> firstPlus = FirstPlus(Headers[j]);
                            foreach (var first in firstPlus)
                            {
                                if (Table[i, Headers.IndexOf(first)] != "<")
                                    Table[i, Headers.IndexOf(first)] += "<";
                            }
                            foreach (var last in lastPlus)
                            {
                                foreach (var first in firstPlus)
                                {
                                    if (Table[Headers.IndexOf(last), Headers.IndexOf(first)] != ">") 
                                        Table[Headers.IndexOf(last), Headers.IndexOf(first)] += ">";
                                }
                            }
                        }
                    }
                }
            }
		}

		private List<string> FirstPlus(string part, List<string> edges = null)
		{
			if (edges == null) {
				edges = new List<string>();
			}
			GrammarLine line = null;
			foreach (var l in Grammar.Lines) {
				if (l.Left == part) {
					line = l;
					break;
				}
			}

			foreach (var rightPart in line.Right) {
				string edge = rightPart[0];
				if (!edges.Contains(edge)) {
					edges.Add(edge);
					if (!IsTerminal(edge)) {
						edges.AddRange(FirstPlus(edge, edges));
					}
				}
			}
			return edges.Distinct().ToList();
		}

		private List<string> LastPlus(string part, List<string> edges = null)
		{
			if (edges == null) {
				edges = new List<string>();
			}
			GrammarLine line = null;
			foreach (var l in Grammar.Lines) {
				if (l.Left == part) {
					line = l;
					break;
				}
			}

			foreach (var rightPart in line.Right) {
				string edge = rightPart[rightPart.Count - 1];
				if (!edges.Contains(edge)) {
					edges.Add(edge);
					if (!IsTerminal(edge)) {
                        //edges.AddRange(FirstPlus(edge, edges));//check
                        edges.AddRange(LastPlus(edge, edges));
					}
				}
			}
			return edges.Distinct().ToList();
		}
	}
}
