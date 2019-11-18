using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSLab4
{
    class Identifier : IComparable<Identifier>
    {
        public string Value { get; private set; }

        public Identifier(string value)
        {
            if (ValidateIdentifier(value) == false)
                throw new ArgumentException();

            Value = value;
        }

        private bool ValidateIdentifier(string identifier)
        {
            if (identifier.Length > 3)
                return false;

            char[] tmp = identifier.ToCharArray();
            if (Char.IsDigit(tmp[0])) return false;
            if (Char.IsDigit(tmp[1])) return false;
            if (Char.IsDigit(tmp[2])) return false;

            return true;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        #region IComparable
        public int CompareTo(Identifier other)
        {
            return Value.CompareTo(other.Value);
        }
        #endregion
    }
}
