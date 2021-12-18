namespace Bifrost.Model
{
    public class OpCodeToStringLine : OpCodeLine
    {
        #region Properties

        protected override string TypeName { get; set; } = "ToString";

        public string Text { get; }

        #endregion

        #region Constructors

        private OpCodeToStringLine(int number, string text)
            : base(number, text)
        {
            //   0        1           2          3
            // # "opcode",Value (hex),"tostring",Format-string

            if (this.Items.Length < 4) return;

            this.Text = this.Items[3];
        }

        #endregion

        #region Methods

        public new static OpCodeToStringLine Create(int number, string text)
        {
            var result = new OpCodeToStringLine(number, text);
            if (result.Items.Length < 4)
                return null;
            return result;
        }

        #endregion

        #region Overrides

        public override string ToString() => $"{base.ToString()}{this.Text}";

        #endregion
    }
}
