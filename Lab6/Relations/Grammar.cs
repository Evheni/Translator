using System;
using System.Collections.Generic;
using System.IO;

namespace GrammarRelations
{
    public static class Grammar
    {
        public static List<GrammarLine> Lines { get; private set; }

        private static string grammar =
        @"	<прогр>		::= var <сп.огол1> begin <enter1> <сп.оп1> end
			<сп.огол>	::= <огол> <enter2> | <сп.огол> <огол> <enter2>
			<огол>		::= <сп.ід> : <тип>
			<сп.ід>		::= , id | <сп.ід> , id
			<тип>		::= real
			<сп.оп>		::= <оп1> <enter2> | <сп.оп> <оп1> <enter2>
			<н.оп>		::= <присв> | <ввід> | <вивід> | <цикл> | <умов.оп>
			<оп>		::= <н.оп> | & label
			<присв>		::= id = <ар.вир1>
			<ввід>		::= ReadLine ( <сп.ід1> )
			<вивід>		::= WriteLine ( <сп.ід1> )
			<цикл>		::= do <умова> <enter1> <сп.оп1> next
			<умова>		::= id = <ар.вир1> to <ар.вир1>
			<умов.оп>	::= if <лог.вир1> then goto label
			<лог.вир>	::= <лог.дод1> | <лог.вир> or <лог.дод1>
			<лог.дод>	::= <лог.мн1> | <лог.дод> and <лог.мн1>
			<лог.мн>	::= <віднош> | not <лог.мн> | [ <лог.вир1> ]
			<віднош>	::= <ар.вир> <знак> <ар.вир1>
			<знак>		::= < | > | <= | >= | == | !=
			<ар.вир>	::= <ар.дод1> | <ар.вир> + <ар.дод1> | <ар.вир> - <ар.дод1>
			<ар.дод>	::= <ар.мн> | <ар.дод> * <ар.мн> | <ар.дод> / <ар.мн>
			<ар.мн>		::= id | con | ( <ар.вир1> )
			<сп.огол1>	::= <сп.огол>
			<сп.ід1>	::= <сп.ід>
			<сп.оп1>	::= <сп.оп>
			<лог.вир1>	::= <лог.вир>
			<лог.дод1>	::= <лог.дод>
			<лог.мн1>	::= <лог.мн>			
			<ар.вир1>	::= <ар.вир>
			<ар.дод1>	::= <ар.дод>
			<оп1>		::= <оп>			
			<enter1>	::= <enter2>
			<enter2>	::= enter";


        static Grammar()
        {
            Lines = new List<GrammarLine>();
            StringReader reader = new StringReader(grammar.Replace("\t", ""));
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                Lines.Add(new GrammarLine(line));
            }
        }
    }

    public class GrammarLine
    {
        public string Left { get; private set; }
        public List<List<string>> Right { get; private set; }

        public GrammarLine(string line)
        {
            string[] parts = line.Split(new string[] { "::= " }, StringSplitOptions.RemoveEmptyEntries);
            Left = parts[0];
            Right = new List<List<string>>();
            foreach (var part in parts[1].Split('|'))
            {
                Right.Add(new List<string>(part.Split(new string[] { " " },
                    StringSplitOptions.RemoveEmptyEntries)));
            }
        }
    }
}
