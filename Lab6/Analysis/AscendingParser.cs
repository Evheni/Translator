using GrammarRelations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Analysis
{
    class AscendingParser
    {
        public List<string> input { get; private set; } = new List<string>(); //входная цепочка
        public List<string> stack { get; private set; } = new List<string>(); //стек-результат
        public List<string> temp { get; private set; } = new List<string>(); //цепочка для редукции
        public List<string> lineNumber { get; private set; } = new List<string>(); //номера строк элементов входа
        public List<string> poliz { get; private set; } = new List<string>(); //ПОЛИЗ
        List<string> cpol { get; set; } = new List<string>();
        

        public Dictionary<string, double> Values { get; private set; } = new Dictionary<string, double>(); //переменные и их значения
        //private Dictionary<int, string> lines = new Dictionary<int, string>();

        public List<object[]> AscAn { get; private set; } = new List<object[]>();

        public bool AscendingAnalysis(List<string[]> outLex)
        {
            //int index = 0;
            int step;
            string ss, si, st, sp, sv;
            bool isAllConst = true;
            bool isRule = false;
            bool needClear = false;
            lineNumber.Add("#");
            for (var i = 0; i < outLex.Count - 1; i++)
            {
                if (outLex[i][3] == "35")
                {
                    input.Add("id");
                    lineNumber.Add(outLex[i][1]);                    
                }
                else if (outLex[i][3] == "36")
                {
                    input.Add("con");
                    lineNumber.Add(outLex[i][1]);
                }
                else if (outLex[i][3] == "37")
                {
                    input.Add("label");
                    lineNumber.Add(outLex[i][1]);
                }
                else
                {
                    input.Add(outLex[i][2]);
                    lineNumber.Add(outLex[i][1]);
                }
            }
            input.Add("#");
            stack.Add("#");
            step = 1;
            ss = ""; si = ""; st = ""; sp = ""; sv = ""; isRule = false;
            for (var i = 0; i < stack.Count(); i++) ss = ss + stack[i].ToString() + " ";
            for (var j = 0; j < input.Count(); j++) si = si + input[j].ToString() + " ";
            for (var j = 0; j < poliz.Count(); j++) sp = sp + poliz[j].ToString() + " ";
            AscAn.Add(new object[] { step, ss, "", si, sp, sv });
            string lastVar = null;
            while (input.Count() != 0)//предел редукции
            {                
                if (lastVar == null && input.Count > 1 && input[1] == "=") 
                    lastVar = outLex[outLex.Count - input.Count][2];
                if (FindRelation(input.First(), stack.Last()) != "") AscAn[step - 1][2] = FindRelation(input.First(), stack.Last());
                else
                {
                    throw new Exception(
                        "(" + lineNumber.First() + ") - " 
                        + "'" + stack.Last() + "' can't stand next to '" 
                        + input.First() + "'");
                    // return false;
                }
                if (AscAn[step - 1][2].ToString() == "<" || AscAn[step - 1][2].ToString() == "=")
                {
                    stack.Add(input.First());
                    input.RemoveAt(0);
                    lineNumber.RemoveAt(0);
                    step++;
                }
                else
                {
                    temp.Add(stack.Last());
                    stack.RemoveAt(stack.Count - 1);
                    while (FindRelation(temp.Last(), stack.Last()) != "<")
                    {
                        temp.Add(stack.Last());
                        stack.RemoveAt(stack.Count - 1);
                    }
                    temp.Reverse();

                    //добаление константы или идентификатора в ПОЛИЗ
                    if (temp.Count == 1 && (temp.Last() == "id" || temp.Last() == "con"))
                    {
                        //if (temp.Last() == "id") isAllConst = false;
                        poliz.Add(outLex[outLex.Count - input.Count - 1][2]);
                    }

                    if (temp.First() == "<прогр>")
                        return true;
                    else
                    {
                        foreach (var line in Grammar.Lines)
                        {
                            for (var i = 0; i < line.Right.Count(); i++)
                            {
                                if (temp.SequenceEqual(line.Right[i]))
                                {
                                    //для ПОЛИЗ
                                    if (temp.Count == 3 && (temp[1] == "+" || temp[1] == "-" || temp[1] == "*" || temp[1] == "/"))
                                    {
                                        //-
                                        poliz.Add(temp[1]);
                                        stack.Add(line.Left);
                                        isRule = true;
                                        temp.Clear();
                                        step++; break;
                                    }
                                    //------------------------------------
                                    else
                                    {
                                        stack.Add(line.Left);
                                        isRule = true;
                                        temp.Clear();
                                        step++; break;
                                    }
                                }
                            }
                            if (isRule) break;
                        }

                        //очищение стекa
                        if ((stack.Last() == "<ар.вир2>" && stack[stack.Count - 2] != "(") ||
                            (stack.Last() == "<ар.вир1>" && stack[stack.Count - 2] != "(") ||
                            (stack.Last() == "<знак>" && stack[stack.Count - 2] == "<ар.вир>"))
                        {
                            //посчитать ПОЛИЗ!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                            if (isAllConst)
                            {
                                for (var i = 0; i < poliz.Count; i++) cpol.Add(poliz[i]);
                                if (cpol.Count > 2)
                                {
                                    var i = 2;
                                    while (i < cpol.Count)
                                    {
                                        if (cpol[i] == "+" || cpol[i] == "-" || cpol[i] == "*" || cpol[i] == "/")
                                        {
                                            if ((cpol[i - 1] != "+" || cpol[i - 1] != "-" || cpol[i - 1] != "*" || cpol[i - 1] != "/") &&
                                                (cpol[i - 2] != "+" || cpol[i - 2] != "-" || cpol[i - 2] != "*" || cpol[i - 2] != "/"))
                                            {
                                                NumberStyles styles = NumberStyles.AllowExponent | NumberStyles.Number;
                                                //var cul = CultureInfo.CreateSpecificCulture("en-US");
                                                if (Values.ContainsKey(cpol[i - 2])) cpol[i - 2] = GetVariableValue(cpol[i - 2]).ToString();
                                                if (Values.ContainsKey(cpol[i - 1])) cpol[i - 1] = GetVariableValue(cpol[i - 1]).ToString();
                                                if (cpol[i] == "+")
                                                    cpol[i - 2] = (double.Parse(cpol[i - 2].Replace('.', ','), styles) + double.Parse(cpol[i - 1].Replace('.', ','), styles)).ToString();
                                                else if (cpol[i] == "-")
                                                    cpol[i - 2] = (double.Parse(cpol[i - 2].Replace('.', ','), styles) - double.Parse(cpol[i - 1].Replace('.', ','), styles)).ToString();
                                                else if (cpol[i] == "*")
                                                    cpol[i - 2] = (double.Parse(cpol[i - 2].Replace('.', ','), styles) * double.Parse(cpol[i - 1].Replace('.', ','), styles)).ToString();
                                                else if (cpol[i] == "/")
                                                    cpol[i - 2] = (double.Parse(cpol[i - 2].Replace('.', ','), styles) / double.Parse(cpol[i - 1].Replace('.', ','), styles)).ToString();
                                                for (var j = i - 1; j < cpol.Count - 2; j++)
                                                    cpol[j] = cpol[j + 2];
                                                while (cpol.Count > cpol.Count - 2)
                                                    cpol.RemoveAt(cpol.Count - 1);
                                                i = 2;
                                            }
                                            else i++;
                                        }
                                        else i++;
                                    }
                                    sv = cpol[0];
                                }
                                else if (cpol.Count == 1)
                                {
                                    sv = cpol[0];
                                    //if()
                                }
                            }
                            else
                            {
                                sv = "0";
                                isAllConst = true;
                            }
                            needClear = true;
                        }

                        st = "";
                        if (!isRule)
                        {
                            for (var i = 0; i < temp.Count(); i++)
                                st = st + temp[i].ToString() + " ";
                            throw new Exception("(" + lineNumber.First() + ") - " + "Can't reduce '" + st);
                            // return false;
                        }
                    }
                }
                ss = "";
                si = "";
                st = "";
                sp = "";
                isRule = false;
                for (var i = 0; i < stack.Count(); i++)
                    ss += stack[i].ToString() + " ";
                for (var j = 0; j < input.Count(); j++)
                    si += input[j].ToString() + " ";
                for (var j = 0; j < poliz.Count(); j++)
                    sp += poliz[j].ToString() + " ";
                if (lastVar != null && sv != "")
                {
                    SetVariable(lastVar, double.Parse(sv));
                    lastVar = null;
                }
                if (Values.ContainsKey(sv))
                    sv = Values[sv].ToString();
                AscAn.Add(new object[] { step, ss, "", si, sp, sv });
                sv = "";
                if (needClear)
                {
                    poliz.Clear();
                    needClear = false;
                }
                cpol.Clear();
            }
            return true;
        }

        public void AddVariables(string[] names)
        {
            foreach (var n in names)
                AddVariable(n);
        }

        protected void AddVariable(params string[] names)
        {
            foreach (var name in names)
                if(!Values.ContainsKey(name))
                    Values.Add(name, 0);
        }

        protected void SetVariable(string name, double value)
        {
            Values[name] = value;
        }

        protected double GetVariableValue(string name)
        {
            return Values[name];
        }
        
        protected string FindRelation(string input, string stack)
        {
            var rel = Relations.GetInstance();
            int i;
            int left = -1;
            int right = -1;
            if (stack == "#") return "<";
            else if (input == "#") return ">";
            else
            {
                for (i = 0; i < rel.Headers.Count; i++)
                {
                    if (stack == rel.Headers[i]) left = i;
                    if (input == rel.Headers[i]) right = i;
                }

                return rel.Table[left, right];
            }
        }

    }
}
