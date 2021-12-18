namespace Bifrost.Model
{
    public class OpCodeDescriptionLine : OpCodeLine
    {
        #region Properties

        protected override string TypeName { get; set; } = "Description";

        public string Text { get; }

        #endregion

        #region Constructors

        private OpCodeDescriptionLine(int number, string text)
            : base(number, text)
        {
            //   0        1           2             3
            // # "opcode",Value (hex),"description",Text

            if (this.Items.Length < 4) return;

            this.Text = this.Items[3].Trim('"');
        }

        #endregion

        #region Methods

        public new static OpCodeDescriptionLine Create(int number, string text)
        {
            var result = new OpCodeDescriptionLine(number, text);
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
