using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    public struct Operation
    {
        public string Name { get; private set;}
        public int Priority { get; private set; }

        public Operation(string name, int priority)
        {
            this.Name = name;
            this.Priority = priority;
        }
    }
    class Poliz
    {
        Stack<Operation> shop = new Stack<Operation>();
        List<string> output = new List<string>();
        HashSet<Operation> priorityTable = new HashSet<Operation>();

        public Poliz()
        {
            priorityTable.Add(new Operation("(", 0));
            priorityTable.Add(new Operation("[", 0));
            priorityTable.Add(new Operation(")", 1));
            priorityTable.Add(new Operation("]", 1));
            priorityTable.Add(new Operation("or", 1));
            priorityTable.Add(new Operation("and", 2));
            priorityTable.Add(new Operation("not", 3));
            priorityTable.Add(new Operation("<", 4));
            priorityTable.Add(new Operation(">", 4));
            priorityTable.Add(new Operation("==", 4));
            priorityTable.Add(new Operation("!=", 4));
            priorityTable.Add(new Operation("<=", 4));
            priorityTable.Add(new Operation(">=", 4));
            priorityTable.Add(new Operation("+", 5));
            priorityTable.Add(new Operation("-", 5));
            priorityTable.Add(new Operation("*", 6));
            priorityTable.Add(new Operation("/", 6));
        }

        public string AddElement(string name, bool isConstOrId = false)
        {
            if (isConstOrId)
            {
                AddToList(name);
            }
            else if (IsOperation(name))
            {
                AddToStack(name);
            }
            //else
            //{
            //    throw new ArgumentException("Element isn't consistented in the table");
            //}
            
            return string.Join<string>(" ", output);
        }

        public List<string> Build()
        {
            while (shop.Count > 0)
                AddToList(shop.Pop().Name);
            return output;
        }

        private void AddToList(string name)
        {
            if (name == "(" 
                || name == ")" 
                || name == "[" 
                || name == "]")
                return;
            output.Add(name);
        }

        private void AddToStack(string name)
        {
            var operation = GetOperation(name);

            if (name != "(" && name != "[")
            {
                while (shop.Count > 0
                    && shop.Peek().Priority >= operation.Priority)
                {
                    AddToList(shop.Pop().Name);
                }
            }

            if (name != ")" && name != "]")
                shop.Push(operation);
        }

        public bool IsOperation(string name)
        {
            try
            {
                var item = GetOperation(name);
            }
            catch
            {
                return false;
            }
            return true;
        }

        private Operation GetOperation(string name)
        {
            return priorityTable.First(x => { return x.Name == name; });
        }
    }
}
