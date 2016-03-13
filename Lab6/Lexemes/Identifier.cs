namespace Translator
{
    internal class Identifier
    {
        #region Properties
        public int? Index { get; set; }
        public int? Value { get; set; }
        public string Type { get; set; }
        public string Name { get; protected set; }
        #endregion

        #region Constructors
        public Identifier(int? index, string type, string name)
        {
            Index = index;
            Type = type;
            Name = name;
        }

        public Identifier(int? index, string name) :
            this(index, null, name)
        { }

        public Identifier(string name) :
            this(null, null, name)
        { }
        #endregion

        #region Overriding System.Object
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var str = obj.ToString();
            if (str == null)
                return false;
            return Name.Equals(str);
        }

        public override string ToString()
        {
            return Name;
        }
        #endregion
    }
}