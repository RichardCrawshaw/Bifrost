using System.Globalization;
using System.Linq;

namespace CBUS.Bifrost.Model
{
    public abstract class OpCodeLine : Line
    {
        #region Properties

        protected abstract string TypeName { get; set; }

        public byte Value { get; }

        #endregion

        #region Constructors

        protected OpCodeLine(int number, string text)
            : base(number, text)
        {
            //   0        1           
            // # "opcode",Value (hex),

            var value = this.Items[1].ToUpper().Replace("0X", string.Empty);
            this.Value = byte.Parse(value, NumberStyles.HexNumber);
            this.Items = this.Items.ToArray();
        }

        #endregion

        #region Methods

        public static new OpCodeLine Create(int number, string text)
        {
            var items = text.Split(',');

            if (items.Length < 3) return null;

            if (items[2].Trim() == "values")
                return OpCodeValueLine.Create(number, text);
            if (items[2].Trim() == "reserved")
                return OpCodeReservedLine.Create(number, text);
            if (items[2].Trim() == "description")
                return OpCodeDescriptionLine.Create(number, text);
            if (items[2].Trim() == "property")
                return OpCodePropertyLine.Create(number, text);
            if (items[2].Trim() == "tostring")
                return OpCodeToStringLine.Create(number, text);

            return null;
        }

        #endregion

        #region Overrides

        public override string ToString() => $"0x{this.Value:X2} {this.TypeName}: ";

        #endregion
    }
}
