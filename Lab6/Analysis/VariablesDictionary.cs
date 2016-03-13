using Lexemes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analysis
{
    class VariablesDictionary : IEnumerable<Identifier>
    {
        HashSet<Identifier> variables = new HashSet<Identifier>();
        static VariablesDictionary instance = null;

        private VariablesDictionary() { }

        public static VariablesDictionary Instance
        {
            get
            {
                if (instance == null)
                    instance = new VariablesDictionary();
                return instance;
            }
        }

        public void Add(string name, double value = 0)
        {
            var conteins = from c in variables
                           where c.Name == name
                           select c;
            if (conteins.Count() == 0)
            {
                var v = new Identifier(name);
                v.Value = value;
                variables.Add(v);
            }
        }

        public void Update(string name, double value)
        {
            variables.First(x => { return x.Name == name; }).Value = value;
        }

        public IEnumerator<Identifier> GetEnumerator()
        {
            return ((IEnumerable<Identifier>)variables).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Identifier>)variables).GetEnumerator();
        }
    }
}
