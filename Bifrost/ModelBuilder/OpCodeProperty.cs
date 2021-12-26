using System.Collections.Generic;
using System.Linq;

namespace CBUS.Bifrost.Model
{
    public class OpCodeProperty
    {
        //# "opcode",Value (hex),"property",Source,Name
        // # "property",Name,DataType

        #region Properties

        public byte OpCodeValue { get; }
        public string Source { get; }
        public char[] ByteIndexes { get; }
        public char[] BitIndexes { get; }
        public string Name { get; }
        public string DataType { get; }
        public string Format { get; }

        #endregion

        #region Constructors

        private OpCodeProperty(OpCodePropertyLine opCodePropertyLine, List<PropertyLine> propertyLines)
        {
            this.OpCodeValue = opCodePropertyLine.Value;
            this.Source = opCodePropertyLine.Source;
            this.Name = opCodePropertyLine.Name;

            var propertyLine =
                propertyLines
                    .Where(pl => pl.Name == this.Name)
                    .FirstOrDefault();
            this.DataType = propertyLine?.DataType ?? "byte";
            this.Format = propertyLine?.Format ?? "decimal";

            if (this.Source.Contains(':'))
            {
                var items = this.Source.Split(':');
                this.ByteIndexes = items[0].ToCharArray();
                this.BitIndexes = items[1].ToCharArray();
            }
            else
            {
                this.ByteIndexes = this.Source.ToCharArray();
                this.BitIndexes = System.Array.Empty<char>();
            }
        }

        #endregion

        #region Methods

        public static OpCodeProperty Create(OpCodePropertyLine opCodePropertyLine, List<PropertyLine> propertyLines)
        {
            var result = new OpCodeProperty(opCodePropertyLine, propertyLines);
            return result;
        }

        #endregion
    }
}
