using System.Globalization;

namespace CBUS.Bifrost.Model
{
    public class EnumerationLine : Line
    {
        #region Properties

        public string EnumName { get; set; }
        public int Value { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public bool IsFlags { get; }

        #endregion

        #region Constructors

        private EnumerationLine(int number, string text)
            : base(number, text)
        {
            //   0             1        2     3        4
            // # "enumeration",EnumName,Value,ItemName,Description

            if (this.Items.Length <= 4) return;

            this.EnumName = this.Items[1];
            var value = this.Items[2].ToUpper();
            if (!value.StartsWith("0X"))
            {
                this.IsFlags = true;
            }

            this.Value = this.IsFlags
                ? int.Parse(value)
                : int.Parse(value.Replace("0X", string.Empty),
                            NumberStyles.HexNumber);

            this.ItemName = this.Items[3];
            this.Description = this.Items[4];
        }

        #endregion

        #region Methods

        public static new EnumerationLine Create(int number, string text)
        {
            var result = new EnumerationLine(number, text);
            if (result.Items.Length <= 4)
                return null;
            if (string.IsNullOrEmpty(result.EnumName))
                return null;
            if (string.IsNullOrEmpty(result.ItemName))
                return null;
            return result;
        }

        #endregion

        #region Overrides

        public override string ToString() =>
            $"{this.EnumName} : {this.Value} : {this.ItemName} : {this.Description}";

        #endregion
    }
}
