namespace CBUS.Bifrost.Model
{
    public class OpCodeReservedLine : OpCodeLine
    {
        #region Properties

        protected override string TypeName { get; set; } = "Reserved";

        public string Reason { get; }

        #endregion

        #region Constructors

        private OpCodeReservedLine(int number, string text)
            : base(number, text)
        {
            //   0        1           2
            // # "opcode",Value (hex),"reserved"

            if (this.Items.Length < 3) return;

            this.Reason = this.Items[2];
        }

        #endregion

        #region Methods

        public new static OpCodeReservedLine Create(int number, string text)
        {
            var result = new OpCodeReservedLine(number, text);
            if (result.Items.Length < 3)
                return null;
            return result;
        }

        #endregion

        #region Overrides

        public override string ToString() => $"{base.ToString()}{this.Reason}";

        #endregion
    }
}
